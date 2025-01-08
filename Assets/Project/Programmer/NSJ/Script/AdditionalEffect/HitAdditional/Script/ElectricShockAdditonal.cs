using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "AdditionalEffect/Hit/Electric Shock")]
public class ElectricShockAdditonal : HitAdditional
{
    [Range(0, 100)]
    [SerializeField] private float _moveSpeedReduction;
    [Range(0, 100)]
    [SerializeField] private float _attackSpeedReduction;

    private float _decreasedMoveSpeed;
    private float _decreasedAttackSpeed;
    public override void Enter()
    {
        Debug.Log($"{gameObject.name} ����");

       
        if(_debuffRoutine == null)
        {
            _debuffRoutine = CoroutineHandler.StartRoutine(DurationRoutine());

            // ���� �̵��ӵ� ���
            float originMoveSpeed = Battle.Debuff.MoveSpeed;
            Battle.Debuff.MoveSpeed *= 1 - _moveSpeedReduction / 100;
            _decreasedMoveSpeed = originMoveSpeed - Battle.Debuff.MoveSpeed;

            // ���� ���ݼӵ� ���
            float originAttackSpeed = Battle.Debuff.AttackSpeed;
            Battle.Debuff.AttackSpeed *= 1 - _attackSpeedReduction / 100;
            _decreasedAttackSpeed = originAttackSpeed - Battle.Debuff.AttackSpeed;
        }
    }
    public override void Exit()
    {
        if (_debuffRoutine != null) 
        {
            CoroutineHandler.StopRoutine(_debuffRoutine);
            _debuffRoutine = null;
            // ���� �� ��ŭ ����
            Battle.Debuff.MoveSpeed += _decreasedMoveSpeed;
            Battle.Debuff.AttackSpeed += _decreasedAttackSpeed;
        }
    }

    IEnumerator DurationRoutine()
    {
        _remainDuration = Duration;
        while(_remainDuration > 0)
        {
            _remainDuration -= Time.deltaTime;
            yield return null;
        }
        Battle.EndDebuff(this);
    }
}
