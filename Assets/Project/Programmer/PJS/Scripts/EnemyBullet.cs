using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [HideInInspector] public BattleSystem Battle;
    public int Atk;     // ���ݷ�

    private Rigidbody rigid;
    private float speed;    // ��ô ���ǵ�

    public float Speed { set { speed = value; } }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(DestroyRoutine());
    }

    private void FixedUpdate()
    {
        rigid.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == Tag.Player)
        {
            Battle.TargetAttack(other.transform, Atk, false);
            ObjectPool.ReturnPool(this);
        }

        ObjectPool.ReturnPool(this);
    }

    IEnumerator DestroyRoutine()
    {
        yield return 5f.GetDelay();
        ObjectPool.ReturnPool(this);
    }
}
