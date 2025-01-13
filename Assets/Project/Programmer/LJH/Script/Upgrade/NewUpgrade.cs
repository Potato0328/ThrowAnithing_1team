using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class NewUpgrade : BaseUI
{
    [Inject]
    private GlobalGameData _gameData;

    [SerializeField] GameObject pause;

    //�÷��̾� / ī�޶� �����
    PlayerController player;
    float cameraSpeed;

    int ho;
    int ver;

    //Comment : Infomation > name
    [SerializeField] TMP_Text itemName;
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemInfo;
    [SerializeField] TMP_Text infotext;


    //Comment : for Test
    [SerializeField] int usedCost;

    int costLimit1 = 5000;
    int costLimit2 = 30000;
    int costLimit3 = 80000;
    int costLimit4 = 200000;

    // Comment : Cost Tier
    int tier;
    Color lockedColor = new(0.1f, 0, 0.2f);

    bool axisInUse; // ���� ���� ������

    //�ؽ�Ʈ ó����
    [SerializeField] GameObject upText;

    int slot;

    List<List<Button>> buttons = new();
    List<Button> tier1 = new();
    List<Button> tier2 = new();
    List<Button> tier3 = new();
    List<Button> tier4 = new();
    List<Button> tier5 = new();

    [SerializeField] List<Button> buttonIndex = new();
    [SerializeField] List<Image> slotImage = new();

    Button curButton;

    private void Awake()
    {
        Bind();
        Init();
    }

    private void Start()
    {

    }

    private void OnEnable()
    {

        cameraSpeed = player.setting.cameraSpeed;
        player.setting.cameraSpeed = 0;
    }

    private void OnDisable()
    {
        ver = 0;
        ho = 0;

        player.setting.cameraSpeed = cameraSpeed;
    }

    private void Update()
    {
        curButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();


        if (pause.activeSelf)
            return;

        MaxSlot();

        Slot_Selected();

        //Comment : For test
        if (InputKey.GetButtonDown(InputKey.PrevInteraction))
        {
            curButton.onClick.Invoke();
        }
        
    

    }



    /// <summary>
    /// Comment : if usedCost greater than costLimit Method
    /// </summary>
    void TierCal()
    {
        tier = 1;
        if (_gameData.usingCoin >= costLimit1)
            tier = 2;
        if (_gameData.usingCoin >= costLimit2)
            tier = 3;
        if (_gameData.usingCoin >= costLimit3)
            tier = 4;
        if (_gameData.usingCoin >= costLimit4)
            tier = 5;
    }

    /// <summary>
    /// ����� ���� ����Ͽ� ��ư Ȱ��ȭ / ��Ȱ��ȭ
    /// </summary>
    void SlotLimit()
    {
        TierCal();

        for (int i = tier; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                //Todo : Change Color
                buttons[i][j].GetComponent<Image>().color = new(0.1f, 0, 0.2f);
                buttons[i][j].interactable = false;
            } 
        }

    }


    /// <summary>
    /// Comment : ���� ó��
    /// </summary>
    void Slot_Selected()
    {

        // Comment : �ٸ� ���� �� ����
        ColorReset();
        // Comment : ������ ���� ���������

        curButton.GetComponent<Image>().color = new(0.7f, 0.7f, 0.1f);

        itemName.text = curButton.name;
        itemInfo.text = curButton.name;

        for (int i = 0; i < buttonIndex.Count; i++)
        {
            if(curButton == buttonIndex[i])
                itemImage.sprite = slotImage[i].sprite;
            infotext.text = $"{_gameData.upgradeLevels[i]} / 5";
        }

        //�׽�Ʈ��
        //MaxSlot();

        SlotLimit();
    }

    void MaxSlot()
    {
        //�׽�Ʈ��
        for (int i = 0; i < slotMaxCheck.Length; i++)
        {
            if (_gameData.upgradeLevels[i] == 5)
            {
                int ver;
                int ho;

                ver = i / 4;
                ho = i % 4;
                curButton.transform.transform.GetChild(1).GetComponent<TMP_Text>().gameObject.SetActive(true);
                curButton.onClick.RemoveAllListeners();

                SaveMaxSlot();
            }
        }
    }

    bool[] slotMaxCheck = new bool[20];

    //5���� ���� Ư���� ��ȭ �Ϸ� ǥ�� ����
    void SaveMaxSlot()
    {
        for (int i = 0; i < buttonIndex.Count; i++)
        {
                slotMaxCheck[i] = buttonIndex[i].transform.GetChild(1).GetComponent<TMP_Text>().gameObject.activeSelf;
            
        }
    }

    
    //���� Ŭ����
    void ClickedSlots(Button button)
    {
        if (button.GetComponent<Image>().color != lockedColor)
        {
            curButton.GetComponent<Image>().color = new(0.2f, 0.25f, 0.6f);

            itemName.text = button.name;
            itemInfo.text = button.name;
            for (int i = 0; i < buttonIndex.Count; i++)
            {
                if (curButton == buttonIndex[i])
                    slotImage[i] = button.transform.GetChild(0).GetComponent<Image>();
            }
            curButton.GetComponent<Image>().color = new(0.7f, 0.7f, 0.1f);

            if (button.GetComponent<Image>().color != lockedColor)
            {
                for (int i = 0; i < tier; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        buttons[i][j].interactable = true;
                    }
                }
            }
        }
    }

    // ������ ��ư�� �ε����� slots �ε��� ��ü
   //(int, int) FindButton(Button button)
   //{
   //    for (int i = 0; i < 5; i++)
   //    {
   //        for (int j = 0; j < 4; j++)
   //        {
   //            if (slots[i, j].name == button.name)
   //            {
   //                return (i, j);
   //            }
   //        }
   //    }
   //    //�ν� ������ �� 0,0 ���� �ʱ�ȭ
   //    return (0, 0);
   //
   //}

    void ColorReset()
    {
        curButton.GetComponent<Image>().color = new(0.2f, 0.25f, 0.6f);
    }

    public void UpgradeText()
    {
        // ���׷��̵� �Ϸ� ���� �������� ����
        int cost = _gameData.upgradeCosts[slot];

        if (_gameData.coin >= cost)
            Instantiate(upText, buttonIndex[10].transform.position, Quaternion.identity);
    }

    void Init()
    {
        // 0 1 2 3  tier 1
        tier1.Add(GetUI<Button>("���� ���ݷ� ����"));
        tier1.Add(GetUI<Button>("���Ÿ� ���ݷ� ����"));
        tier1.Add(GetUI<Button>("�̵� �ӵ� ����"));
        tier1.Add(GetUI<Button>("�ִ� ü�� ����"));
        // 4 5 6 7  tier 2
        tier2.Add(GetUI<Button>("���׹̳� ����"));
        tier2.Add(GetUI<Button>("���� �ӵ� ����"));
        tier2.Add(GetUI<Button>("ġ��Ÿ Ȯ�� ����"));
        tier2.Add(GetUI<Button>("��� �߰� Ȯ�� ����"));
        // 8 9 10 11 tier 3
        tier3.Add(GetUI<Button>("���ݷ� ����"));
        tier3.Add(GetUI<Button>("������Ʈ ������ ����"));
        tier3.Add(GetUI<Button>("���� ����"));
        tier3.Add(GetUI<Button>("���� ȸ���� ����"));
        // 12 13 14 15 tier 4
        tier4.Add(GetUI<Button>("���׹̳� ���ҷ� ����"));
        tier4.Add(GetUI<Button>("���Ÿ� ���ݷ� ����2"));
        tier4.Add(GetUI<Button>("���� ���ݷ� ����2"));
        tier4.Add(GetUI<Button>("������Ʈ ȹ�淮 ����"));
        // 16 17 18 19 tier 5
        tier5.Add(GetUI<Button>("���� ���ҷ� ����"));
        tier5.Add(GetUI<Button>("ü�� ����� ����"));
        tier5.Add(GetUI<Button>("���� ����2"));
        tier5.Add(GetUI<Button>("��� �߰� Ȯ�� ����2"));

        buttons.Add(tier1);
        buttons.Add(tier2);
        buttons.Add(tier3);
        buttons.Add(tier4);
        buttons.Add(tier5);


        for (int i = 0; i < slotImage.Count; i++)
        {
                slotImage[i] = buttonIndex[i].transform.GetChild(0).GetComponent<Image>();

                int row = i;

                buttonIndex[i].onClick.AddListener(() => ClickedSlots(buttonIndex[row]));
                buttonIndex[i].onClick.AddListener(() => UpgradeText());
                
        }

        player = GameObject.FindWithTag(Tag.Player).GetComponent<PlayerController>();

        
    }
}