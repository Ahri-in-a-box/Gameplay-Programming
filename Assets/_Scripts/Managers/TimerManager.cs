using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField]
    private CTime m_GameDuration = new() { Minutes = 1, Secondes = 30 };
    [SerializeField]
    private TextMeshProUGUI m_TimerUI = null;

    private System.Diagnostics.Stopwatch m_Stopwatch = null;
    public bool IsGameOver { get; private set; }
    public bool IsStarted
    {
        get
        {
            return m_Stopwatch != null && m_Stopwatch.Elapsed != System.TimeSpan.Zero;
        }
    }
    public bool IsPaused
    {
        get
        {
            return !(IsStarted && m_Stopwatch.IsRunning);
        }
    }

    public delegate void GameOverEvent();
    public static event GameOverEvent OnGameOver;

    private static TimerManager m_Instance = null;
    public static TimerManager Instance => m_Instance;

    protected void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        if (m_Instance != this)
            Destroy(this);
    }

    void Start()
    {
        m_Stopwatch = new System.Diagnostics.Stopwatch();
        if (m_TimerUI)
            m_TimerUI.text = $"0:00";
    }

    void Update()
    {
        if (IsGameOver || IsPaused || !IsStarted)
            return;

        CTime remaining = GetRemainingTime();

        if (m_TimerUI)
            m_TimerUI.text = $"{remaining.Minutes}:{remaining.Secondes:D2}";

        if (!(m_GameDuration > GetTimeElapsed()))
        {
            StopTimer();
            OnGameOver?.Invoke();
        }
    }

    public void StartTimer()
    {
        if(!IsStarted || IsGameOver)
        {
            IsGameOver = false;
            m_Stopwatch.Reset();
            m_Stopwatch.Start();
        }
    }

    public void PauseTimer()
    {
        if(IsStarted && !IsGameOver)
            if (IsPaused)
                m_Stopwatch.Start();
            else
                m_Stopwatch.Stop();
    }

    public void StopTimer()
    {
        m_Stopwatch.Stop();
        IsGameOver = true;
    }

    public CTime GetTimeElapsed()
    {
        long secondsPassed = m_Stopwatch.ElapsedMilliseconds / 1000;
        return new ()
        {
            Minutes = (int)(secondsPassed / 60),
            Secondes = (int)(secondsPassed % 60)
        };
    }

    public CTime GetRemainingTime()
    {
        return m_GameDuration - GetTimeElapsed();
    }
}
