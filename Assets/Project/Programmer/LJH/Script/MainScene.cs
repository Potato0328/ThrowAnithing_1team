using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum CurState
{
    main,
    _continue,
    optionDepth1,
    optionDepth2,
};
public class MainScene : BaseUI
{
    public CurState curState { get; protected set; }

    //���� ȭ��
    protected GameObject main;
    //�޴� ��ư
    GameObject[] menuButtons;

    GameObject continueImage;
    GameObject newImage;
    GameObject optionImage;
    GameObject exitImage;

    //�̾��ϱ� �г�
    GameObject main_continue;

    //�ɼ� �г�
    GameObject option;

    //������ �˾�
    GameObject exitPopUpObj;
    GameObject[] exitButtons;

    GameObject exitYes;
    GameObject exitNo;

    int exitNum;

    //�޴� �����ϴ� �迭�� �ε���(���� ���õ� �޴�)
    int curMenu;

    protected bool isOption;
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
        if (!option.activeSelf && !main_continue.activeSelf)
            curState = CurState.main;

        Debug.Log(curState);
        if (curState == CurState.main)
        {
            MenuSelect();

            SelectedEnter();

            if (exitPopUpObj.activeSelf)
                ExitPopUp();
        }

        
    }

    // Comment : �ʱ�ȭ �뵵�� �Լ�
    void Init()
    {
        main = GetUI("MainButtons");
       // curState = CurState.main;

        menuButtons = new GameObject[4];
        menuButtons[0] = continueImage = GetUI("ContinueImage");
        menuButtons[1] = newImage = GetUI("NewImage");
        menuButtons[2] = optionImage = GetUI("OptionImage");
        menuButtons[3] = exitImage = GetUI("ExitImage");

        exitButtons = new GameObject[2];
        exitButtons[0] = exitYes = GetUI("YesImage");
        exitButtons[1] = exitNo = GetUI("NoImage");

        exitPopUpObj = GetUI("ExitPopUp");

        main_continue = GetUI("Background_continue");

        option = GetUI("Background_option");
        
        
        curMenu = 0;
        exitNum = 0;

        
    }

    // Comment : W/S �Ǵ� ��/�Ʒ� ȭ��ǥ Ű�� �̿��� �޴� ���� ���� �Լ�
    // Todo : �� ȿ������ ����� �ִ��� �����غ���
    private void MenuSelect()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            menuButtons[curMenu].SetActive(false);

            if (curMenu == menuButtons.Length-1)
            {
                curMenu = 0;
                menuButtons[curMenu].SetActive(true);
                return;
            }

            curMenu++;
            menuButtons[curMenu].SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            menuButtons[curMenu].SetActive(false);

            if (curMenu == 0)
            {
                curMenu = menuButtons.Length - 1;
                menuButtons[curMenu].SetActive(true);
                return;
            }

            curMenu--;
            menuButtons[curMenu].SetActive(true);
        }
    }

    //Comment : ������ �޴��� �����ϴ� �Լ�
    void SelectedEnter()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (curMenu)
            {
                case 0:
                    Debug.Log("����ϱ� ����_���� ���� �˾� ����");
                    //Todo : ���� ���� �˾� �������� > �鿣��� ���� �ʿ��� ��
                    main_continue.SetActive(true);
                    curState = CurState._continue;
                    break;

                case 1:
                    Debug.Log("�����ϱ� ����_���� ���� ȭ������ �̵�");
                    //Todo : ���� ȭ������ �̵� ��������
                    break;

                case 2:
                    Debug.Log("�ɼ� ����_�ɼ� �˾� ����");
                    option.SetActive(true);
                    curState = CurState.optionDepth1;
                    //Todo : �ɼ� �˾� ��������
                    break;

                case 3:
                    Debug.Log("���� ���� ����_���� ����");
                    exitPopUpObj.SetActive(true);
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitPopUpObj.SetActive(true);
            Debug.Log("���� ���� ����_���� ����");
        }
    }

    void ExitPopUp()
    {

        switch (exitNum)
        {
            case 0:
                exitButtons[exitNum].GetComponent<Image>().color = Color.black;
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    exitButtons[exitNum].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                    exitNum = 1;
                }
                if (Input.GetKeyDown(KeyCode.E))
                    ExitGame();
                break;

            case 1:
                exitNo.GetComponent<Image>().color = Color.black;
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    exitNo.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                    exitNum = 0;
                }
                if (Input.GetKeyDown(KeyCode.E))
                    exitPopUpObj.SetActive(false);
                break;

        }
    }
    void ExitGame()
    {
        //Comment : ����Ƽ �����ͻ󿡼� ����
        UnityEditor.EditorApplication.isPlaying = false;
        //Comment : ���� �󿡼� ����
        Application.Quit();
    }

}
