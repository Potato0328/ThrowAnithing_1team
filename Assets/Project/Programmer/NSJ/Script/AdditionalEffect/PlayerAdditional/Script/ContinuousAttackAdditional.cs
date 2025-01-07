using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ContinuousAttack", menuName = "AdditionalEffect/Player/ContinuousAttack")]
public class ContinuousAttackAdditional : PlayerAdditional
{
    [Header("���ݷ� ������")]
    [SerializeField] private int _attackPower;
    [Header("���ӽð�")]
    [SerializeField] private float _duration;

    private bool _isUseSpecial;
    private bool _isTimerStart;
    Coroutine _increaseAttackPowerRoutine;

    public override void Enter()
    {
        if (_increaseAttackPowerRoutine == null)
            _increaseAttackPowerRoutine = CoroutineHandler.StartRoutine(IncreaseAttackPowerRoutine());
    }

    public override void Exit()
    {
        if(_increaseAttackPowerRoutine != null)
        {
            CoroutineHandler.StopRoutine(_increaseAttackPowerRoutine);
            _increaseAttackPowerRoutine = null;
        }

        // Ÿ�̸� ���ư��� ���϶� (���ݷ� ���� ����)
        if(_isTimerStart == true)
        {
            // �ٽ� ���ݷ� ����
            Model.AttackPower = GetPlayerAttackPower(-_attackPower);
        }
    }

    public override void Trigger()
    {
        // ��ų ���
        if (CurState != PlayerController.State.SpecialAttack)
            return;

        _isUseSpecial = true;
    }
    IEnumerator IncreaseAttackPowerRoutine()
    {
        _isTimerStart = false;
        float timer = _duration;
        while (true)
        {
            // ��ų ��� �� Ÿ�̸� 5�� �� ����
            if (_isUseSpecial)
            {
                _isUseSpecial = false;

                timer = _duration;
                _isTimerStart = true;
                Model.AttackPower = GetPlayerAttackPower(_attackPower);
            }

            timer -= Time.deltaTime;
            if(timer < 0 && _isTimerStart == true)
            {
                _isTimerStart = false;

                // �ٽ� ���ݷ� ����
                Model.AttackPower = GetPlayerAttackPower(-_attackPower);
            }
            yield return null;
        }
    }
}
