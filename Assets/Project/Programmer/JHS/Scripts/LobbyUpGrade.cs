using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUpGrade : MonoBehaviour
{
    // GlobalGameData�� BuyUpgradeSlot���� �� ������ �޾ƿ� ���ϰ�� ����  ���׷��̵� 
    /*public void IncreaseStat(string stat, int amount)
    {
        switch (stat)
        {
            case "HP":
                UserDataManager.instance.nowPlayer.maxHp += amount;
                break;
            case "Speed":
                UserDataManager.instance.nowPlayer.speed += amount;
                break;
            case "Attack":
                UserDataManager.instance.nowPlayer.attackDamage += amount;
                break;
            case "Luck":
                UserDataManager.instance.nowPlayer.luck += amount;
                break;
            case "Coin":
                UserDataManager.instance.nowPlayer.coin += amount;
                break;
            default:
                Debug.LogWarning("Invalid stat type.");
                break;
        }
        UpdateSlotUI.instance.UpdateUI();
    }

    public void HpUpgrade(int amount)
    {
        // ���ұ�
        int payment = 100;
        if (UserDataManager.instance.nowPlayer.maxHp >= payment)
        {
            UserDataManager.instance.nowPlayer.coin -= payment;
            UserDataManager.instance.nowPlayer.maxHp += amount;
            UpdateSlotUI.instance.UpdateUI();
        }
        else
        {
            Debug.Log("coin ����");
        }
    }
    public void SpeedUpgrade(int amount)
    {
        // ���ұ�
        int payment = 100;
        if (UserDataManager.instance.nowPlayer.coin >= payment)
        {
            UserDataManager.instance.nowPlayer.coin -= payment;
            UserDataManager.instance.nowPlayer.speed += amount;
            UpdateSlotUI.instance.UpdateUI();
        }
        else
        {
            Debug.Log("coin ����");
        }
    }
    public void CoinUpgrade(int amount)
    {
        UserDataManager.instance.nowPlayer.coin += amount;
        UpdateSlotUI.instance.UpdateUI();
    }*/
}
