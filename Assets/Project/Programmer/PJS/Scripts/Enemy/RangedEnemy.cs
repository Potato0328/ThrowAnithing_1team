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
        EnemyBullet bulletPool = ObjectPool.GetPool(bulletPrefab, muzzle.position, muzzle.rotation);
        bulletPool.target = playerObj.Value.transform;
        bulletPool.Speed = bulletSpeed;
        bulletPool.Atk = state.Atk;
        bulletPool.Battle = Battle;
    }
}
