using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Power JumpDown", menuName = "Arm/AttackType/Power/JumpDown")]
public class PowerJumpDown : ArmJumpDown
{
    public override void Enter()
    {
        Debug.Log("���� �ٿ�");
    }
}
