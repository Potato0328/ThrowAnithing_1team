using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slow", menuName = "AdditionalEffect/Hit/Slow")]
public class SlowAddtional : HitAdditional
{
    [System.Serializable]
    struct EffectStrcut
    {
        public GameObject EffectPrefab;
        [HideInInspector] public GameObject Effect;
    }
    [Header("�̼� ���ҷ�(%)")]
    [SerializeField] public float SlowAmount;
    [SerializeField] EffectStrcut _effect;


    private float _decreaseMoveSpeedEnemyValue;
    public override void Enter()
    {
        if (_debuffRoutine == null)
        {
            _debuffRoutine = CoroutineHandler.StartRoutine(DurationRoutine());
            CreateEffect();
            ChangeValue(true);
        }
    }
    public override void Exit()
    {
        if (_debuffRoutine != null)
        {
            CoroutineHandler.StopRoutine(_debuffRoutine);
            _debuffRoutine = null;
            // ���� �� ��ŭ ����
            ChangeValue(false);
            ObjectPool.ReturnPool(_effect.Effect);
        }
    }

    IEnumerator DurationRoutine()
    {
        Debug.Log($"{transform.name} , ���ο� ��");
        _remainDuration = Duration;
        while (_remainDuration > 0)
        {
            _remainDuration -= Time.deltaTime;
            yield return null;
        }
        Battle.EndDebuff(this);
    }

    private void ChangeValue(bool isDecrease)
    {
        if (_targetType == TargetType.Player)
            ChangePlayerValue(isDecrease);
        else if (_targetType == TargetType.Enemy)
            ChangeEnemyValue(isDecrease);
    }

    private void ChangePlayerValue(bool isDecrease)
    {
        if (transform.tag != Tag.Player)
            return;

        // �÷��̾� ���� ���� ���
        if (isDecrease == true)
        {
            Player.Model.MoveSpeedMultyplier -= SlowAmount;
        }
        // �÷��̾� ���� ����
        else
        {
            Player.Model.MoveSpeedMultyplier += SlowAmount;
        }
    }

    private void ChangeEnemyValue(bool isDecrease)
    {
        // ���� ���� ���� ���
        if (isDecrease == true)
        {
            // �̼Ӱ���
            float enemyMoveSpeed = GetEnemyMoveSpeed();
            _decreaseMoveSpeedEnemyValue = enemyMoveSpeed * SlowAmount / 100f;
            enemyMoveSpeed -= _decreaseMoveSpeedEnemyValue;
            SetEnemyMoveSpeed(enemyMoveSpeed);

        }
        // ���� ���� ����
        else
        {
            // �̼� ����
            float enemyMoveSpeed = GetEnemyMoveSpeed();
            enemyMoveSpeed += _decreaseMoveSpeedEnemyValue;
            SetEnemyMoveSpeed(enemyMoveSpeed);
        }
    }


    private void CreateEffect()
    {
        _effect.Effect = ObjectPool.GetPool(_effect.EffectPrefab, transform);
    }
}
