using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ExplosionThrow", menuName = "AdditionalEffect/Throw/ExplosionThrow")]
public class ExplosionThrowAdditonal : ThrowAdditional
{
    [System.Serializable]
    struct EffectStrcut
    {
        public GameObject EffectPrefab;
        [HideInInspector] public GameObject Effect;
        public float EffectDuration;
    }
    [SerializeField] EffectStrcut _effect;
   

    [Header("���� ����")]
    [SerializeField] private float _explosionRange;
    [Header("���� ������(%)")]
    [SerializeField] private float _explosionDamage;

    private int damage => (int)(_throwObject.Damage * _explosionDamage/ 100);

    private ExplotionThrowEffectPool _pool;
    public override void Enter()
    {
        _pool = Player.CreateNewPool<ExplotionThrowEffectPool>(_effect.EffectPrefab ,nameof(ExplotionThrowEffectPool));
    }


    public override void Exit()
    {
        if (_throwObject.CanAttack == false)
            return;

        Explosion();
    }

    private void Explosion()
    {
        // ����� ��(�̹� ���� ��) Ž��
        TargetInfo nearTarget = FindNearTarget();

        int hitCount = Physics.OverlapSphereNonAlloc(_throwObject.transform.position, _explosionRange, Player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++) 
        {
            Transform target = Player.OverLapColliders[i].transform;
            // ���� ���� �� �ȶ���
            if (target == nearTarget.transform)
                continue;
            Battle.TargetAttack(target, damage, false);
        }

        ShowEffect();
    }

    private void ShowEffect()
    {
        CoroutineHandler.StartRoutine(ShowEffectRoutine());
    }

    IEnumerator ShowEffectRoutine()
    {
        _effect.Effect = _pool.GetPool(_throwObject.transform.position, _throwObject.transform.rotation);
        yield return _effect.EffectDuration.GetDelay();
        _pool.ReturnPool(_effect.Effect);
    }
}
