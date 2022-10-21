using System;
using UnityEngine;
public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static Lazy<T> LazyInstance = null;

    public static T Instance => LazyInstance.Value;

    private static T CreateSingleton()
    {
        var ownerObject = new GameObject($"{typeof(T).Name} (singleton)");
        var instance = ownerObject.AddComponent<T>();
        DontDestroyOnLoad(ownerObject);
        return instance;
    }

    protected virtual void Awake()
    {
        if(LazyInstance == null)
            LazyInstance = new(CreateSingleton);
    }
}