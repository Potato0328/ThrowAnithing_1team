using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    [SerializeField] private List<HitAdditional> _hitAdditionals = new List<HitAdditional>();

    private Rigidbody _rb;
    private int _damage;
    private float _radius;

    private Collider[] _overlapCollider = new Collider[20];

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 4)
        {
            HitTarget();
        }
    }

    public void Init(PlayerModel.ThrowStruct throwStruct, List<HitAdditional> hitAdditionals)
    {
        _damage = throwStruct.Damage;
        _radius = throwStruct.BoomRadius;
        AddHitAdditional(hitAdditionals);
    }

    public void Shoot()
    {
        _rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
    }

    private void HitTarget()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _radius, _overlapCollider, 1 << 4);
        if (hitCount > 0)
        {
            for (int i = 0; i < hitCount; i++)
            {
                NSJMonster monster = _overlapCollider[i].gameObject.GetComponent<NSJMonster>();
                // ����� �ֱ�
                foreach (HitAdditional hitAdditional in _hitAdditionals)
                {
                    HitAdditional cloneDebuff = Instantiate(hitAdditional);
                    cloneDebuff.Origin = hitAdditional;
                    cloneDebuff.Init(_damage);
                    monster.AddDebuff(cloneDebuff);
                }
            }
        }
        Destroy(gameObject);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    private void AddHitAdditional(List<HitAdditional> hitAdditionals)
    {
        foreach (HitAdditional hitAdditional in hitAdditionals)
        {
            int index = _hitAdditionals.FindIndex(origin => origin.Origin.Equals(hitAdditional.Origin));
            if (index >= _hitAdditionals.Count)
                return;

            if(index == -1)
            {
                _hitAdditionals.Add(hitAdditional);
            }
        }
    }

}
