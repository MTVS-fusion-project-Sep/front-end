using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Net;

public class UDPConnenter : MonoBehaviour
{
    public int portNumber = 5000;
    public List<Vector3> receivedPoseList = new List<Vector3>();
    // public ReceivedPoseList receivedPoseList;

    Thread udpThread;
    UdpClient receivePort;

    UdpClient sendPort;

    void Start()
    {
        InitializeUDPThread();
    }

    void InitializeUDPThread()
    {
        // ��׶��忡�� �� Thread�� �����ϰ� �ʹ�.
        udpThread = new Thread(new ThreadStart(ReceiveData));
        udpThread.IsBackground = true;
        udpThread.Start();
    }

    void ReceiveData()
    {
        //�����͸� �ް� ��ȯ�Ͽ� ����Ʈ�� �ִ� ����

        // ���� ���� �� ���� Ŭ���̾�Ʈ�� �����Ѵ�.
        receivePort = new UdpClient(portNumber);
        IPEndPoint remoteClient = new IPEndPoint(IPAddress.Any, portNumber);
        try
        {
            while (true)
            {
                // ��� ����� ���̳ʸ� �����͸� �޴´�.
                byte[] bins = receivePort.Receive(ref remoteClient);
                string binaryString = Encoding.UTF8.GetString(bins);
                //print($"���� ������ : {binaryString}");

                PoseList jsonData = JsonUtility.FromJson<PoseList>(binaryString);

                receivedPoseList.Clear();

                // ��ȯ�� json �迭 �����͸� ���� ������ ����Ʈ�� �����Ѵ�.
                foreach (PoseData poseData in jsonData.landmarkList)
                {
                    Vector3 receiveVector = new Vector3(poseData.x, poseData.y, poseData.z);
                    receivedPoseList.Add(receiveVector);

                }
            }
        }
        catch (SocketException message)
        {
            // ��ſ����ڵ� �� ���� ������ ����Ѵ�.
            Debug.LogError($"Error code : {message.ErrorCode} - {message}");
        }
        finally
        {
            receivePort.Close();
        }

    }


    //������ ������
    void SendData(string message)
    {
        //Ŭ���̾�Ʈ�μ� �غ�
        sendPort = new UdpClient(portNumber);

        //�����͸� �����Ѵ�.
        byte[] binData = Encoding.UTF8.GetBytes(message);
        sendPort.Send(binData, binData.Length, "168.12.0.1", 7000);
    }




    private void OnDisable()
    {
        //UDP ��Ʈ���� �����Ѵ�.
        receivePort.Close();
    }
}



[System.Serializable]
public struct PoseData
{
    public float x;
    public float y;
    public float z;

}

[System.Serializable]
public struct PoseList
{
    public List<PoseData> landmarkList;
}

public enum PoseName
{
    nose,
    left_eye_inner,
    left_eye,
    left_eye_outer,
    right_eye_inner,
    right_eye,
    right_eye_outer,
    left_ear,
    right_ear,
    mouth_left,
    mouth_right,
    left_shoulder,
    right_shoulder,
    left_elbow,
    right_elbow,
    left_wrist,
    right_wrist,
    left_pinky,
    right_pinky,
    left_index,
    right_index,
    left_thumb,
    right_thumb,
    left_hip,
    right_hip,
    left_knee,
    right_knee,
    left_ankle,
    right_ankle,
    left_heel,
    right_heel,
    left_foot_index,
    right_foot_index
}

//[System.Serializable]
//public struct ReceivedPoseList
//{
//    public Vector3 nose;
//    public Vector3 left_eye_inner;
//    public Vector3 left_eye;
//    public Vector3 left_eye_outer;
//    public Vector3 right_eye_inner;
//    public Vector3 right_eye;
//    public Vector3 right_eye_outer;
//    public Vector3 left_ear;
//    public Vector3 right_ear;
//    public Vector3 mouth_left;
//    public Vector3 mouth_right;
//    public Vector3 left_shoulder;
//    public Vector3 right_shoulder;
//    public Vector3 left_elbow;
//    public Vector3 right_elbow;
//    public Vector3 left_wrist;
//    public Vector3 right_wrist;
//    public Vector3 left_pinky;
//    public Vector3 right_pinky;
//    public Vector3 left_index;
//    public Vector3 right_index;
//    public Vector3 left_thumb;
//    public Vector3 right_thumb;
//    public Vector3 left_hip;
//    public Vector3 right_hip;
//    public Vector3 left_knee;
//    public Vector3 right_knee;
//    public Vector3 left_ankle;
//    public Vector3 right_ankle;
//    public Vector3 left_heel;
//    public Vector3 right_heel;
//    public Vector3 left_foot_index;
//    public Vector3 right_foot_index;

//    public void SetVectorData(List<Vector3> list)
//    {

//    }
//}
