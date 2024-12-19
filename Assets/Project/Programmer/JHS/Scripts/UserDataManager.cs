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
        // �����Ϳ� ���� ������ ����
        string data = JsonUtility.ToJson(nowPlayer);
        // path ����� fileName���Ͽ� ������ ���� 
        File.WriteAllText(path + nowSlot.ToString(), data);
        Debug.Log("���� ���� �Ϸ�!");
    }
    // �ε� ��� 
    public void LoadData()
    {
        // �����Ϳ� path ����� fileName���� �ҷ�����
        string data = File.ReadAllText(path + nowSlot.ToString());
        // ���� �÷��̾ �ҷ��� ������ ����
        nowPlayer = JsonUtility.FromJson<GlobalPlayerData>(data);
        Debug.Log("���� �ε� �Ϸ�!");
    }
    public void DataClear()
    {
        nowSlot = 0;
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
}
