using UnityEngine;
using UnityEngine.SceneManagement;

public class LSH_Teleport : MonoBehaviour
{
    int travelNum;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Portal")
        {
            Debug.Log("1.����ϴ�?");
            travelNum = other.GetComponent<PortalSceneNumber>().SceneNum;                        
            Debug.Log(travelNum);
            SceneManager.LoadScene(travelNum);
        }
        //�÷��̾ ��� �ֵ鸶�� �̸� �����Ҽ��� ����
        //������ �°� �����ִ� ���� int���� �����ͼ�
        //�� ������ �Ѿ�� �ϸ� �����Ű�����
        //�ϱ� ������ �÷��̾ �ѱ�� �ٸ� ��ü�� �ѱ��
        //�ֵ��ǰ� ��������ϱ� ���� �Ѱܵ� ������ڱ���

        
    }

    //�̰� ��Ż�� ���ڶ� ��� ��������?????
    public void ChangeScene(int SceneNum)
    {
        SceneManager.LoadScene("Test02_Trap");
    }

}
