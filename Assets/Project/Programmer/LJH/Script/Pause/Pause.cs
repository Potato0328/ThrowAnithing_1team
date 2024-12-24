using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Rendering.InspectorCurveEditor;

public class Pause : MainScene
{
    GameObject pause;


    GameObject continueButton_P;
    GameObject optionButton_P;
    GameObject exitButton_P;

    [SerializeField] GameObject ingameOptions;

    int curMenu_p;

    GameObject[] pauseButtons;
    GameObject[] exitButtons_p;

    Coroutine settingCo;

    int exitNum_p;

    GameObject exitPopUpObj_p;
    GameObject exitNo_p;

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

        if (curMenu_p == pauseButtons.Length)
        {
            curMenu_p = 0;
            pauseButtons[pauseButtons.Length - 1].SetActive(false);
            pauseButtons[curMenu_p].SetActive(true);
            yield return null;
        }

        if (curMenu_p == -1)
        {
            curMenu_p = pauseButtons.Length - 1;
            pauseButtons[0].SetActive(false);
            pauseButtons[curMenu_p].SetActive(true);
            yield return null;
        }

        for (int i = 0; i < pauseButtons.Length; i++)
        {
            pauseButtons[i].SetActive(false);
        }
        pauseButtons[curMenu_p].SetActive(true);

        yield return new WaitForSecondsRealtime(inputDelay);
        settingCo = null;
    }


    //Comment : ������ �޴��� �����ϴ� �Լ�
    void SelectedEnter()
    {
        // Todo: �е���� ���� �����ϰ� �ٲ����
        if (Input.GetButtonDown("Interaction"))
        {
            switch (curMenu_p)
            {
                case 0:
                    Debug.Log("���� �ٽ� ����");
                    pause.SetActive(false);
                    break;

                case 1:
                    Debug.Log("�ɼ� ����_�ɼ� �˾� ����");
                    ingameOptions.SetActive(true);
                    break;

                case 2:
                    Debug.Log("���� ����");
                    ExitGame();
                    break;
            }
        }
    }

    void ExitPopUp()
    {

        switch (exitNum_p)
        {
            case 0:
                exitButtons_p[exitNum_p].GetComponent<Image>().color = Color.black;
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    exitButtons_p[exitNum_p].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                    exitNum_p = 1;
                }
                if (Input.GetButtonDown("Interaction"))
                    ExitGame();
                break;

            case 1:
                exitNo_p.GetComponent<Image>().color = Color.black;
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    exitNo_p.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
                    exitNum_p = 0;
                }
                if (Input.GetButtonDown("Interaction"))
                    exitPopUpObj_p.SetActive(false);
                break;

        }
    }
    void ExitGame()
    {
#if UNITY_EDITOR
        //Comment : ����Ƽ �����ͻ󿡼� ����
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //Comment : ���� �󿡼� ����
        Application.Quit();
#endif
    }

    private void Init()
    {
        pause = GetUI("pause");

        pauseButtons = new GameObject[3];

        pauseButtons[0] = continueButton_P = GetUI("ContinueImage");
        pauseButtons[1] = optionButton_P = GetUI("OptionImage");
        pauseButtons[2] = exitButton_P = GetUI("ExitImage");

    }

}
