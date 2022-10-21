using UnityEngine;

[System.Serializable]
public class CTime
{
    [SerializeField, Range(0, 3)]
    public int Minutes;
    [SerializeField, Range(0, 59)]
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

        if (res.Secondes < 60)
        {
            res.Secondes += 60;
            res.Minutes--;
        }
        if (res.Secondes >= 60)
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

