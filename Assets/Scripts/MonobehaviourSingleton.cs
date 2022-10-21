using System;
using UnityEngine;
public abstract class MonoBehaviourSingleton : MonoBehaviour
{
    private static MonoBehaviourSingleton m_Instance = null;

    public static MonoBehaviourSingleton Instance => m_Instance;

    protected virtual void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        if(m_Instance != this)
            Destroy(this);
    }
}