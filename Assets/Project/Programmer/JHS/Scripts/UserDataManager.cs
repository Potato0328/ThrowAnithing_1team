using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Zenject;

[System.Serializable]
public class UserDataManager : MonoBehaviour
{
    // �÷��̾� ������ ����
    [SerializeField] GlobalGameData nowPlayer;

    [SerializeField] GlobalPlayerStateData playerstate;

    // ���̺� ���� ���� ���
    public string path;
    // ���� ���Թ�ȣ
    public int nowSlot;

    [Inject]
    private void Init(GlobalGameData nowPlayer, GlobalPlayerStateData playerstate)
    {
        this.nowPlayer = nowPlayer;
        this.playerstate = playerstate;
        // �ʱ�ȭ �ڵ� �ۼ�
        path = Application.persistentDataPath + "/save";
        Debug.Log($"Save path: {path}");

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        // ���� ��� �ʱ�ȭ (���̺� ��δ� Zenject���� ���Ե�)
        Debug.Log($"Save path: {path}");
        playerstate.NewPlayerSetting();
    }
    // ���� ���
    public void SaveData()
    {
        // ���� ��¥�� �ð� ����
        nowPlayer.saveDateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        // �����Ϳ� ���� ������ ����
        string data = JsonUtility.ToJson(nowPlayer);
        // �� ���Կ� ���� ���� ��� �ٸ��� ���� (���� ��ȣ�� �´� ���Ϸ� ����)
        string slotPath = path + $"slot_{nowSlot}.json";
        Debug.Log($"{data}");
        File.WriteAllText(slotPath, data);
        Debug.Log($"���� {nowSlot + 1}�� ���� ���� �Ϸ�! ���� �ð�: {nowPlayer.saveDateTime}");
    }
    // �ε� ��� 
    public void LoadData()
    {
        // �� ���Կ� ���� ���� ��� �ٸ��� ���� (���� ��ȣ�� �´� ���Ϸ� �ҷ�����)
        string slotPath = path + $"slot_{nowSlot}.json";
        if (File.Exists(slotPath))
        {
            string data = File.ReadAllText(slotPath);
            // ���� �÷��̾ �ҷ��� ������ ����
            // �ϳ��ϳ� ����
            GlobalGameData loadedData = JsonUtility.FromJson<GlobalGameData>(data);
            nowPlayer.coin = loadedData.coin;
            nowPlayer.saveDateTime = loadedData.saveDateTime;
            nowPlayer.upgradeLevels = loadedData.upgradeLevels;
            nowPlayer.usingCoin = loadedData.usingCoin;
            // ��ȭ �÷��̾� ���� ������ ���� ��
            nowPlayer.bringData = true;
            Debug.Log($"���� {nowSlot + 1}���� ���� �ε� �Ϸ�!");
            Debug.Log($"Checking file path: {slotPath}, Exists: {File.Exists(slotPath)}");
        }
        else
        {
            Debug.LogWarning($"���� {nowSlot + 1}�� ����� �����Ͱ� �����ϴ�.");
        }
    }
    public void DataClear()
    {
        nowPlayer.coin = 0;
        nowPlayer.saveDateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        nowPlayer.upgradeLevels = new int[20];
        nowPlayer.usingCoin = 0;

        // �÷��̾� ���� ����
        playerstate.NewPlayerSetting();
    }
    public void DeleteSlotData(int slotIndex)
    {
        string slotPath = path + $"slot_{slotIndex}.json";
        if (File.Exists(slotPath))
        {
            File.Delete(slotPath); // �ش� ���� ���� ����
            Debug.Log($"���� {slotIndex + 1} ������ ���� �Ϸ�");
        }
        else
        {
            Debug.LogWarning($"���� {slotIndex + 1}�� �����Ͱ� �����ϴ�.");
        }
    }
    public void DeleteData()
    {
        // ��� ���Կ� ���� ������ ����
        for (int i = 0; i < 3; i++) // ���� ���� 3���� ����, ���� ���� �°� ����
        {
            string slotPath = path + $"slot_{i}.json";
            if (File.Exists(slotPath))
            {
                File.Delete(slotPath); // ���� ���� ����
                Debug.Log($"���� {i + 1} ������ ���� �Ϸ�");
            }
        }

        // �÷��̾� ������ �ʱ�ȭ
        DataClear();

        Debug.Log("��� ���� ������ ���� �Ϸ�");
    }
}