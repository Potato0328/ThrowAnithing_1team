using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GlobalPlayerData
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
    // �� ���� ���� ���� (Balance, Power, Speed)
    public enum AmWeapon { Balance, Power, Speed }
    // �κ� �� ���׷��̵� ���� ��Ȳ
    public Dictionary<string, int> upgrades;
    // ��¥�� �ð�
    public string saveDateTime;

}


public class UserDataManager : MonoBehaviour
{
    // �̱���
    public static UserDataManager instance;
    // �÷��̾� ������ ����
    public GlobalPlayerData nowPlayer = new GlobalPlayerData();
    // ���̺� ���� ���� ���
    public string path;
    // ���� ���Թ�ȣ
    public int nowSlot;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(instance.gameObject);

        DontDestroyOnLoad(this.gameObject);

        // ���� ��� ����
        path = Application.persistentDataPath + "/save";
        print(path);
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
            nowPlayer = JsonUtility.FromJson<GlobalPlayerData>(data);
            Debug.Log($"���� {nowSlot + 1}���� ���� �ε� �Ϸ�!");
        }
        else
        {
            Debug.LogWarning($"���� {nowSlot + 1}�� ����� �����Ͱ� �����ϴ�.");
        }
    }
    public void DataClear()
    {
        nowPlayer = new GlobalPlayerData
        {
            playerName = "New Player",
            maxHp = 100,
            attackDamage = 10,
            speed = 5,
            luck = 1,
            coin = 0,
            upgrades = new Dictionary<string, int>()
        };
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
