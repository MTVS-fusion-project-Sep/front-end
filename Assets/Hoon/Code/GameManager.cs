using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������ �������� �߰�
using Photon.Pun;

//����ȭ �뵵 Ŭ������ �θ�� MonoBehaviourPun
public class GameManager : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPlayer());

        // OnPhotonSerializeView ���� ������ ���� �󵵼� �����ϱ� (perSeconds) 
        PhotonNetwork.SerializationRate = 30;
        // ��κ��� ������ ���ۺ� (perSeconds). ����, Instantiate, Load, ����
        PhotonNetwork.SendRate = 30;

    }
    IEnumerator SpawnPlayer()
    {
        //�뿡 ������ �ɶ����� ��ٸ���.
        yield return new WaitUntil(() => { return PhotonNetwork.InRoom; });


        Vector2 radomPos = Random.insideUnitCircle * 5.0f;
        Vector3 initPosition = new Vector3(radomPos.x, 1.0f, radomPos.y);
        //�÷��̾� ��������, �̸�,��ġ.ȸ��
        PhotonNetwork.Instantiate("Player", initPosition, Quaternion.identity);

        // ������ �������� Owner�� �÷��̾�Ը� ����������. Owner�� ������ �����ϸ� ���� �����.
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    


}
