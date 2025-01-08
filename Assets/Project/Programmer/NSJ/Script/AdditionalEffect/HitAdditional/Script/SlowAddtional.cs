using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Slow", menuName = "AdditionalEffect/Hit/Slow")]
public class SlowAddtional : HitAdditional
{
    [Header("�̼� ���ҷ�(%)")]
    [SerializeField] public float SlowAmount;


    private float _decreaseMoveSpeedEnemyValue;
    public override void Enter()
    {
        if (_debuffRoutine == null)
        {
            _debuffRoutine = CoroutineHandler.StartRoutine(DurationRoutine());

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
        }
    }

    IEnumerator DurationRoutine()
    {
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
            Debug.Log(enemyMoveSpeed);
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
}
