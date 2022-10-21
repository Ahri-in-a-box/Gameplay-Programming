using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PanelAccueil;
    [SerializeField] private GameObject m_PanelCredits;
    [SerializeField] private GameObject m_PanelMenu;
    [SerializeField] private GameObject m_PanelControles;
    [SerializeField] private GameObject m_PanelFin;
    [SerializeField] private GameObject m_PanelSuccess;
    [SerializeField] private GameObject m_PanelNotSuccess;
    [SerializeField] private GameObject m_InvisibleWalls;
    [SerializeField] private GameObject m_ButtonMenu;


    private bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        m_PanelAccueil.SetActive(true);
        m_PanelCredits.SetActive(false);
        m_PanelMenu.SetActive(false);
        m_PanelControles.SetActive(false);
        m_PanelFin.SetActive(false);
        m_PanelSuccess.SetActive(false);
        m_PanelNotSuccess.SetActive(false);
        m_ButtonMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        print("PlayGame");
        m_PanelAccueil.SetActive(false);
        m_PanelControles.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowControls()
    {
        m_PanelControles.SetActive(true);
    }

    public void QuitControls()
    {
        m_PanelControles.SetActive(false);
        if (!m_PanelMenu.active)
        {
            m_InvisibleWalls.SetActive(false);
            m_ButtonMenu.SetActive(true);
        }
    }
    
    public void ShowCredit()
    {
        m_PanelCredits.SetActive(true);
    }

    public void QuitCredit()
    {
        m_PanelCredits.SetActive(false);
        m_PanelAccueil.SetActive(true);
    }

    public void ReturnAccueil()
    {
        print("Accueil");
        m_PanelAccueil.SetActive(true);
        m_PanelCredits.SetActive(false);
        m_PanelMenu.SetActive(false);
        m_PanelControles.SetActive(false);
        m_PanelFin.SetActive(false);
    }

    public void ContinueGame()
    {
        m_PanelMenu.SetActive(false);
        m_PanelControles.SetActive(false);
        m_InvisibleWalls.SetActive(false);
        m_ButtonMenu.SetActive(true);
    }

    public void AfficherMenu()
    {
        m_PanelMenu.SetActive(true);
        m_InvisibleWalls.SetActive(true);
        m_ButtonMenu.SetActive(false);
    }

    public void AfficherSuccess()
    {
        if (isActive)
        {
            m_PanelSuccess.SetActive(false);
            isActive = false;
        }
        else
        {
            m_PanelSuccess.SetActive(true);
            isActive = true;
            Invoke("AfficherSuccess", 5);
        }
    }

    public void AfficherNotSuccess()
    {
        if (isActive)
        {
            m_PanelNotSuccess.SetActive(false);
            isActive = false;
        }
        else
        {
            m_PanelNotSuccess.SetActive(true);
            isActive = true;
            Invoke("AfficherSuccess", 5);
        }
    }
}
