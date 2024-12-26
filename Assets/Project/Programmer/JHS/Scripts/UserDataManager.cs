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
    [Inject]
    GlobalPlayerStateData playerstate;

    // ���̺� ���� ���� ���
    public string path;
    // ���� ���Թ�ȣ
    public int nowSlot;

    [Inject]
    private void Init(GlobalGameData nowPlayer)
    {
        this.nowPlayer = nowPlayer;
        // �ʱ�ȭ �ڵ� �ۼ�
        path = Application.persistentDataPath + "/save";
        Debug.Log($"Save path: {path}");

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        // ���� ��� �ʱ�ȭ (���̺� ��δ� Zenject���� ���Ե�)
        Debug.Log($"Save path: {path}");
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
            nowPlayer = JsonUtility.FromJson<GlobalGameData>(data);
            UpdatePlayerStats();
            Debug.Log($"���� {nowSlot + 1}���� ���� �ε� �Ϸ�!");
        }
        else
        {
            Debug.LogWarning($"���� {nowSlot + 1}�� ����� �����Ͱ� �����ϴ�.");
        }
    }
    public void DataClear()
    {
        nowPlayer = new GlobalGameData
        {
            coin = 0,
            saveDateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            upgradeLevels = new int[20]
        };
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

    public void UpdatePlayerStats()
    {
        
    }
}