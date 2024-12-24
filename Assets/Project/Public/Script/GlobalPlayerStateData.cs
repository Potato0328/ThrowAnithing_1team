using System;
using System.Collections.Generic;

/// <summary>
/// �۷ι� �÷��̾� ���� ������
/// �����: ������
/// ��� �� ��� �����ÿ�
/// </summary>

[System.Serializable]
public class GlobalPlayerStateData
{
    // GlobalGameData�� �κ� Ư�� ��ȭ �ܰ� Ȯ�� �� �÷��̾��� ���� ����
    // ���� json���� ������ �ʿ� ���� ���׷��̵� �ܰ踦 �����Ͽ� �׿� ���� ���� ����
    // �κ� ���׷��̵� ������ ���� ����
    
    // �κ񿡼� ���۵Ǵ� �÷��̾� ����
    // �ִ� ü�� �⺻ : 60
    public float maxHp;
    // ���� ���ݷ� �⺻ : 0
    public float commonAttack;
    // �ٰŸ� ���ݷ� �⺻ : 1Ÿ 25 2Ÿ 40 3Ÿ 60
    public float shortRangeAttack;
    // ���Ÿ� ���ݷ� �⺻ : 1Ÿ 10 2Ÿ 40 3Ÿ 70 4Ÿ 110
    public float longRangeAttack;
    // ���� �ӵ� �⺻ : 1
    public float attackSpeed;
    // �̵� �ӵ� �⺻ : 100
    public float movementSpeed;
    // ũ��Ƽ�� Ȯ�� �⺻ : 10
    public float criticalChance;
    // ���� �⺻ : 0
    public float defense;
    // ��� ȹ�� Ȯ�� ���� �⺻ 0 ���� �ϰ� ����� Ȯ���� �÷���
    public float equipmentDropUpgrade;
    // ����� ��� �⺻ 0
    public float drainLife;
    // ���׹̳� �ִ�ġ ���� �⺻ : 100
    public float maxStamina;
    // ���׹̳� ȸ�� ���� �⺻ : 20
    public float regainStamina;
    // ������ ���ݴ� ���� ȸ�� ���� �⺻ : 1Ÿ 3 2Ÿ 8 3Ÿ 13 4Ÿ 20
    public float regainMana;
    // ���� �Ҹ� ���� �⺻ : 1Ÿ 30 2Ÿ 70 3Ÿ 100
    public float manaConsumption;
    // ���׹̳� �Ҹ� ���� �⺻ : 0 ��� ���׹̳� �Ҹ� ����
    public float consumesStamina;
    // ��ô�� �߰� ȹ�� Ȯ�� ���� �⺻ : 0
    public float gainMoreThrowables;
    // ���� ��ô�� ���� ���� / �⺻ 50
    public float maxThrowables;
    // �� ���� ���� ���� (Balance, _power, Speed)
    public enum AmWeapon { Balance, Power, Speed }

    // �κ񿡼� ���۵��� �ʴ� �÷��̾� ����

    // �޴� ���� ���� (Ȯ�� �ƴ�)
    // �ִ� ���� �⺻ : 100
    public float maxMana;
    // �ִ� ���� Ƚ�� �⺻ : 2
    public float maxJumpCount;
    // ������ �⺻ : 100
    public float jumpPower;
    // ���� �Ҹ� ���׹̳� �⺻ : 20
    public float jumpConsumesStamina;
    // ���� ���� �Ҹ� ���׹̳� �⺻ : 10
    public float doubleJumpConsumesStamina;
    // �뽬 �Ÿ� �⺻ : 200
    public float dashDistance;
    // �뽬 �Ҹ� ���׹̳� �⺻ : 50
    public float dashConsumesStamina;
    // ���� ���� ���׹̳� �⺻ : 1Ÿ 20 2Ÿ 30 3Ÿ 50
    public float shortRangeAttackStamina;
    // Ư�� ���ݷ� ��ġ �⺻ : 1Ÿ 75 2Ÿ 150 3Ÿ 225
    public float specialAttack;
}
