using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ArmChange : BaseUI
{
    int arm_cur;
    GameObject[] armUnits;
    Button[] armButtons;

    float inputDelay = 0.25f;

    Coroutine armCo;

    Color color;

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
        if (armCo == null)
            armCo = StartCoroutine(ArmUnit_Select());

        Select_ArmUnit();
    }
    private IEnumerator ArmUnit_Select()
    {
        float x = Input.GetAxisRaw("Horizontal");

        arm_cur += (int)x;

        if (arm_cur == armUnits.Length)
        {
            arm_cur = 0;
            // Comment ���� ��ư�� ������ 0.1�� ����
            color = armUnits[armUnits.Length - 1].GetComponent<Image>().color;
            color.a = 0.1f;
            armUnits[armUnits.Length - 1].GetComponent<Image>().color = color;

            // Comment : ���õ� ��ư�� ������ 1�� ����
            color = armUnits[arm_cur].GetComponent<Image>().color;
            color.a = 1;
            armUnits[arm_cur].GetComponent<Image>().color = color;
            yield return null;
        }

        if (arm_cur == -1)
        {
            arm_cur = armUnits.Length - 1;
            // Comment ���� ��ư�� ������ 0.1�� ����
            color = armUnits[0].GetComponent<Image>().color;
            color.a = 0.1f;
            armUnits[0].GetComponent<Image>().color = color;

            // Comment : ���õ� ��ư�� ������ 1�� ����
            color = armUnits[arm_cur].GetComponent<Image>().color;
            color.a = 1;
            armUnits[arm_cur].GetComponent<Image>().color = color;
            yield return null;
        }

        //Comment : ��� ��ư�� ������ 0.1�� ����
        ButtonReset();

        //Comment : ���õ� ��ư�� ������ 1�� ����
        color = armUnits[arm_cur].GetComponent<Image>().color;
        color.a = 1f;
        armUnits[arm_cur].GetComponent<Image>().color = color;

        yield return inputDelay.GetDelay();
        armCo = null;
    }

    void ButtonReset()
    {
        for (int i = 0; i < armUnits.Length; i++)
        {
            color = armUnits[i].GetComponent<Image>().color;
            color.a = 0.1f;
            armUnits[i].GetComponent<Image>().color = color;
        }
    }

    void Select_ArmUnit()
    {
        if (Input.GetButtonDown("Interaction"))
        {
            armButtons[arm_cur].onClick.Invoke();
        }
    }
    #region �׽�Ʈ�� �Լ�
    public void Power()
    {
        Debug.Log("�Ŀ� Ÿ�� ����");
    }

    public void Balance()
    {
        Debug.Log("�뷱�� Ÿ�� ����");
    }

    public void Speed()
    {
        Debug.Log("���ǵ� Ÿ�� ����");
    }
    #endregion

    public void ArmUnitClose()
    {
        gameObject.SetActive(false);
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
    }
}