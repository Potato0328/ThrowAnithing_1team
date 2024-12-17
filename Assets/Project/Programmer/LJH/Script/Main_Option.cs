using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Main_Option : MainScene
{
    //�ɼ� 1Depth
    GameObject[] depth1;

    GameObject gamePlay;
    GameObject language;
    GameObject sound;
    GameObject input;
    GameObject exit;


    GameObject inputImage;

    int depth1_cur;

    void Start()
    {
        Init();
    }


    private void Update()
    {
        OptionTitle();
        Depth1_Select();
        SelectedEnter();
    }

    void OptionTitle()
    {
        GameObject optionTitle = GetUI("optionTitle");

        switch (depth1_cur)
        {
            case 0:
                optionTitle.GetComponent<TMP_Text>().text = "MiniMap";
                break;

            case 1:
                optionTitle.GetComponent<TMP_Text>().text = "Language";
                break;

            case 2:
                optionTitle.GetComponent<TMP_Text>().text = "Sound";
                break;

            case 3:
                optionTitle.GetComponent<TMP_Text>().text = "Input";
                break;

            case 4:
                optionTitle.GetComponent<TMP_Text>().text = "";
                break;
        }

        if (depth1_cur == 3)
            inputImage.SetActive(true);
        else
            inputImage.SetActive(false);

    }

    private void Depth1_Select()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            depth1[depth1_cur].GetComponent<TMP_Text>().color = Color.white;

            if (depth1_cur == depth1.Length-1)
            {
                depth1_cur = 0;
                depth1[depth1_cur].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
                return;
            }

            depth1_cur++;
            depth1[depth1_cur].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            depth1[depth1_cur].GetComponent<TMP_Text>().color = Color.white;

            if (depth1_cur == 0)
            {
                depth1_cur = depth1.Length - 1;
                depth1[depth1_cur].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
                return;
            }

            depth1_cur--;
            depth1[depth1_cur].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
        }
    }

    void SelectedEnter()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (depth1_cur)
            {
                case 0:
                    Debug.Log("�����÷��� ����");
                    //Todo : ���� ���� �˾� �������� > �鿣��� ���� �ʿ��� ��
                    break;

                case 1:
                    Debug.Log("��� ����");
                    //Todo : ���� ȭ������ �̵� ��������
                    break;

                case 2:
                    Debug.Log("�Ҹ� ����");
                    //Todo : �ɼ� �˾� ��������
                    break;

                case 3:
                    Debug.Log("���� Ű ���� �̹��� ����");
                    //Todo : ����Ű ����
                    break;

                case 4:
                    Debug.Log("��� ȭ�� ������");
                    gameObject.SetActive(false);
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            Debug.Log("�ɼ� ȭ�� ������");
        }
    }

    private void Init()
    {
        depth1 = new GameObject[5];

        depth1[0] = gamePlay = GetUI("GamePlay");
        depth1[1] = language = GetUI("Language");
        depth1[2] = sound = GetUI("Sound");
        depth1[3] = input = GetUI("Input");
        depth1[4] = exit = GetUI("Exit");

        inputImage = GetUI("InputImage");

        depth1_cur = 0;
    }
}
