using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Status : BaseStatus
{
    Image hpBar;
    Image mpBar;
    Image staminaBar;

    float maxHp;
    float curHp;

    float maxMp;
    float curMp;

    float maxStamina;
    float curStamina;

    private void Awake()
    {
        Bind();
    }
    private void Start()
    {
        Init();
    }

    //Comment : ü��,����,���׹̳� �� 1�� / �ش� ��ġ�� �ִ� ��ġ / �ش� ��ġ�� ���� ��ġ 3������ �μ��� �Է��Ͽ� ���� ä���� ����
    protected void BarValueController(Image bar, float maxValue, float curValue)
    {
        float per;

        per = curValue/ maxValue;

        bar.fillAmount = per;
    }


    void Init()
    {
        hpBar = GetImage("HpBar");
        mpBar = GetImage("MpBar");
        staminaBar = GetImage("StaminaBar");

    }


}
