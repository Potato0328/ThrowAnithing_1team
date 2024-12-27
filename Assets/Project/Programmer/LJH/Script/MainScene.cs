using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    Button[] menuButtons;

    Button continueImage;
    Button newImage;
    Button optionImage;
    Button exitImage;

    //�̾��ϱ� �г�
    GameObject main_continue;

    //�ɼ� �г�
    protected GameObject option;

    //������ �˾�
    GameObject exitPopUpObj;
    GameObject[] exitButtons;

    GameObject exitYes;
    GameObject exitNo;

    //Comment : Exit üũ�� ����
    int exitCheck;

    int exitNum;

    //�޴� �����ϴ� �迭�� �ε���(���� ���õ� �޴�)
    int curMenu;

    protected bool isOption;
    protected float inputDelay = 0.15f;

    protected Coroutine menuCo;
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

       // if (option.activeSelf)
       //     curState = CurState.optionDepth1;

        if (curState == CurState.main)
        {
            if (menuCo == null)
            {
                menuCo = StartCoroutine(MenuSelect());
            }
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

        menuButtons = new Button[4];
        menuButtons[0] = continueImage = GetUI<Button>("ContinueImage");
        menuButtons[1] = newImage = GetUI<Button>("NewImage");
        menuButtons[2] = optionImage = GetUI<Button>("OptionImage");
        menuButtons[3] = exitImage = GetUI<Button>("ExitImage");

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

    private IEnumerator MenuSelect()
    {
        float y = -Input.GetAxisRaw("Vertical");


        curMenu += (int)y;

        if (curMenu == menuButtons.Length)
        {
            curMenu = 0;
            menuButtons[menuButtons.Length - 1].gameObject.SetActive(false);
            menuButtons[curMenu].gameObject.SetActive(true);
            yield return null;
        }

        if (curMenu == -1)
        {
            curMenu = menuButtons.Length - 1;
            menuButtons[0].gameObject.SetActive(false);
            menuButtons[curMenu].gameObject.SetActive(true);
            yield return null;
        }

        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].gameObject.SetActive(false);
        }
        menuButtons[curMenu].gameObject.SetActive(true);

        yield return inputDelay.GetDelay();
        menuCo = null;
    }


    //Comment : ������ �޴��� �����ϴ� �Լ�
    void SelectedEnter()
    {
        // Todo: �е���� ���� �����ϰ� �ٲ����
        if (Input.GetButtonDown("Interaction"))
        {
            switch (curMenu)
            {
                case 0:
                    Debug.Log("����ϱ� ����_���� ���� �˾� ����");
                    //Todo : ���� ���� �˾� �������� > �鿣��� ���� �ʿ��� ��
                    ContinueButton();
                    break;

                case 1:
                    Debug.Log("�����ϱ� ����_���� ���� ȭ������ �̵�");
                    //Todo : ���� ȭ������ �̵� ��������
                    NewButton();
                    break;

                case 2:
                    Debug.Log("�ɼ� ����_�ɼ� �˾� ����");
                    OptionButton();
                    //Todo : �ɼ� �˾� ��������
                    break;

                case 3:
                    Debug.Log("���� ���� ����_���� ����");
                    ExitButton();
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitPopUpObj.SetActive(true);
            Debug.Log("���� ���� ����_���� ����");
        }
    }

    public void ContinueButton()
    {
        main_continue.SetActive(true);
        curState = CurState._continue;
    }

    public void NewButton()
    {

    }

    public void OptionButton()
    {
        option.SetActive(true);
        curState = CurState.optionDepth1;
    }

    public void ExitButton()
    {
        exitPopUpObj.SetActive(true);
        exitCheck ++;
        Debug.Log(exitCheck);
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
                    if (Input.GetButtonDown("Interaction"))
                    {
                        Debug.Log("����");
                    if (exitCheck > 1)
                    {
                        ExitGame();
                    }
                    exitCheck++;
                    }
                    break;

                case 1:
                    exitNo.GetComponent<Image>().color = Color.black;
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                    {
                        exitNo.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                        exitNum = 0;
                    }
                    if (Input.GetButtonDown("Interaction"))
                    {
                        exitCheck = 0;
                        exitPopUpObj.SetActive(false);
                    }
                    break;

            }
    }
    protected void ExitGame()
    {
#if UNITY_EDITOR
        //Comment : ����Ƽ �����ͻ󿡼� ����
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //Comment : ���� �󿡼� ����
        Application.Quit();
#endif
    }

}
