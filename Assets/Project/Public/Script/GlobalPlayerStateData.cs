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

    // �÷��̾� ����
    // �ִ� ü�� 
    public int maxHp;
    // �ٰŸ� ���ݷ�
    public int shortRangeAttack;
    // ���Ÿ� ���ݷ�
    public int longRangeAttack;
    // ���� �ӵ�
    public int attackSpeed;
    // �̵� �ӵ�
    public int movementSpeed;
    // ũ��Ƽ�� Ȯ��
    public int criticalChance;
    // ���� 
    public int defense;
    // ��� ȹ�� Ȯ��
    public int equipmentDrop;
    // ����� ���
    public int luck;
    //  ���� �̼� ũȮ ���� ü�� ��� ȹ��
    // ����� ��� ���׹̳� �ִ�ġ ���� ���� ȸ�� ���� 
    // ���� ȸ�� ���� ���� �Ҹ� ���� ���� �Ҹ� ����
    // ��ô�� �߰� ȹ�� Ȯ�� ����, ���� ��ô�� ���� ����
    // �� ���� ���� ���� (Balance, _power, Speed)
    public enum AmWeapon { Balance, Power, Speed }
}
