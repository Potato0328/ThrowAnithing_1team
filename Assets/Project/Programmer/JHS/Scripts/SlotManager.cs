using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SlotManager : MonoBehaviour
{
    public Button[] continueSlotButtons; // ��Ƽ�� ���� ��ư �迭
    public TextMeshProUGUI[] continueSlotTexts;     // ��Ƽ�� ���� �ؽ�Ʈ �迭

    public Button[] newGameSlotButtons;  // ������ ���� ��ư �迭
    public TextMeshProUGUI[] newGameSlotTexts;      // ������ ���� �ؽ�Ʈ �迭

    private UserDataManager userDataManager;

    private void Start()
    {
        userDataManager = UserDataManager.instance;

        // ���� UI �ʱ�ȭ
        UpdateSlotUI();
    }
    private void UpdateSlotUI()
    {
        // ��Ƽ�� �г� ���� UI ����
        for (int i = 0; i < continueSlotButtons.Length; i++)
        {
            string slotPath = userDataManager.path + i.ToString();
            if (File.Exists(slotPath))
            {
                string data = File.ReadAllText(slotPath);
                GlobalPlayerData playerData = JsonUtility.FromJson<GlobalPlayerData>(data);
                continueSlotTexts[i].text = $"Slot {i + 1}: {playerData.playerName} (Coins: {playerData.coin})";
                newGameSlotTexts[i].text = $"Slot {i + 1}: Data Exists";
            }
            else
            {
                continueSlotTexts[i].text = $"Slot {i + 1}: Empty";
                newGameSlotTexts[i].text = $"Slot {i + 1}: Empty";
            }
        }
    }
    public void OnContinueSlotClicked(int slotIndex)
    {
        string slotPath = userDataManager.path + slotIndex.ToString();
        if (File.Exists(slotPath))
        {
            userDataManager.nowSlot = slotIndex;
            userDataManager.LoadData();
            Debug.Log($"Slot {slotIndex + 1} loaded: {userDataManager.nowPlayer.playerName}");
            // ���� �ε� �� �� ��ȯ �߰� ����
        }
        else
        {
            Debug.Log($"Slot {slotIndex + 1} is empty.");
        }
    }
    public void OnNewGameSlotClicked(int slotIndex)
    {
        string slotPath = userDataManager.path + slotIndex.ToString();
        if (File.Exists(slotPath))
        {
            Debug.Log($"Slot {slotIndex + 1} already has data.");
        }
        else
        {
            userDataManager.nowSlot = slotIndex;
            userDataManager.DataClear();
            userDataManager.SaveData();
            Debug.Log($"New game started in Slot {slotIndex + 1}");
            // �� ���� ���� �� �� ��ȯ �߰� ����
        }
    }
}
