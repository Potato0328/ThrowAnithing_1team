using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    [SerializeField] EnemyBullet bulletPrefab;
    [SerializeField] Transform muzzle;
    [Header("����ü �ӵ�")]
    [SerializeField] float bulletSpeed;

    public float BulletSpeed { get { return bulletSpeed; } }

    public void Attack()
    {
        EnemyBullet bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation).GetComponent< EnemyBullet>();
        bullet.Speed = bulletSpeed;
        bullet.transform.parent = transform;
    }
}
