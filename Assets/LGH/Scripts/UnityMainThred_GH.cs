using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityMainThred_GH : MonoBehaviour
{
    private static readonly Queue<string> _executionQueue = new Queue<string>();

    private void Update()
    {
        lock (_executionQueue)
        {
            while (_executionQueue.Count > 0)
            {
                _executionQueue.Dequeue();
            }
        }
    }

    public string GetStgring()
    {
        while (_executionQueue.Count > 0)
        {
            return _executionQueue.Dequeue();
        }

        return "";
    }

    public void Enqueue(string str)
    {
        lock (_executionQueue)
        {
            _executionQueue.Enqueue(str);
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
