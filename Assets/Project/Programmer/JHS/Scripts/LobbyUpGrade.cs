using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LobbyUpGrade : MonoBehaviour
{
    // GlobalGameData�� BuyUpgradeSlot���� �� ������ �޾ƿ� ���ϰ�� ����  ���׷��̵� 
    [Inject]
    public GlobalGameData gameData;
    [Inject]
    public GlobalPlayerStateData playerState;

    // ù��° �� 1�� ���� ���� ���� ��ȭ
    public void OneLine_UpgradeShortAttack(int slot)
    {
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���� ���� ��ȭ
            for (int i = 0; i < 3;)
            {
                playerState.shortRangeAttack[i] += 1;
            }            
            Debug.Log($"���� ���� ����");
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // ù��° �� 2�� ���� ���Ÿ� ���� ��ȭ
    public void OneLine_UpgradeLongAttack(int slot)
    {
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���Ÿ� ���� ��ȭ
            for (int i = 0; i < 4;)
            {
                playerState.longRangeAttack[i] += 1;
            }
            Debug.Log($"���Ÿ� ���� ����");
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // ù��° �� 3�� ���� �̵� �ӵ� ��ȭ 
    public void OneLine_UpgradeMovementSpeed(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� �̵� �ӵ� ����
            playerState.movementSpeed *= 1.04f;
            Debug.Log($"�̵� �ӵ�: {playerState.movementSpeed}");
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // ù��° �� 4�� ���� �ִ� ü�� ��ȭ
    public void OneLine_UpgradeMaxHpSlot(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� �ִ� ü�� ����
            playerState.maxHp += 6;
            Debug.Log($"�ִ� ü��: {playerState.maxHp}");
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }

    /**************************************************************/

    // �ι�° �� 1�� ���� ���׹̳� �ִ�ġ ����
    public void TwoLine_UpgradeMaxStamina(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���׹̳� �ִ�ġ ����
            playerState.maxStamina += 10;
            Debug.Log($"���׹̳� �ִ�ġ : {playerState.maxStamina}");
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // �ι�° �� 2�� ���� ���� �ӵ� 5�� ����
    public void TwoLine_UpgradeAttackSpeed(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���� �ӵ� 5�� ����
            playerState.attackSpeed *= 1.05f;
            Debug.Log($"���� �ӵ�: {playerState.attackSpeed}");
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // �ι�° �� 3�� ���� ũ��Ƽ�� Ȯ�� 2 ����
    public void TwoLine_UpgradeCriticalChance(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ũ��Ƽ�� Ȯ�� 2 ����
            playerState.criticalChance += 2;
            Debug.Log($"ũ��Ƽ�� Ȯ�� : {playerState.criticalChance}");
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // �ι�° �� 4�� ���� �߰� ��� ȹ�� Ȯ�� 2�� ����
    public void TwoLine_UpgradeEquipmentDrop(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� �߰� ��� ȹ�� Ȯ�� 2�� ����
            playerState.equipmentDropUpgrade += 2;
            Debug.Log($"�߰� ��� ȹ�� Ȯ�� : {playerState.equipmentDropUpgrade}");
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    /**************************************************************/

    // ����° �� 1�� ���� ���� ���ݷ� 2����
    // ����° �� 2�� ���� ���� ��ô�� 6����
    // ����° �� 3�� ���� ���� 0.4 ����
    // ����° �� 4�� ���� ���� ȸ���� 10�� ����

    /**************************************************************/

    // �׹�° �� 1�� ���� ���׹̳� �Ҹ� 6�� ����
    // �׹�° �� 2�� ���� ���Ÿ� ���ݷ� 2����
    // �׹�° �� 3�� ���� �ٰŸ� ���ݷ� 2����
    // �׹�° �� 4�� ���� ��ô�� �߰� ȹ�� 20�� ����

    /**************************************************************/

    // �ټ���° �� 1�� ���� ���� �Ҹ� ���� 6��
    // �ټ���° �� 2�� ���� ����� ��� 0.6�� 
    // �ټ���° �� 3�� ���� ���� 0.6 ����
    // �ټ���° �� 4�� ���� ��� ȹ�� Ȯ�� 3�� ����
}
