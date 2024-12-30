using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Rendering.InspectorCurveEditor;

public class Pause : MainScene
{
    GameObject pause;


    Button continueButton_P;
    Button optionButton_P;
    Button exitButton_P;

    [SerializeField] GameObject ingameOptions;

    int curMenu_p;

    Button[] pauseButtons;
    Button[] exitButtons_p;

    Coroutine settingCo;

    int exitNum_p;

    GameObject exitPopUpObj_p;

    //Comment : ���� ����Ǿ����� üũ�� �Һ���
    bool isPaused;


    private void Awake()
    {
        Bind();
    }
    void Start()
    {
        Init();
    }

    private void Update()
    {
        if (pause.activeSelf)
        {
            if (settingCo == null)
                settingCo = StartCoroutine(MenuSelect());

            if (Input.GetButtonDown("Interaction"))
                SelectedEnter();

            //Time.timeScale = 0;
            isPaused = true;
        }
        else if (!pause.activeSelf)
        {
            Time.timeScale = 1f;
            isPaused = false;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("escŰ������");
            PauseOnOff();
        }

    }

    void PauseOnOff()
    {
        pause.SetActive(!pause.activeSelf);
    }

    private IEnumerator MenuSelect()
    {
        float y = -Input.GetAxisRaw("Vertical");


        curMenu_p += (int)y;

        //Comment : ������ ��ư�� ��, ù ��ư���� ���ư���
        if (curMenu_p == pauseButtons.Length)
        {
            curMenu_p = 0;
            pauseButtons[pauseButtons.Length - 1].gameObject.SetActive(false);
            pauseButtons[curMenu_p].gameObject.SetActive(true);
            yield return null;
        }

        //Comment : ù ��ư�� ��, ������ ��ư���� ���ư���
        if (curMenu_p == -1)
        {
            curMenu_p = pauseButtons.Length - 1;
            pauseButtons[0].gameObject.SetActive(false);
            pauseButtons[curMenu_p].gameObject.SetActive(true);
            yield return null;
        }

        //Comment : �������� ���� ��ư ��� ��Ȱ��ȭ �۾�
        for (int i = 0; i < pauseButtons.Length; i++)
        {
            pauseButtons[i].gameObject.SetActive(false);
        }

        //Comment : ������ ��ư Ȱ��ȭ
        pauseButtons[curMenu_p].gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(inputDelay);
        settingCo = null;
    }


    //Comment : ������ �޴��� �����ϴ� �Լ�
    void SelectedEnter()
    {
        if (Input.GetButtonDown("Interaction"))
        {
            switch (curMenu_p)
            {
                case 0:
                    Debug.Log("���� �ٽ� ����");
                    ContinueButton();
                    break;

                case 1:
                    Debug.Log("�ɼ� ����_�ɼ� �˾� ����");
                    OptionButton();
                    break;

                case 2:
                    Debug.Log("���� ����");
                    ExitButton();
                    break;
            }
        }
    }

    public void ContinueButton()
    {
        pause.SetActive(false);
    }

    public void OptionButton()
    {
        ingameOptions.SetActive(true);
    }

    public void ExitButton()
    {
        exitPopUpObj_p.SetActive(true);
    }


    private void Init()
    {
        pause = GetUI("pause");

        pauseButtons = new Button[3];

        pauseButtons[0] = continueButton_P = GetUI<Button>("ContinueImage");
        pauseButtons[1] = optionButton_P = GetUI<Button>("OptionImage");
        pauseButtons[2] = exitButton_P = GetUI<Button>("ExitImage");

        exitPopUpObj_p = GetUI("ExitPopUp");

        exitButtons_p = new Button[2];
        exitButtons_p[0] = GetUI<Button>("Interaction");
        exitButtons_p[1] = GetUI<Button>("Negative");

    }

}
