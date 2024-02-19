
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PersistentSingleton<T> : MonoBehaviour where T : Component
{
    public bool AutoUnparentOnAwake = true;

    protected static T _instance;
    public static bool HasInstance()
    {
        return _instance != null;
    }

    public static T TryGetInstance() => HasInstance() ? _instance : null;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    var go = new GameObject(typeof(T).Name + "Generated");
                    _instance = go.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        InitializeSingleton();
    }

    protected virtual void InitializeSingleton()
    {
        if (!Application.isPlaying) { return; }
        if (AutoUnparentOnAwake) { transform.SetParent(null); }
        if(_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
