using System.Collections;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using Zenject.SpaceFighter;

public class NewArmChange : BaseUI
{
    [Inject]
    GlobalGameData playerStateData;
    [Inject]
    PlayerData playerData;
    int arm_cur;
    GameObject[] armUnits;
    Button[] armButtons;
    [SerializeField] SaveSystem saveSystem;

    Forge _forge;

    float inputDelay = 0.25f;

    Coroutine armCo;

    Color color;

    //ī�޶� �����
    PlayerController player;
    float cameraSpeed;

    private void Awake()
    {
        Bind();
        Init();
        _forge = GetComponentInParent<Forge>();
    }


    private void OnEnable()
    {
        cameraSpeed = player.setting.cameraSpeed;
        player.setting.cameraSpeed = 0;

    }
    private void OnDisable()
    {

        player.setting.cameraSpeed = cameraSpeed;

    }
    private void Update()
    {
        ArmUnit_changeColor();
    }
    
    //Todo : �Լ� �̸� �ٲ����
    void ArmUnit_changeColor()
    {
        for (int i = 0; i < armUnits.Length; i++)
        {
            color = armUnits[i].GetComponent<Image>().color;
            color.a = 0.1f;
            armUnits[i].GetComponent<Image>().color = color;
        }

        color = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;
        color.a = 1f;
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = color;

        saveSystem.SavePlayerData();

    }

    #region �׽�Ʈ�� �Լ�
    public void Power()
    {
        Debug.Log("�Ŀ� Ÿ�� ����");
        playerStateData.nowWeapon = GlobalGameData.AmWeapon.Power;
        playerData.NowWeapon = playerStateData.nowWeapon;
        // ������ ���̺�
        saveSystem.SavePlayerData();
    }

    public void Balance()
    {
        Debug.Log("�뷱�� Ÿ�� ����");
        playerStateData.nowWeapon = GlobalGameData.AmWeapon.Balance;
        playerData.NowWeapon = playerStateData.nowWeapon;
        // ������ ���̺�
        saveSystem.SavePlayerData();
    }

    public void Speed()
    {
        Debug.Log("���ǵ� Ÿ�� ����");
    }
    #endregion

    public void ArmUnitClose()
    {
        // gameObject.SetActive(false);
        _forge.SetUnActiveUI();
    }
    void Init()
    {
        armUnits = new GameObject[3];

        armUnits[0] = GetUI("PowerButton");
        armUnits[1] = GetUI("BalanceButton");
        armUnits[2] = GetUI("SpeedButton");

        armButtons = new Button[3];

        armButtons[0] = GetUI<Button>("PowerButton");
        armButtons[1] = GetUI<Button>("BalanceButton");
        armButtons[2] = GetUI<Button>("SpeedButton");

        armButtons[0].onClick.AddListener(Power);
        armButtons[1].onClick.AddListener(Balance);
        armButtons[2].onClick.AddListener(Speed);

        player = GameObject.FindWithTag(Tag.Player).GetComponent<PlayerController>();
    }
}