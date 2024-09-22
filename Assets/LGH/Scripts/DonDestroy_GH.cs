using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonDestroy_GH : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


}
