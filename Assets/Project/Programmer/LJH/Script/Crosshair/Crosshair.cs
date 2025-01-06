using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : BaseUI
{
    //  UI ����Ʈ
    List<GameObject> uiList = new List<GameObject>();
    
    // ũ�ν����
    GameObject crossHair;

    private void Awake()
    {
        Bind();
        Init();
    }
    private void Update()
    {
        if(Time.timeScale == 0)
            crossHair.SetActive(false);
        else
            crossHair.SetActive(true);

        // �Ƚᵵ �ɰ� ���Ƽ� ����
        //crossHair.SetActive(CrosshairOnOff());
    }

    // Comment : �Ƚᵵ �ɰ� ���Ƽ� ����
    bool CrosshairOnOff()
    {
        for (int i = 0; i < uiList.Count; i++)
        {
            if (uiList[i].activeSelf)
            {
                return false;
            }

        }
        return true;
    }

    void Init()
    {
        crossHair = GetUI("Crosshair");

    }

}
