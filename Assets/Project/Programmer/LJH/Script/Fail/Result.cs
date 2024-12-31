using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Result : BaseUI
{
    [Inject]
    PlayerData playerData;

    //�÷��� Ÿ��
    TMP_Text timer;
    // Ŭ���� ���� ��
    TMP_Text round;
    // ���� ���� ��
    TMP_Text kill;
    // ȹ���� ����Ʈ ����Ʈ
    TMP_Text rp;
    private void Awake()
    {
        Bind();
        Init();
    }

    void Update()
    {
        if (Input.GetButtonDown("Interaction"))
            ToBase.ToLobby();
    }



    void Init()
    {
        // ���� �� ���� ä������
        timer = GetUI<TMP_Text>("Timer");
        round = GetUI<TMP_Text>("StageText");
        kill = GetUI<TMP_Text>("KillText");
        rp = GetUI<TMP_Text>("RpText");

    }
}
