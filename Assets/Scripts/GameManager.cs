using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    private int m_Score = 0;

    [SerializeField]
    private TimerManager m_TimeManager = null;

    [SerializeField]
    private XRSocketInteractor m_Socket1 = null;
    [SerializeField]
    private XRSocketInteractor m_Socket2 = null;
    [SerializeField]
    private XRSocketInteractor m_Socket3 = null;

    public void Awake()
    {
        if (!m_TimeManager)
            throw new System.Exception("TimeManager of GameManager must be set");
        if (!(m_Socket1 && m_Socket2 && m_Socket3))
            throw new System.Exception("All Interation Socket of GameManager must be set");
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

        

        /*
         * Si 0: rien
         * Si 1: Animation ou Son �chec OU rien
         * Si 2: Animation ou Son r�ussite OU rien
         * Si 3: Si 2 + augmentation du score
         */
    }
}
