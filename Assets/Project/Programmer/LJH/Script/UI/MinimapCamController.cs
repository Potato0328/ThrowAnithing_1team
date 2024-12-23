using BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MinimapCamController : MonoBehaviour
{
    //��Ŀ�� ĳ����, ������Ʈ, ���� � �����տ� �ٿ��ְ� ���̾� ó��

    [SerializeField] GameObject player;
    [SerializeField] GameObject minimap;

    [SerializeField] bool minimapAct;
    [SerializeField] bool minimapFix;
    private void Update()
    {
        CamPos();
        CamRot();
        CamActivated();
    }

    // ���ξ��� �ھƾ���
    void CamPos()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x, 10, player.transform.position.z);
    }

    // ȯ�� �������� �̴ϸ� �Ƚ��� ���������� ȣ��
    void CamRot()
    {
        if(!minimapFix)
        transform.eulerAngles = new Vector3(90, player.transform.eulerAngles.y, 0);

    }

    // ȯ�� �������� �̴ϸ� ��Ƽ����Ʈ ���������� ȣ��
    void CamActivated()
    {
        
        minimap.SetActive(minimapAct);
        
    }
}
