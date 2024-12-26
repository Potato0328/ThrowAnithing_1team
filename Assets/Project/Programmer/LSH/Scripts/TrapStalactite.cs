using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapStalactite : MonoBehaviour
{
    [SerializeField] GameObject ray;
    RaycastHit hit;
    [SerializeField] float maxDistance = 15f;

    [SerializeField] GameObject stalactite;
    [SerializeField] Transform stalactiteBeforeMove;
    [SerializeField] Transform stalactiteAfterMove;
    //���̸� ����ִٰ�
    //�÷��̾ ������ ���� ����
    //���ô� �÷��̾�� �ε����� ü�� -40�����
    //���ô� 5�ʵڸ� �����


    private void Update()
    {
        Debug.DrawRay(ray.transform.position, Vector3.down, Color.red);
        Debug.DrawLine(ray.transform.position, Vector3.down, Color.red);
        if (Physics.Raycast(ray.transform.position, Vector3.down, out hit, maxDistance))
        {
            if (hit.collider.gameObject.tag == Tag.Player)
            {
                //TODO: õõ�� �������� ��ġ��
                stalactite.transform.position = 
                    Vector3.MoveTowards(stalactiteBeforeMove.position, stalactiteAfterMove.position, 5f);
            }
            
        }


    }


}
