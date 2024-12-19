using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTester : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float angle;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    public void Attack()
    {
        // ���� �տ� �ִ� ���͵��� Ȯ���ϰ� �������� �ش�.
        // 1. �����ȿ� ���͸� Ȯ���Ѵ�.
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach(Collider collider in colliders)
        {
            // 2. ���� ���� �ִ��� Ȯ���Ѵ�.
            Vector3 source = transform.position;
            source.y = 0;
            Vector3 destination = collider.transform.position;
            destination.y = 0;


            Vector3 targetDir = (destination - source).normalized;
            float targetAngle = Vector3.Angle(transform.forward, targetDir);
            if (targetAngle > angle * 0.5f)
                continue;

            Debug.Log(collider.gameObject.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // �Ÿ� �׸���
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        // ���� �׸���
        Vector3 rightDir = Quaternion.Euler(0, angle * 0.5f, 0) * transform.forward;
        Vector3 leftDir = Quaternion.Euler(0, angle * -0.5f, 0) * transform.forward;

        Gizmos.color = Color.blue;

    }
}
