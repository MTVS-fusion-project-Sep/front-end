using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������ �������� �߰�
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
        //�뿡 ������ �ɶ����� ��ٸ���.
        yield return new WaitUntil(() => { return PhotonNetwork.InRoom; });


        Vector2 radomPos = Random.insideUnitCircle * 5.0f;
        Vector3 initPosition = new Vector3(radomPos.x, 1.0f, radomPos.y);
        //�÷��̾� ��������, �̸�,��ġ.ȸ��
        PhotonNetwork.Instantiate("Player", initPosition, Quaternion.identity);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    


}
