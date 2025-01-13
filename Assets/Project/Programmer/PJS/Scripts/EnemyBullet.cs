using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [HideInInspector] public BattleSystem Battle;
    [HideInInspector] public Transform target;    // �÷��̾�
    public int Atk;     // ���ݷ�

    private Rigidbody rigid;
    private float speed;    // ��ô ���ǵ�

    public float Speed { set { speed = value; } }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rigid.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == Tag.Player)
        {
            Battle.TargetAttack(other.transform, Atk, true);
            ObjectPool.ReturnPool(this);
        }

        ObjectPool.ReturnPool(this);
    }
}
