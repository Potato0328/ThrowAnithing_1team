using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Upgrade : UpgradeBinding
{
    [Inject]
    private GlobalGameData _gameData;

    Button[,] slots;
    Image[,] slotImages;

    int ho = 0;
    int ver = 0;

    Coroutine slotCo;
    Coroutine buttonCo;
    float inputDelay = 0.25f;

    //Comment : Infomation > name
    [SerializeField] TMP_Text itemName;
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemInfo;


    //Comment : for Test
    [SerializeField] int usedCost;

    int costLimit1 = 5000;
    int costLimit2 = 30000;
    int costLimit3 = 80000;
    int costLimit4 = 200000;

    // Comment : Cost Tier
    int tier;

    private void Awake()
    {
        Bind();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (slotCo == null)
            slotCo = StartCoroutine(Slot_Selected());

        //Comment : For test
        if (Input.GetButtonDown("Interaction"))
        {
            slots[ver, ho].onClick.Invoke();
        }
    }

    //Comment : if usedCost greater than costLimit Method
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

    void SlotLimit()
    {
        TierCal();

        for (int i = tier; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                //Todo : Change Color
                slots[i, j].GetComponent<Image>().color = new(0.1f, 0, 0.2f);
            }
        }

    }


    //Comment : ���� �̵� �Լ�
    IEnumerator Slot_Selected()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = -Input.GetAxisRaw("Vertical");

        ho += (int)x;
        ver += (int)y;

        if (ho == -1)
        {
            ho = 3;
        }
        if (ho == 4)
        {
            ho = 0;
        }
        if (ver == -1)
        {
            ver = tier - 1;
        }
        if (ver == tier)
        {
            ver = 0;
        }

        // Comment : Other Buttons color Reset
        ColorReset();
        // Comment : Selected Button color Changed

        slots[ver, ho].GetComponent<Image>().color = new(0.7f, 0.7f, 0.1f);

        itemName.text = slots[ver, ho].name;
        itemImage.sprite = slotImages[ver, ho].sprite;
        itemInfo.text = slots[ver, ho].name;

        SlotLimit();
        yield return inputDelay.GetDelay();
        slotCo = null;

    }


    // Comment : for Test
    public void ¥��()
    {
        Debug.Log("¥��");


    }

    void ColorReset()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                slots[i, j].GetComponent<Image>().color = new(0.2f, 0.25f, 0.6f);
            }
        }
    }

    void Init()
    {
        slots = new Button[5, 4];
        slotImages = new Image[5, 4];

        slots[0, 0] = GetUI<Button>("���� ���ݷ� ����");
        slots[0, 1] = GetUI<Button>("���Ÿ� ���ݷ� ����");
        slots[0, 2] = GetUI<Button>("�̵� �ӵ� ����");
        slots[0, 3] = GetUI<Button>("�ִ� ü�� ����");

        slots[1, 0] = GetUI<Button>("���׹̳� ����");
        slots[1, 1] = GetUI<Button>("���� �ӵ� ����");
        slots[1, 2] = GetUI<Button>("ġ��Ÿ Ȯ�� ����");
        slots[1, 3] = GetUI<Button>("��� �߰� Ȯ�� ����");

        slots[2, 0] = GetUI<Button>("���ݷ� ����");
        slots[2, 1] = GetUI<Button>("������Ʈ ������ ����");
        slots[2, 2] = GetUI<Button>("���� ����");
        slots[2, 3] = GetUI<Button>("���� ȸ���� ����");

        slots[3, 0] = GetUI<Button>("���׹̳� ���ҷ� ����");
        slots[3, 1] = GetUI<Button>("���Ÿ� ���ݷ� ����2");
        slots[3, 2] = GetUI<Button>("���� ���ݷ� ����2");
        slots[3, 3] = GetUI<Button>("������Ʈ ȹ�淮 ����");

        slots[4, 0] = GetUI<Button>("���� ���ҷ� ����");
        slots[4, 1] = GetUI<Button>("ü�� ����� ����");
        slots[4, 2] = GetUI<Button>("���� ����2");
        slots[4, 3] = GetUI<Button>("��� �߰� Ȯ�� ����2");

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                slotImages[i, j] = slots[i, j].transform.GetChild(0).GetComponent<Image>();
            }
        }
    }
}