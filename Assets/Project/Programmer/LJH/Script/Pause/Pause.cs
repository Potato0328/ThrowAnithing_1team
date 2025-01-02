using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pause : Main_Option
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
        Init();
    }
    private void OnEnable()
    {
        StartCoroutine(MenuSelect());
    }
    private void Update()
    {
        if (pause.activeSelf)
        {
            if (InputKey.GetButtonDown(InputKey.Interaction))
                SelectedEnter();

            //Time.timeScale = 0;

        }
        else if (!pause.activeSelf)
        {
            //Time.timeScale = 1f;

        }
    }



    private IEnumerator MenuSelect()
    {
        while (true)
        {
            float y = -InputKey.GetAxisRaw(InputKey.Vertical);
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

            if (y == 0)
                yield return null;
            else
                yield return inputDelay.GetRealTimeDelay();
        }
    }


    //Comment : ������ �޴��� �����ϴ� �Լ�
    void SelectedEnter()
    {
        if (InputKey.GetButtonDown(InputKey.Interaction))
        {
            switch (curMenu_p)
            {
                case 0:
                    Debug.Log("���� �ٽ� ����");
                    ContinueButton();
                    break;

                case 1:
                    Debug.Log("�ɼ� ����_�ɼ� �˾� ����");
                    //OptionButton();
                    Panel.ChangeBundle(PausePanel.Bundle.Option);
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
        Panel.ChangeBundle(PausePanel.Bundle.None);
    }

    public void OptionButton()
    {
        Panel.ChangeBundle(PausePanel.Bundle.Option);
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
