using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpike : MonoBehaviour
{
    // To Do : n�ʸ��� ���������� ��ֹ����� ���ݹޱ� �÷��̾� ��ũ��Ʈ�� �ű��
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //other.gameObject.

        }

    }
}
