using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Main_Continue : MainScene
{
    GameObject[] slots;
    List<Button> buttons = new List<Button>();

    GameObject slot1;
    GameObject slot2;
    GameObject slot3;

    int slots_cur;


    void Start()
    {
        Init();
        //SlotPill();

    }

    void Update()
    {
        if (menuCo == null)
        {
            menuCo = StartCoroutine(Slots_Select());
        }
        SelectedEnter();
    }

    private IEnumerator Slots_Select()
    {
        float y = -InputKey.GetAxisRaw(InputKey.Vertical);


        slots_cur += (int)y;

        if (slots_cur == slots.Length)
        {
            slots_cur = 0;
            slots[slots.Length - 1].GetComponent<Outline>().effectDistance = new(0, 0);
            slots[slots_cur].GetComponent<Outline>().effectDistance = new(10, 10);
            yield return null;
        }

        if (slots_cur == -1)
        {
            slots_cur = slots.Length - 1;
            slots[0].GetComponent<Outline>().effectDistance = new(0, 0);
            slots[slots_cur].GetComponent<Outline>().effectDistance = new(10, 10);
            yield return null;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Outline>().effectDistance = new(0, 0);
        }
        slots[slots_cur].GetComponent<Outline>().effectDistance = new(10, 10);
        if (y == 0)
            yield return null;
        else
            yield return inputDelay.GetDelay();
        menuCo = null;
    }

    void SelectedEnter()
    {
        if (InputKey.GetButtonDown(InputKey.Interaction))
        {
            //switch (slots_cur)
            //{
            //    case 0:
            //        Debug.Log("1�� �������� ���� ����");
            //        buttons[slots_cur].onClick.Invoke();
            //        break;

            //    case 1:
            //        Debug.Log("2�� �������� ���� ����");
            //        //Todo : ���� ȭ������ �̵� ��������
            //        break;

            //    case 2:
            //        Debug.Log("3�� �������� ���� ����");
            //        //Todo : �ɼ� �˾� ��������
            //        break;
            //}
            buttons[slots_cur].onClick.Invoke();
        }

        if (InputKey.GetButtonDown(InputKey.Cancel))
        {
            gameObject.SetActive(false);
            Debug.Log("�̾��ϱ� ȭ�� ������");
        }
    }

    void SlotPill()
    {
        //Todo : ���̺� ���� ���� ��� ���� ä������
        slot1.GetComponentInChildren<TMP_Text>().text = "Empty";
        slot2.GetComponentInChildren<TMP_Text>().text = "Empty";
        slot3.GetComponentInChildren<TMP_Text>().text = "Empty";
    }

    private void Init()
    {

        slots = new GameObject[3];

        slots[0] = slot1 = GetUI("FirstSlot");
        slots[1] = slot2 = GetUI("SecondSlot");
        slots[2] = slot3 = GetUI("ThirdSlot");
        Debug.Log(GetUI<Button>("FirstSlot"));
        buttons.Add(GetUI<Button>("FirstSlot"));
        buttons.Add(GetUI<Button>("SecondSlot"));
        buttons.Add(GetUI<Button>("ThirdSlot"));
        slots_cur = 0;
    }

}
