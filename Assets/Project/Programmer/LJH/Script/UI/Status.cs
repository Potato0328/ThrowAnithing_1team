using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Status : MonoBehaviour
{
    //Comment : ü��,����,���׹̳� �� 1�� / �ش� ��ġ�� �ִ� ��ġ / �ش� ��ġ�� ���� ��ġ 3������ �μ��� �Է��Ͽ� ���� ä���� ����
    protected void BarValueController(Slider bar, float maxValue, float curValue)
    {
        float per;

        per = curValue/ maxValue * 100;

        bar.value = per;
    }


    void Init()
    {
        

    }


}
