using System.Collections.Generic;

/// <summary>
/// �۷ι� �÷��̾� ������
/// �����: ������
/// ��� �� ��� �����ÿ�
/// </summary>

[System.Serializable]
public partial class GlobalPlayerData
{
    // �÷��̾� �̸�
    public string playerName;
    // �÷��̾� ����
    public int maxHp;
    public int attackDamage;
    public int speed;
    public int luck;
    // ���� ��ȭ
    public int coin;
    // �� ���� ���� ���� (Balance, _power, Speed)
    public enum AmWeapon { Balance, Power, Speed }
    // ��¥�� �ð�
    public string saveDateTime;
    // �κ� ���׷��̵� üũ
    // �������� ���� �� ��� Ȯ��
}

