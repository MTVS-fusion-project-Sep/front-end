using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//포톤을 쓰기위해 추가
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPlayer());
    }
    IEnumerator SpawnPlayer()
    {
        //룸에 입장이 될때까지 기다린다.
        yield return new WaitUntil(() => { return PhotonNetwork.InRoom; });


        Vector2 radomPos = Random.insideUnitCircle * 5.0f;
        Vector3 initPosition = new Vector3(radomPos.x, 1.0f, radomPos.y);
        //플레이어 생성하자, 이름,위치.회전
        PhotonNetwork.Instantiate("Player", initPosition, Quaternion.identity);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    


}
