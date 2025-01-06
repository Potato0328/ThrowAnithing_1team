using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LobbyUpGradeState", menuName = "Game/LobbyUpGradeState")]
public class LobbyUpGradeState : ScriptableObject
{
    [Header("�κ� ��ȭ ��ġ")]
    [Tooltip("�ٰŸ� ���� 1")]
    public float shortRangeAttackUPO = 1;
    [Tooltip("���Ÿ� ���� 1")]
    public float longRangeAttackUPO = 1;
    [Tooltip("�̵��ӵ� ����")]
    public float movementSpeedUP = 4;
    [Tooltip("�ִ� ü��")]
    public float maxHpUP = 6;  
    public float commonAttackUP = 2;        
    public float attackSpeedUP = 0.05f;   
    public float criticalChanceUP = 2;
    public float defenseUPO = 0.4f;
    public float equipmentDropUpgradeUPO = 2;
    public float drainLifeUP = 0.6f;
    public float maxStaminaUP = 10;
    public float regainManaUP = 0.1f;
    public float manaConsumptionUP = 0.06f;
    public float consumesStaminaUP = 6;
    public float gainMoreThrowablesUP = 20;
    public float maxThrowablesUP = 6;
    public float longRangeAttackUPT = 2;
    public float shortRangeAttackUPT = 2;
    public float defenseUPT = 0.06f;
    public float equipmentDropUpgradeUPT = 3;
}
