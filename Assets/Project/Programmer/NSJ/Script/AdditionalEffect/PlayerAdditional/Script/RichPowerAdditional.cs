using System;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "RichPower", menuName = "AdditionalEffect/Player/RichPower")]
public class RichPowerAdditional : PlayerAdditional
{
    [Header("��ô�� �� ���ݷ� ������(%)")]
    [SerializeField] private float _increaseDamage;

    private int _increaseDamageAmount;
    IDisposable _disposable;
    public override void Enter()
    {
       _disposable = Model.CurThrowCountSubject
            .DistinctUntilChanged()
            .Subscribe(x => { ChangeDamage(x); });

        ChangeDamage(Model.CurThrowables);
    }
    public override void Exit()
    {
        _disposable.Dispose();
        Model.AttackPower -= _increaseDamageAmount;
    }

    private void ChangeDamage(int count)
    {
        // �� ���Ĩ���� ���� ������ ���ݷ� ����
        Model.AttackPower =  (Model.AttackPower - (int)Model.Data.EquipStatus.Damage) - _increaseDamageAmount;
        // ���� ���ݷ¿��� ��ô�� �Ѱ��� ���ݷ� ������ ���
        float attackPowerPerObject = (Model.AttackPower * _increaseDamage / 100);
        // �Ѱ��� ���ݷ� * ��ô�� ������ŭ ���ݷ¿� �߰�
        _increaseDamageAmount = (int)(attackPowerPerObject * count);
        Model.AttackPower = (Model.AttackPower - (int)Model.Data.EquipStatus.Damage) + _increaseDamageAmount;
    }
}
