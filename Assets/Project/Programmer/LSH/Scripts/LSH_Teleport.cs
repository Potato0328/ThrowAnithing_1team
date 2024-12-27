using UnityEngine;
using UnityEngine.SceneManagement;

public class LSH_Teleport : MonoBehaviour
{
    SceneField nextStage; //�������� �̵��� ����
    SceneField randomHiddenRoom; //��й� ����� ����

    Vector3 beforeTeleportPos; //���� ������ ��й����� ����Ÿ�� �� ��ġ
    Vector3 afterTeleportPos; //�ֵ�Ƽ�� ������ ����ź �� ��ġ

    [SerializeField] bool isSceneAdditive; //T�ֵ�Ƽ�� ���� ��������, F�ƴ�


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "PortalHidden")
        {            
            //��Ƽ �� �ε�, LoadSceneMode.Additive
            randomHiddenRoom = other.GetComponent<PortalSceneNumber>().hiddenSceneArr[Random.Range(0, 2)];
            ChangeScene(randomHiddenRoom); //0���� 1���� 2���Ĩ
        }

        Debug.Log(0);   

        if (other.tag == Tag.Portal)
        {
            Debug.Log(1);   
            //���߾� �ε����϶�
            if (isSceneAdditive)
            {
                Debug.Log(2);   
                //�÷��̾� �������� �ִ� �ڸ��� ��������
                transform.position = beforeTeleportPos;
                Debug.Log(3);   
                //���� false�� �ٲ� ��
                isSceneAdditive = false;
                Debug.Log(4);   

                //����� ���� ���� ������ �� ��ε��() ����
                Scene additiveScene = SceneManager.GetSceneByName(randomHiddenRoom.SceneName);
                Debug.Log(5);   
                SceneManager.UnloadScene(additiveScene);
                Debug.Log(6);   
                return;
                Debug.Log(7);   

            }
            //�������� �̵��Ҷ�
            else
            {
                Debug.Log(8);   

                //���� �� �̵�, LoadSceneMode.Single
                nextStage = other.GetComponent<PortalSceneNumber>().nextScene;
                Debug.Log(9);   
                SceneManager.LoadScene(nextStage);
                Debug.Log(10);   
                return;
                Debug.Log(11);   


            }
            Debug.Log(12);   
        }
        
        Debug.Log(13);   
        

    }

    public void ChangeScene(SceneField SceneName)
    {
        //�ֵ�Ƽ��� ���� �� ����
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        isSceneAdditive = true;

        //�÷��̾� ������ ��ġ ����
        beforeTeleportPos = transform.position;
        afterTeleportPos = new Vector3(100, 1, 100);

        //�÷��̾� ���������� �ֵ�Ƽ�� ������ ��ġ �̵�
        transform.position = afterTeleportPos;

    }

}
