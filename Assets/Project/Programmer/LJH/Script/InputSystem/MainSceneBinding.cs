using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class MainSceneBinding : BaseUI
{
    //ĵ������
    /*[SerializeField]*/
    public Canvas mainCanvas;
    /*[SerializeField]*/
    public Canvas continueCanvas;
    /*[SerializeField]*/
    public Canvas newCanvas;
    /*[SerializeField]*/
    public Canvas optionCanvas;


    private void Awake()
    {
        Bind();
        Init();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //ĵ���� ��ü �Լ�
    public void CanvasChange(GameObject loadCanvas, GameObject curCanvas)
    {

        loadCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        curCanvas.SetActive(false);
    }

    //���õ� ��ư ���� ����
    public void SelectedButtonHighlight(List<Button> buttons)
    {
        if (EventSystem.current.currentSelectedGameObject != null)
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].GetComponent<TMP_Text>().color = Color.white;

                if (buttons[i] == EventSystem.current.currentSelectedGameObject.GetComponent<Button>())
                {
                    buttons[i].GetComponent<TMP_Text>().color = new Color(1, 0.5f, 0);
                }
            }
    }
    /// <summary>
    /// ���õ� ���� ���� ����
    /// </summary>
    /// <param name ="slots">���� ���� ��ư ����Ʈ</param>
    public void SelectedSlotHighlight(List<Button> slots)
    {
        if (EventSystem.current.currentSelectedGameObject != null)
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].GetComponent<Image>().color = new Color(1, 0.5f, 0, 0.1f);

                if (slots[i] == EventSystem.current.currentSelectedGameObject.GetComponent<Image>())
                    slots[i].GetComponent<Image>().color = new Color(1, 0.5f, 0, 1);
            }
    }

    // ������ ���Խ� �ʱ� ��ư ����
    public void ButtonFirstSelect(GameObject firstButton)
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject);
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(firstButton);
        }

        // if(!EventSystem.current.currentSelectedGameObject.activeSelf)
        // {
        //     EventSystem.current.SetSelectedGameObject(firstButton);
        // }
    }

    void Init()
    {
        //ĵ���� ���ε�
        mainCanvas = GetUI<Canvas>("MainCanvas");
        continueCanvas = GetUI<Canvas>("ContinueCanvas");
        newCanvas = GetUI<Canvas>("NewCanvas");
        optionCanvas = GetUI<Canvas>("OptionCanvas");


    }
}
