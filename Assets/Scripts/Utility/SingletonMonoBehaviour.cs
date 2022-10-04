using System;
using UnityEngine;

namespace Team8.Unemployment.Utility
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get;private set; }
        public virtual void Awake()
        {
            if(Instance == null)
            {
                Instance = this as T;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }
    }
}