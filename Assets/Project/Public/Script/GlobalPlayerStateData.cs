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
    public float[] shortRangeAttack = new float[3];
    // ���Ÿ� ���ݷ� �⺻ : 1Ÿ 10 2Ÿ 40 3Ÿ 70 4Ÿ 110
    public float[] longRangeAttack = new float[4];
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
    // ���׹̳� �ִ�ġ �⺻ : 100
    public float maxStamina;
    // ���׹̳� ȸ�� �⺻ : 20
    public float regainStamina;
    // ������ ���ݴ� ���� ȸ�� �⺻ : 1Ÿ 3 2Ÿ 8 3Ÿ 13 4Ÿ 20
    public float[] regainMana = new float[4];
    // ���� �Ҹ� ���� �⺻ : 1Ÿ 30 2Ÿ 70 3Ÿ 100
    public float[] manaConsumption = new float[3];
    // ���׹̳� �Ҹ� ���� �⺻ : 0 ��� ���׹̳� �Ҹ� ����
    public float consumesStamina;
    // ��ô�� �߰� ȹ�� Ȯ�� ���� �⺻ : 0
    public float gainMoreThrowables;
    // ���� ��ô�� ���� ���� / �⺻ 50
    public float maxThrowables;
    // �� ���� ���� ���� (Balance, _power, MoveSpeed)
    public enum AmWeapon { Balance, Power, Speed }
    public AmWeapon nowWeapon;
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
    public float[] shortRangeAttackStamina = new float[3];
    // Ư�� ���ݷ� ��ġ �⺻ : 1Ÿ 75 2Ÿ 150 3Ÿ 225
    public float[] specialAttack = new float[3];
    public void NewPlayerSetting()
    {
        maxHp = 60;
        commonAttack = 0;
        shortRangeAttack[0] = 25;
        shortRangeAttack[1] = 40;
        shortRangeAttack[2] = 60;
        longRangeAttack[0] = 10;
        longRangeAttack[1] = 40;
        longRangeAttack[2] = 70;
        longRangeAttack[3] = 110;
        attackSpeed = 1;
        movementSpeed = 100;
        criticalChance = 10;
        defense = 0;
        equipmentDropUpgrade = 0;
        drainLife = 0;
        maxStamina = 100;
        regainStamina = 20;
        regainMana[0] = 3;
        regainMana[1] = 8;
        regainMana[2] = 13;
        regainMana[3] = 20;
        manaConsumption[0] = 30;
        manaConsumption[1] = 70;
        manaConsumption[2] = 100;
        consumesStamina = 0;
        gainMoreThrowables = 0;
        maxThrowables = 50;
        nowWeapon = AmWeapon.Balance;
        maxMana = 100;
        maxJumpCount = 2;
        jumpPower = 100;
        jumpConsumesStamina = 20;
        doubleJumpConsumesStamina = 10;
        dashDistance = 200;
        dashConsumesStamina = 50;
        shortRangeAttackStamina[0] = 20;
        shortRangeAttackStamina[1] = 30;
        shortRangeAttackStamina[2] = 50;
        specialAttack[0] = 75;
        specialAttack[1] = 150;
        specialAttack[2] = 225;
    }
}
