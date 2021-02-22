using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonPattern<T> : MonoBehaviour where T : SingletonPattern<T>
{
    /// <summary> The singleton instance of this class. </summary>
    public static T Instance;

    //To create Awake in derived classes, use an override Awake and call base.Awake()
    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this as T;
        //DontDestroyOnLoad(this);
    }
}
