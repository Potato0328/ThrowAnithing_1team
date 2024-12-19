using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
//using Zenject;

public class SlotManager : MonoBehaviour
{
    public Button[] continueSlotButtons; // ��Ƽ�� ���� ��ư �迭
    public TextMeshProUGUI[] continueSlotTexts;     // ��Ƽ�� ���� �ؽ�Ʈ �迭

    public Button[] newGameSlotButtons;  // ������ ���� ��ư �迭
    public TextMeshProUGUI[] newGameSlotTexts;      // ������ ���� �ؽ�Ʈ �迭

    private UserDataManager userDataManager;
    /*
    [Inject]
    public void Construct(UserDataManager userDataManager)
    {
        this.userDataManager = userDataManager;
    }*/

    private void Start()
    {
        userDataManager = UserDataManager.instance;

        // ���� UI �ʱ�ȭ
        UpdateSlotUI();
    }
    private void UpdateSlotUI()
    {
        // ���� UI ����
        for (int i = 0; i < continueSlotButtons.Length; i++)
        {
            string slotPath = userDataManager.path + $"slot_{i}.json"; // �� ���Ը��� ���� ��� �ٸ��� ����
            if (File.Exists(slotPath))
            {
                string data = File.ReadAllText(slotPath);
                GlobalPlayerData playerData = JsonUtility.FromJson<GlobalPlayerData>(data);

                // ����� �ð� ǥ��
                string saveTime = string.IsNullOrEmpty(playerData.saveDateTime) ? "No Date" : playerData.saveDateTime;

                continueSlotTexts[i].text = $"Last Saved: {saveTime}";
                newGameSlotTexts[i].text = $"Last Saved: {saveTime}";
            }
            else
            {
                continueSlotTexts[i].text = $"Slot {i + 1}: Empty";
                newGameSlotTexts[i].text = $"Slot {i + 1}: Empty";
            }
        }
    }
    // ��Ƽ�� ���� ��ư
    public void OnContinueSlotClicked(int slotIndex)
    {
        string slotPath = userDataManager.path + $"slot_{slotIndex}.json"; // �� ���Ը��� ���� ��� �ٸ��� ����
        if (File.Exists(slotPath))
        {
            userDataManager.nowSlot = slotIndex;  // ���� ��ȣ ����
            userDataManager.LoadData(); // �����͸� �ε��ϰ�
            Debug.Log($"Slot {slotIndex + 1} ������ �ҷ�����");
            // ���� �ε� �� �� ��ȯ �߰� ����
            UpdateSlotUI();
        }
        else
        {
            Debug.Log($"Slot {slotIndex + 1}�� �����Ͱ� �����ϴ�");
        }
    }
    // �� ���� ��ư 
    public void OnNewGameSlotClicked(int slotIndex)
    {
        string slotPath = userDataManager.path + $"slot_{slotIndex}.json"; // �� ���Ը��� ���� ��� �ٸ��� ����
        if (File.Exists(slotPath))
        {
            Debug.Log("������ ����");
        }
        else
        {
            userDataManager.nowSlot = slotIndex;  // ���� ��ȣ ����
            userDataManager.DataClear(); // ������ �ʱ�ȭ (nowSlot�� �������� ����)
            userDataManager.SaveData(); // �� ���� ����
            Debug.Log($"�� ���� ���� {slotIndex + 1}");
            // �� ���� ���� �� �� ��ȯ �߰� ����
            UpdateSlotUI();
        }
    }

    //������ ���� �Լ�
    public void DeleteButtonClicked()
    {
        userDataManager.DeleteData(); // ��� ������ ����
        UpdateSlotUI(); // ���� UI ����
        Debug.Log("��� ���� �����Ͱ� �����Ǿ����ϴ�.");
    }
}
