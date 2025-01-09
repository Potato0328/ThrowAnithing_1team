using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BalanceArm" ,menuName = "Arm/Balance")]
public class BalanceArm : ArmUnit
{
    [Header("�뽬 ���׹̳�(%)-�Ŀ���尡 �⺻")]
    [SerializeField] private float _dashStamina;
    public override void Init(PlayerController player)
    {
        base.Init(player);
        Model.DashStamina = (int)(Model.GlobalStateData.dashConsumesStamina * _dashStamina/100f);
    }
}
