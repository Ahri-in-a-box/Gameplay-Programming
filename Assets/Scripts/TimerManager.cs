using TMPro;
using UnityEngine;

[System.Serializable]
public class CTime
{
    [SerializeField, Range(0, 3)]
    public int Minutes;
    [SerializeField, Range(0, 60)]
    public int Secondes;
    public long TotalSecondes
    {
        get
        {
            return Minutes * 60 + Secondes;
        }
    }

    public static CTime operator -(CTime a, CTime b)
    {
        CTime res = a > b ? new()
        {
            Minutes = a.Minutes - b.Minutes,
            Secondes = a.Secondes - b.Secondes
        } : new()
        {
            Minutes = b.Minutes - a.Minutes,
            Secondes = b.Secondes - a.Secondes
        };

        if(res.Secondes < 60)
        {
            res.Secondes += 60;
            res.Minutes--;
        }

        if (res.Secondes > 60)
        {
            res.Secondes -= 60;
            res.Minutes++;
        }

        return res;
    }

    public static bool operator >(CTime a, CTime b)
    {
        if (a.Minutes > b.Minutes)
            return true;
        if (a.Minutes < b.Minutes)
            return false;
        return a.Secondes > b.Secondes;
    }

    public static bool operator <(CTime a, CTime b)
    {
        if (a.Minutes < b.Minutes)
            return true;
        if (a.Minutes > b.Minutes)
            return false;
        return a.Secondes < b.Secondes;
    }
}

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

    void Start()
    {
        m_Stopwatch = new System.Diagnostics.Stopwatch();
    }

    void Update()
    {
        if (IsGameOver || IsPaused || !IsStarted)
            return;

        CTime remaining = GetRemainingTime();

        if (m_TimerUI)
            m_TimerUI.text = $"{remaining.Minutes}:{remaining.Secondes}";

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
