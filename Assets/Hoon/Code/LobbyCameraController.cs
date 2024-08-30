using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0, 2, 5);
        transform.localEulerAngles = new Vector3(0, 180, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
