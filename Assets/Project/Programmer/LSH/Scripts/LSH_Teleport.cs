using UnityEngine;
using UnityEngine.SceneManagement;

public class LSH_Teleport : MonoBehaviour
{
    [SerializeField] GameObject player;
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
            other.gameObject.SetActive(false);
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

                ////����� ���� ���� ������ �� ��ε��() ���� -> ����, ���� �ʴ°����� �ذ�
                //Scene additiveScene = SceneManager.GetSceneByName(randomHiddenRoom);
                //Debug.Log(4);
                //SceneManager.UnloadScene(additiveScene);
                //Debug.Log(5);

                //���� false�� �ٲ�
                isSceneAdditive = false;
                Debug.Log(6);
                

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
                


            }
            Debug.Log(12);   
        }
        
        Debug.Log(13);   
        

    }

    public void ChangeScene(SceneField SceneName)
    {
        Debug.Log(999);
        //�ֵ�Ƽ��� ���� �� ����
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        isSceneAdditive = true;

        //�÷��̾� ������ ��ġ ����
        beforeTeleportPos = player.transform.position;
        afterTeleportPos = new Vector3(100, 1, 100);

        //�÷��̾� ���������� �ֵ�Ƽ�� ������ ��ġ �̵�
        transform.position = afterTeleportPos;

    }

}
