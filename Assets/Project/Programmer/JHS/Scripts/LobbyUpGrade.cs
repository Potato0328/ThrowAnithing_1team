using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
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
            for (int i = 0; i < 3; i++)
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
            for (int i = 0; i < 4; i++)
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
            playerState.movementSpeed += 4;
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
            playerState.attackSpeed += 0.05f;
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
    public void ThreeLine_UpgradeCommonAttack(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���� ���ݷ� 2����
            playerState.commonAttack += 2;
            Debug.Log($"���� ���ݷ�: {playerState.commonAttack}");
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // ����° �� 2�� ���� ���� ��ô�� 6����
    public void ThreeLine_UpgradeMaxThrowables(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���� ��ô�� 6����
            playerState.maxThrowables += 6;
            Debug.Log($"���������� ��ô��: {playerState.maxThrowables}");
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // ����° �� 3�� ���� ���� 0.4 ����
    public void ThreeLine_UpgradeDefense(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���� 0.4 ����
            playerState.defense += 0.4f;
            Debug.Log($"����: {playerState.defense}");
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // ����° �� 4�� ���� ���� ȸ���� 10�� ����
    public void ThreeLine_UpgradeRegainMana(int slot)
    {
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���� ȸ���� 10�� ���� (������)
            for (int i = 0; i < 4; i++)
            {
                playerState.regainMana[i] += playerState.regainMana[i]*0.1f;
            }
            Debug.Log($"���� ȸ���� 10�� ����");
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    /**************************************************************/

    // �׹�° �� 1�� ���� ���׹̳� �Ҹ� 6�� ����
    public void FourLine_UpgradeConsumesStamina(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���׹̳� �Ҹ� 6�� ����
            playerState.consumesStamina = 6;
            float reduction = playerState.consumesStamina / 100f;
            // �����Ҹ� ���׹̳�
            playerState.jumpConsumesStamina -= playerState.jumpConsumesStamina * reduction;
            // ���� ���� �Ҹ� ���׹̳�
            playerState.doubleJumpConsumesStamina -= playerState.doubleJumpConsumesStamina * reduction;
            // �뽬 �Ҹ� ���׹̳�
            playerState.dashConsumesStamina -= playerState.dashConsumesStamina * reduction;
            // ���� ���� ���׹̳�
            for (int i = 0; i < playerState.shortRangeAttackStamina.Length; i++)
            {
                playerState.shortRangeAttackStamina[i] -= playerState.shortRangeAttackStamina[i] * reduction;
            }

            Debug.Log($"��ü ���׹̳� �Ҹ� 6�� ����");
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // �׹�° �� 2�� ���� ���Ÿ� ���ݷ� 2����
    public void FourLine_UpgradeLongAttack(int slot)
    {
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���Ÿ� ���� ��ȭ
            for (int i = 0; i < 4; i++)
            {
                playerState.longRangeAttack[i] += 2;
            }
            Debug.Log($"���Ÿ� ���� 2 ����");
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // �׹�° �� 3�� ���� �ٰŸ� ���ݷ� 2����
    public void FourLine_UpgradeShortAttack(int slot)
    {
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ���� ���� ��ȭ
            for (int i = 0; i < 3; i++)
            {
                playerState.shortRangeAttack[i] += 2;
            }
            Debug.Log($"���� ���� 2 ����");
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // �׹�° �� 4�� ���� ��ô�� �߰� ȹ�� 20�� ����
    public void FourLine_UpgradeMaxThrowables(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� ��ô�� �߰� ȹ�� 20�� ����
            playerState.maxThrowables += 10;
            Debug.Log($"��ô�� ����: {playerState.maxThrowables}");
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    /**************************************************************/

    // �ټ���° �� 1�� ���� ���� �Ҹ� ���� 6��
    // �ټ���° �� 2�� ���� ����� ��� 0.6�� 
    // �ټ���° �� 3�� ���� ���� 0.6 ����
    // �ټ���° �� 4�� ���� ��� ȹ�� Ȯ�� 3�� ����
}
