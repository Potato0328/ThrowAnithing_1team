using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "TheortOfRelativity", menuName = "AdditionalEffect/Player/TheortOfRelativity")]
public class TheortOfRelativityAdditional : PlayerAdditional
{
    [System.Serializable]
    struct EffectStrcut
    {
        public GameObject EffectPrefab;
        [HideInInspector] public GameObject Effect;
        public float EffectDuration;
    }
    [SerializeField] EffectStrcut _effect;

    [SerializeField] private HitAdditional _slow;
    [Header("Ȯ��(%)")]
    [Range(0, 100)]
    [SerializeField] private float _probability;
    [Header("���ݼӵ� ������(%)")]
    [SerializeField] private float _increaseAttackSpeed;
    [Header("�̵��ӵ� ������(%)")]
    [SerializeField] private float _increaseMoveSpeed;
    [Header("���� �ð�")]
    [SerializeField] private float _buffDuration;
    [Header("���ο� ����")]
    [SerializeField] private float _slowRange;
    [Header("�� �̼Ӱ��ҷ�")]
    [SerializeField] private float _enemySlowAmount;
    [Header("����� �ð�")]
    [SerializeField] private float _slowDuration;


    private bool _isBuff;
    Coroutine _buffRoutine;

    public override void Exit()
    {
        // ���� �ÿ� ���� ����
        if (_buffRoutine != null)
        {
            CoroutineHandler.StopRoutine(_buffRoutine);
            _buffRoutine = null;
        }
        if (_isBuff ==true)
        {
            Model.AttackSpeedMultiplier -= _increaseAttackSpeed;
            Model.MoveSpeedMultyplier -= _increaseMoveSpeed;
        }
    }

    public override void Trigger()
    {
        if (CurState != PlayerController.State.SpecialAttack)
            return;

        if (Random.Range(0, 100) < _probability)
        {
            Process();
        }
    }

    private void Process()
    {
        ProcessPlayerBuff();
        ProcessEnemyDebuff();
    }

    private void ProcessPlayerBuff()
    {
        if (_buffRoutine != null)
        {
            CoroutineHandler.StopRoutine( _buffRoutine );
            _buffRoutine = null;
        }

        if (_buffRoutine == null)
            _buffRoutine = CoroutineHandler.StartRoutine(BuffRoutine());
    }

    IEnumerator BuffRoutine()
    {
        // ���� �ߺ� ����
        if (_isBuff == false)
        {
            _isBuff = true;

            Model.AttackSpeedMultiplier += _increaseAttackSpeed;
            Model.MoveSpeedMultyplier += _increaseMoveSpeed;
        }

        yield return _buffDuration.GetDelay();
        // �÷��̾� ���� ��
        Model.AttackSpeedMultiplier -= _increaseAttackSpeed;
        Model.MoveSpeedMultyplier -= _increaseMoveSpeed;


        _isBuff = false;
        _buffRoutine = null;
    }

    private void ProcessEnemyDebuff()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _slowRange, Player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++)
        {
            // TODO: ���ο� ��� �߰�
            
        }

        CreateSlowFieldEffect();
    }

    private void CreateSlowFieldEffect()
    {
        _effect.Effect = Instantiate(_effect.EffectPrefab, transform);

        Vector3 effectScale = _effect.Effect.transform.localScale;
        _effect.Effect.transform.localScale = new Vector3(_slowRange * 2, effectScale.y, _slowRange * 2);

        Destroy(_effect.Effect, _effect.EffectDuration);
    }
}
