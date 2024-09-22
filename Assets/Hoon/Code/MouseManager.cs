using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //마우스 우크ㄹㄹㅣㄱ
        if (Input.GetMouseButtonDown(1))
        {
            print("마우스우클릭");
            // 마우스 위치에서 Ray를 발사
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Ray가 오브젝트에 충돌하는지 확인
            if (Physics.Raycast(ray, out hit))
            {
                // Ray가 충돌한 오브젝트가 이 스크립트가 붙어있는 오브젝트인지 확인
                if (hit.transform.name.Contains("NPC"))
                {
                    // 마우스가 오브젝트 위에 있을 때
                    Debug.Log("마우스가 오브젝트 위에 있습니다: " + hit.transform.name);
                }
            }
        }
    }
}
