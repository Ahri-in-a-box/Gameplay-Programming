using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

/*
 * Notes:
 *  -   Gerer le score
 *  -   Gerer combinaison
 *  -   Gerer aliment al�atoire
 *  -   Action � la fin  du jeu
 */

internal enum GameState
{
    MAIN_MENU,
    STARTED,
    PAUSED,
    OVER
}

public class GameManager : MonoBehaviour
{
    private int m_Score = 0;
    private int m_Combo = 0;
    public int Score { get => m_Score; }

    [Header("Score")]
    [SerializeField]
    private TextMeshProUGUI m_ScoreUI = null;

    [Header("Sockets")]
    [SerializeField]
    private XRSocketInteractor m_Socket1 = null;
    [SerializeField]
    private XRSocketInteractor m_Socket2 = null;
    [SerializeField]
    private XRSocketInteractor m_Socket3 = null;

    [SerializeField]
    private List<GameObject> m_RandomObjectPrefabs = new();

    private TimerManager m_TimerManager;
    private RecipeManager m_RecipeManager;

    private GameState m_GameState = GameState.MAIN_MENU;
    private readonly List<int> m_RecipeDone = new();

    //events
    public delegate void RecipeFailedEvent();
    public static event RecipeFailedEvent OnRecipeFailed;
    public delegate void RecipeSuccessEvent(string recipeName);
    public static event RecipeSuccessEvent OnRecipeSuccess;

    private static GameManager m_Instance = null;
    public static GameManager Instance => m_Instance;

    protected void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        if (m_Instance != this)
            Destroy(this);
    }

    void Start()
    {
        if (!(m_Socket1 && m_Socket2 && m_Socket3))
            throw new System.Exception("All Interation Socket of GameManager must be set");

        m_TimerManager = (TimerManager)TimerManager.Instance;
        m_RecipeManager = (RecipeManager)RecipeManager.Instance;

        TimerManager.OnGameOver += OnTimeEnd;
        if (m_ScoreUI)
            m_ScoreUI.text = $"0";
    }

    public void OnIngredientAdded()
    {
        /*
         * 1: V�rifier s'il y a bien un objet par socket
         * 2: V�rifier si la combinaison existe
         * 3: V�rifier si la combinaison a d�j� �t� faite
         */

        if (!(m_Socket1.hasSelection && m_Socket2.hasSelection && m_Socket3.hasSelection))
            return;

        GameObject o1 = m_Socket1.GetOldestInteractableSelected().colliders[0].gameObject,
            o2 = m_Socket2.GetOldestInteractableSelected().colliders[0].gameObject,
            o3 = m_Socket3.GetOldestInteractableSelected().colliders[0].gameObject;

        
        int res = m_RecipeManager.IsValid(o1, o2, o3);
        // supprimer les �l�ments
        Destroy(o1);
        Destroy(o2);
        Destroy(o3);

        SelectRandomElement();

        if (res < 0)
        {
            //Echec
            m_Combo = 0;
            OnRecipeFailed?.Invoke();
            return;
        }

        OnRecipeSuccess?.Invoke(m_RecipeManager.GetRecipeName(res));

        //R�ussite
        if (!m_RecipeDone.Contains(res))
        {
            m_RecipeDone.Add(res);
            m_Combo++;
            m_Score += 10 * m_Combo;
            if (m_ScoreUI)
                m_ScoreUI.text = $"{m_Score}";
        }else
            m_Combo = 0;


        /*
         * Si 0: rien
         * Si 1: Animation ou Son �chec OU rien
         * Si 2: Animation ou Son r�ussite OU rien
         * Si 3: Si 2 + augmentation du score
         */
    }

    private void SelectRandomElement()
    {
        if(m_RandomObjectPrefabs.Count > 0)
        {
            int rand = Mathf.RoundToInt(Random.Range(0, m_RandomObjectPrefabs.Count) - .5f);
            GameObject obj = Instantiate(m_RandomObjectPrefabs[rand]);
            obj.transform.position = m_Socket1.transform.position;
            m_Socket1.StartManualInteraction(obj.GetComponent<IXRSelectInteractable>());
        }
    }

    public void StartGame()
    {
        if(m_GameState == GameState.MAIN_MENU || m_GameState==GameState.OVER)
        {
            m_GameState = GameState.STARTED;
            SelectRandomElement();
            m_TimerManager.StartTimer();
        }
    }

    public void PauseGame()
    {
        if(m_GameState != GameState.MAIN_MENU && m_GameState != GameState.OVER)
        {
            m_TimerManager.PauseTimer();
            m_GameState = m_TimerManager.IsPaused ? GameState.PAUSED : GameState.STARTED;
        }
    }

    public void QuitGame()
    {
        if(m_GameState != GameState.MAIN_MENU && m_GameState != GameState.OVER)
        {
            m_TimerManager.StopTimer();
            m_GameState = GameState.OVER;
        }
        if (m_GameState != GameState.MAIN_MENU)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void OnTimeEnd()
    {
        if(m_GameState != GameState.MAIN_MENU)
            m_GameState = GameState.OVER;
    }

}
