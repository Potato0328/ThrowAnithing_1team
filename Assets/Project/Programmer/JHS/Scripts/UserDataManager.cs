using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Zenject;

[System.Serializable]
public class UserDataManager : MonoBehaviour
{
    // 플레이어 데이터 생성
    [SerializeField] GlobalGameData nowPlayer;
    [Inject]
    GlobalPlayerStateData playerstate;

    // 세이브 파일 저장 경로
    public string path;
    // 현재 슬롯번호
    public int nowSlot;

    [Inject]
    private void Init(GlobalGameData nowPlayer)
    {
        this.nowPlayer = nowPlayer;
        // 초기화 코드 작성
        path = Application.persistentDataPath + "/save";
        Debug.Log($"Save path: {path}");

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        // 파일 경로 초기화 (세이브 경로는 Zenject에서 주입됨)
        Debug.Log($"Save path: {path}");
        playerstate.NewPlayerSetting();
    }
    // 저장 기능
    public void SaveData()
    {
        // 현재 날짜와 시간 저장
        nowPlayer.saveDateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        // 데이터에 현재 데이터 저장
        string data = JsonUtility.ToJson(nowPlayer);
        // 각 슬롯에 대해 파일 경로 다르게 설정 (슬롯 번호에 맞는 파일로 저장)
        string slotPath = path + $"slot_{nowSlot}.json";
        File.WriteAllText(slotPath, data);
        Debug.Log($"슬롯 {nowSlot + 1}에 게임 저장 완료! 저장 시간: {nowPlayer.saveDateTime}");
    }
    // 로드 기능 
    public void LoadData()
    {
        // 각 슬롯에 대해 파일 경로 다르게 설정 (슬롯 번호에 맞는 파일로 불러오기)
        string slotPath = path + $"slot_{nowSlot}.json";
        if (File.Exists(slotPath))
        {
            string data = File.ReadAllText(slotPath);
            // 현재 플레이어에 불러온 데이터 적용
            nowPlayer = JsonUtility.FromJson<GlobalGameData>(data);
            // 강화 플레이어 스탯 새팅을 위한 값
            nowPlayer.bringData = true;
            Debug.Log($"슬롯 {nowSlot + 1}에서 게임 로드 완료!");
        }
        else
        {
            Debug.LogWarning($"슬롯 {nowSlot + 1}에 저장된 데이터가 없습니다.");
        }
    }
    public void DataClear()
    {
        nowPlayer = new GlobalGameData
        {
            coin = 0,
            saveDateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            upgradeLevels = new int[20],
            usingCoin = 0
        };
        // 플레이어 스탯 리셋
        playerstate.NewPlayerSetting();
    }
    public void DeleteSlotData(int slotIndex)
    {
        string slotPath = path + $"slot_{slotIndex}.json";
        if (File.Exists(slotPath))
        {
            File.Delete(slotPath); // 해당 슬롯 파일 삭제
            Debug.Log($"슬롯 {slotIndex + 1} 데이터 삭제 완료");
        }
        else
        {
            Debug.LogWarning($"슬롯 {slotIndex + 1}에 데이터가 없습니다.");
        }
    }
    public void DeleteData()
    {
        // 모든 슬롯에 대해 데이터 삭제
        for (int i = 0; i < 3; i++) // 슬롯 수는 3으로 가정, 슬롯 수에 맞게 수정
        {
            string slotPath = path + $"slot_{i}.json";
            if (File.Exists(slotPath))
            {
                File.Delete(slotPath); // 슬롯 파일 삭제
                Debug.Log($"슬롯 {i + 1} 데이터 삭제 완료");
            }
        }

        // 플레이어 데이터 초기화
        DataClear();

        Debug.Log("모든 게임 데이터 삭제 완료");
    }
}