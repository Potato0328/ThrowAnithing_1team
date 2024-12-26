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

    // ù��° �� 1��° ���� ���� ���� ��ȭ
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
    // ù��° �� 2��° ���� ���Ÿ� ���� ��ȭ
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
    // ù��° �� 3��° ���� �̵� �ӵ� ��ȭ 
    public void OneLine_UpgradeMovementSpeed(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� �̵� �ӵ� ����
            playerState.movementSpeed *= 1.04f;
            Debug.Log($"�̵� �ӵ� ����: {playerState.movementSpeed}");
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }
    // ù��° �� 4��° ���� �ִ� ü�� ��ȭ
    public void OneLine_UpgradeMaxHpSlot(int slot)
    {
        // ���� ��ȭ �õ�
        if (gameData.BuyUpgradeSlot(slot))
        {
            // ��ȭ ���� �� �ִ� ü�� ����
            playerState.maxHp += 6;
            Debug.Log($"�ִ� ü�� ����: {playerState.maxHp}");
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }

    /**************************************************************/

    
}
