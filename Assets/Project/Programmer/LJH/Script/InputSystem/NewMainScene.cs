using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewMainScene : BaseUI
{
    MainSceneBinding binding;
    
    //��ư��
    Button continueButton;
    Button newButton;
    Button optionButton;
    Button exitButton;


    private void Awake()
    {
        Bind();
        Init();
    }
    private void OnEnable()
    {
    }
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
    }

    void Update()
    {
        binding.ButtonFirstSelect(continueButton.gameObject);
    }


    public void EnterContinue()
    {
        binding.CanvasChange(binding.continueCanvas.gameObject, gameObject);
    }

    public void EnterNew()
    {
        binding.CanvasChange(binding.newCanvas.gameObject, gameObject);
    }

    public void EnterOption()
    {
        binding.CanvasChange(binding.optionCanvas.gameObject, gameObject);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        //Comment : ����Ƽ �����ͻ󿡼� ����
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //Comment : ���� �󿡼� ����
        Application.Quit();
#endif
    }
    void Init()
    {
        binding = GetComponentInParent<MainSceneBinding>();



        //��ư ���ε� �� ��ư ����
        continueButton = GetUI<Button>("Continue");
        newButton = GetUI<Button>("New");
        optionButton = GetUI<Button>("Option");
        exitButton = GetUI<Button>("Exit");

        //��ư ����
        continueButton.onClick.AddListener(EnterContinue);
        newButton.onClick.AddListener(EnterNew);
        optionButton.onClick.AddListener(EnterOption);
        exitButton.onClick.AddListener(ExitGame);

    }
}
