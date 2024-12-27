using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Zenject;
using UnityEngine.SceneManagement;

public class SlotManager : MonoBehaviour
{
    public Button[] continueSlotButtons; // ��Ƽ�� ���� ��ư �迭
    public TextMeshProUGUI[] continueSlotTexts;     // ��Ƽ�� ���� �ؽ�Ʈ �迭

    public Button[] newGameSlotButtons;  // ������ ���� ��ư �迭
    public TextMeshProUGUI[] newGameSlotTexts;      // ������ ���� �ؽ�Ʈ �迭
    [Inject]
    private UserDataManager userDataManager;
    [Inject]
    private GlobalGameData globalPlayerData;
    [Inject]
    private LobbyUpGrade lobbyUpGrade;

    public GameObject confirmDeleteUI; // Ȯ�� UI
    public Button confirmButton; // Ȯ�� ��ư
    public Button cancelButton; // ��� ��ư

    private int selectedSlotIndex; // ���õ� ���� �ε���

    private void Start()
    {
        // ���� UI �ʱ�ȭ
        UpdateSlotUI();

        // Ȯ��/��� ��ư �̺�Ʈ ����
        confirmButton.onClick.AddListener(OnConfirmDelete);
        cancelButton.onClick.AddListener(OnCancelDelete);

        // UI �ʱ� ��Ȱ��ȭ
        confirmDeleteUI.SetActive(false);
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
                GlobalGameData playerData = JsonUtility.FromJson<GlobalGameData>(data);

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

            // �κ� ��ȭ ���� ���� (���߿� �ε� â���� ����)
            lobbyUpGrade.ApplyUpgradeStats();

            UpdateSlotUI();
            SceneManager.LoadScene("LobbyTestScene");
        }
        else
        {
            Debug.Log($"Slot {slotIndex + 1}�� �����Ͱ� �����ϴ�");
        }
    }
    // �� ���� ��ư 
    public void OnNewGameSlotClicked(int slotIndex)
    {
        // �� ���Ը��� ���� ��� �ٸ��� ����
        string slotPath = userDataManager.path + $"slot_{slotIndex}.json";
        selectedSlotIndex = slotIndex; // ���õ� ���� �ε��� ����
        if (File.Exists(slotPath))
        {
            // �����Ͱ� �ִ� ��� Ȯ�� UI Ȱ��ȭ
            confirmDeleteUI.SetActive(true);
        }
        else
        {
            // �����Ͱ� ���� ��� �ٷ� �� ���� ����
            StartNewGame(slotIndex);
        }
    }
    private void StartNewGame(int slotIndex)
    {
        userDataManager.nowSlot = slotIndex; // ���� ��ȣ ����
        userDataManager.DataClear(); // ������ �ʱ�ȭ
        userDataManager.SaveData(); // �� ���� ����
        Debug.Log($"�� ���� ���� {slotIndex + 1}");
        UpdateSlotUI(); // ���� UI ����
        SceneManager.LoadScene("LobbyTestScene");
    }

    private void OnConfirmDelete()
    {
        string slotPath = userDataManager.path + $"slot_{selectedSlotIndex}.json";
        if (File.Exists(slotPath))
        {
            File.Delete(slotPath); // ���� ������ ����
            Debug.Log($"���� {selectedSlotIndex + 1} ������ ���� �Ϸ�");
        }

        // �� ���� ����
        StartNewGame(selectedSlotIndex);

        // Ȯ�� UI ��Ȱ��ȭ
        confirmDeleteUI.SetActive(false);
    }
    // Ȯ�� UI ��� ��ư 
    private void OnCancelDelete()
    {
        // Ȯ�� UI ��Ȱ��ȭ
        confirmDeleteUI.SetActive(false);
    }
    // ���� ���� �Լ�
    public void DeleteButtonClicked()
    {
        userDataManager.DeleteData(); // ��� ������ ����
        UpdateSlotUI(); // ���� UI ����
        Debug.Log("��� ���� �����Ͱ� �����Ǿ����ϴ�.");
    }
}
