using UnityEngine;

public class RangedEnemy : BaseEnemy
{
    [Header("�ǰ� ��� ��Ÿ��")]
    [SerializeField] float hitCoolTime;
    [Header("����ü �ӵ�")]
    [SerializeField] float bulletSpeed;
    [Header("����ü ����")]
    [SerializeField] EnemyBullet bulletPrefab;
    [SerializeField] Transform muzzle;

    public float BulletSpeed { get { return bulletSpeed; } }


    private void Start()
    {
        BaseInit();
        tree.SetVariableValue("HitCoolTime", hitCoolTime);
    }


    public void Attack()
    {
        EnemyBullet bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation).GetComponent< EnemyBullet>();
        bullet.target = playerObj.Value.transform;
        bullet.Speed = bulletSpeed;
        bullet.Atk = state.Atk;
        bullet.transform.parent = transform;
        bullet.Battle = Battle;
    }
}
