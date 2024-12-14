using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : BaseUI
{
    GameObject continueImage;
    GameObject newImage;
    GameObject optionImage;
    GameObject exitImage;

    //�޴� ��ư �迭
    GameObject[] menuButtons;

    //�޴� �����ϴ� �迭�� �ε���(���� ���õ� �޴�)
    int curMenu;
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
        MenuSelect();
        SelectedEnter();
    }

    // Comment : �ʱ�ȭ �뵵�� �Լ�
    void Init()
    {
        menuButtons = new GameObject[4];
        menuButtons[0] = continueImage = GetUI("ContinueImage");
        menuButtons[1] = newImage = GetUI("NewImage");
        menuButtons[2] = optionImage = GetUI("OptionImage");
        menuButtons[3] = exitImage = GetUI("ExitImage");

        curMenu = 0;
    }

    // Comment : W/S �Ǵ� ��/�Ʒ� ȭ��ǥ Ű�� �̿��� �޴� ���� ���� �Լ�
    // Todo : �� ȿ������ ����� �ִ��� �����غ���
    private void MenuSelect()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            menuButtons[curMenu].gameObject.SetActive(false);

            if (curMenu == 3)
            {
                curMenu = 0;
                menuButtons[curMenu].gameObject.SetActive(true);
                return;
            }

            curMenu++;
            menuButtons[curMenu].gameObject.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            menuButtons[curMenu].gameObject.SetActive(false);

            if (curMenu == 0)
            {
                curMenu = 3;
                menuButtons[curMenu].gameObject.SetActive(true);
                return;
            }

            curMenu--;
            menuButtons[curMenu].gameObject.SetActive(true);
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
                    break;

                case 1:
                    Debug.Log("�����ϱ� ����_���� ���� ȭ������ �̵�");
                    //Todo : ���� ȭ������ �̵� ��������
                    break;

                case 2:
                    Debug.Log("�ɼ� ����_�ɼ� �˾� ����");
                    //Todo : �ɼ� �˾� ��������
                    break;

                case 3:
                    Debug.Log("���� ���� ����_���� ����");
                    //Todo : �������� ��������
                    break;
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
            Debug.Log("���� ���� ����_���� ����");
        //Todo : �������� ��������
    }

}
