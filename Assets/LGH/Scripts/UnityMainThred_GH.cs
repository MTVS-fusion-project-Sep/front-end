using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityMainThred_GH : MonoBehaviour
{
    private static readonly Queue<Action> _executionQueue = new Queue<Action>();

    private void Update()
    {
        lock (_executionQueue)
        {
            while (_executionQueue.Count > 0)
            {
                _executionQueue.Dequeue().Invoke();
            }
        }
    }

    public void Enqueue(Action action)
    {
        lock (_executionQueue)
        {
            _executionQueue.Enqueue(action);
        }
    }

    private static UnityMainThred_GH _instance;
    public static UnityMainThred_GH Instance()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<UnityMainThred_GH>();
            if (!_instance)
            {
                var obj = new GameObject("UnityMainThreadDispatcher");
                _instance = obj.AddComponent<UnityMainThred_GH>();
                DontDestroyOnLoad(obj);
            }
        }
        return _instance;
    }
}
