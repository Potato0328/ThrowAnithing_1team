using UnityEngine;
using UnityEngine.SceneManagement;

public class LSH_Teleport : MonoBehaviour
{
    SceneField nextStage; //�������� �̵��� ����
    SceneField randomHiddenRoom; //��й� ����� ����

    Transform beforeTeleportPos;
    Transform afterTeleportPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.Portal)
        {
            nextStage = other.GetComponent<PortalSceneNumber>().nextScene;
            SceneManager.LoadScene(nextStage); //���� �� �̵�, LoadSceneMode.Single
        }
        
        if (other.tag == "PortalHidden")
        {
            //0���� 1���� 2���Ĩ
            randomHiddenRoom = other.GetComponent<PortalSceneNumber>().hiddenSceneArr[Random.Range(0,2)];
            ChangeScene(randomHiddenRoom); //��Ƽ �� �ε�, LoadSceneMode.Additive
        }

    }

    public void ChangeScene(SceneField SceneName)
    {
        //�÷��̾� ������ ��ġ ����
        beforeTeleportPos = this.transform;

        //�ֵ�Ƽ��� �� ����
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);

        //�÷��̾� ���������� �ֵ�Ƽ�� ������ ��ġ �̵�
        Vector3.MoveTowards(transform.position, beforeTeleportPos.position, 1f);
    }

}
