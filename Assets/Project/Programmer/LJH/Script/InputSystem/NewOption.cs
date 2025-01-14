using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class NewOption : BaseUI
{
    [Inject]
    OptionSetting setting;

    MainSceneBinding binding;

    // ���� ����
    // 0 = �ɼ�
    // 1 = �����÷���
    // 2 = �Ҹ�
    int curDepth;

    // ����1 ��ư ���
    Button gamePlayButton;
    Button soundButton;
    Button inputButton;
    Button exitButton;

    List<Button> optionButtons = new();

    //����2 ��ư ��� - �����÷���
    Button minimapAct;
    Button minimapFix;
    Button LanguageChange;
    Button sens;

    Button accept_Gameplay;
    Button cancel_Gameplay;
    Button default_Gameplay;

    List<Button> gameplayButtons = new();

    //�̴ϸ� ������ �Һ���
    bool preAct;
    bool newAct;
    bool defaultAct;

    bool preFix;
    bool newFix;
    bool defaultFix;

    float preSens;
    float newSens;
    float defaultSens;

    GameObject actChecked; //�̴ϸ� Ȱ��ȭ üũ ����
    GameObject fixChecked; //�̴ϸ�   ���� üũ ����
    GameObject actUnChecked;
    GameObject fixUnChecked;


    //����2 ��ư ��� - �Ҹ�
    Button totalVolume;
    Button bgmVolume;
    Button sfxVolume;

    Button accept_Sound;
    Button cancel_Sound;
    Button default_Sound;

    List<Button> soundButtons = new();

    //���� ������

    float preTotal;
    float newTotal;
    float defaultTotal;

    float preBgm;
    float newBgm;
    float defaultBgm;

    float preEffect;
    float newEffect;
    float defaultEffect;

    //�г�
    GameObject gameplayPannel;
    GameObject soundPannel;
    GameObject inputPannel;

    //�ڷ�ƾ
    Coroutine firstCo;

    [SerializeField] NewPause pausePanel;

    private void Awake()
    {
        Bind();
        Init();
    }

    private void OnEnable()
    {

        firstCo = null;
        //Todo : �ڿ������� ó���ؾ���
        binding.ButtonFirstSelect(gamePlayButton.gameObject);
        //���� ���õ� ��ư ���� ��, ù��° ��ư ����
        if (firstCo == null)
            firstCo = StartCoroutine(FirstRoutine());
    }

    private void OnDisable()
    {
        EventSystem.current.SetSelectedGameObject(null);

        if (pausePanel != null)
            EventSystem.current.SetSelectedGameObject(pausePanel.continueButton.gameObject);
        
    }

    void Start()
    {
        //�ɼ�â �������� ���� ��ư ��� 0���� ����
        curDepth = 0;

    }

    IEnumerator FirstRoutine()
    {
        while (true)
        {
            Debug.Log("ù��° ��ư ���õǴ���");
            binding.ButtonFirstSelect(gamePlayButton.gameObject);
            yield return 0.1f.GetDelay();
        }
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            return;


        DepthCal();
        curTabChecker(EventSystem.current.currentSelectedGameObject.GetComponent<Button>());
    }

    //���� ������ �°� ��ư ���̶���Ʈ
    void DepthCal()
    {
        switch (curDepth)
        {
            case 0:
                binding.SelectedButtonHighlight(optionButtons);
                break;

            case 1:
                binding.SelectedButtonHighlight(gameplayButtons);
                break;

            case 2:
                binding.SelectedButtonHighlight(soundButtons);
                break;
        }
    }
    /// <summary>
    /// ���� ��Ŀ�̵� �޴��� �°� �г� ü����
    /// </summary>
    /// <param name="curButton">���� ���õ� ��ư</param>
    void curTabChecker(Button curButton)
    {

        if (curDepth == 0)
        {
            if (curButton == gamePlayButton)
            {
                gameplayPannel.SetActive(true);
                soundPannel.SetActive(false);
                inputPannel.SetActive(false);
            }
            else if (curButton == soundButton)
            {
                gameplayPannel.SetActive(false);
                soundPannel.SetActive(true);
                inputPannel.SetActive(false);
            }
            else if (curButton == inputButton)
            {
                gameplayPannel.SetActive(false);
                soundPannel.SetActive(false);
                inputPannel.SetActive(true);
            }
        }
    }

    //�ڷ�ƾ�� ����� UI �ʱ�ȭ�� �ð� ������
    IEnumerator SetSelectRoutine(GameObject obj)
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(obj);
    }

    public void GamePlayButton()
    {
        StartCoroutine(SetSelectRoutine(minimapAct.gameObject));
        curDepth = 1;
        firstCo = null;
    }

    public void SoundButton()
    {
        StartCoroutine(SetSelectRoutine(totalVolume.gameObject));
        curDepth = 2;
        firstCo = null;
    }

    public void InputButton()
    {
        StartCoroutine(SetSelectRoutine(inputButton.gameObject));
        firstCo = null;
    }

    public void ExitButton()
    {
        firstCo = null;
        binding.CanvasChange(binding.mainCanvas.gameObject, gameObject);
        curDepth = 0;
    }

    public void ExitButton_Pause()
    {   firstCo = null;
        curDepth = 0;
        
        
        gameObject.SetActive(false);
    }

    public void MinimapAct()
    {
        actChecked.SetActive(!actChecked.activeSelf);
        StartCoroutine(SetSelectRoutine(minimapAct.gameObject));
        curDepth = 1;
    }

    public void MinimapFix()
    {
        fixChecked.SetActive(!fixChecked.activeSelf);
        StartCoroutine(SetSelectRoutine(minimapFix.gameObject));
        curDepth = 1;
    }

    /// <summary>
    /// ����, ���, �ʱ�ȭ ������ ��ư ���� �ʱ�ȭ
    /// </summary>
    /// <param name="list"></param>
    void ButtonReset(List<Button> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<TMP_Text>().color = Color.white;
        }
    }

    public void AcceptButton_Gameplay()
    {
        MinimapCheck();
        setting.miniMapOnBool = newAct;
        setting.miniMapFixBool = newFix;

        preAct = setting.miniMapOnBool;
        preFix = setting.miniMapFixBool;

        ButtonReset(gameplayButtons);

        EventSystem.current.SetSelectedGameObject(gamePlayButton.gameObject);


        curDepth = 0;
    }

    void MinimapCheck()
    {
        newAct = actChecked.activeSelf;
        newFix = fixChecked.activeSelf;
    }

    public void CancelButton_Gameplay()
    {
        setting.miniMapOnBool = preAct;
        setting.miniMapFixBool = preFix;

        ButtonReset(gameplayButtons);

        EventSystem.current.SetSelectedGameObject(gamePlayButton.gameObject);

        curDepth = 0;
    }

    public void DefaultButton_Gameplay()
    {
        setting.miniMapOnBool = defaultAct;
        setting.miniMapFixBool = defaultFix;

        preAct = setting.miniMapOnBool;
        preFix = setting.miniMapFixBool;

        ButtonReset(gameplayButtons);

        EventSystem.current.SetSelectedGameObject(gamePlayButton.gameObject);

        curDepth = 0;
    }

    public void AcceptButton_Sound()
    {
        //setting.wholesound = newTotal;
        //setting.backgroundSound = newBgm;
        //setting.effectSound = newEffect;

        //preTotal = setting.wholesound;
        //preBgm = setting.backgroundSound;
        //preEffect = setting.effectSound;

        ButtonReset(gameplayButtons);

        EventSystem.current.SetSelectedGameObject(soundButton.gameObject);
        curDepth = 0;
    }

    void VolumeCheck()
    {
        //newTotal = setting.wholesound;
        //newBgm = setting.backgroundSound;
        //newEffect = setting.effectSound;
    }

    public void CancelButton_Sound()
    {
        //setting.wholesound = preTotal;
        //setting.backgroundSound = preBgm;
        //setting.effectSound = preEffect;

        ButtonReset(soundButtons);

        EventSystem.current.SetSelectedGameObject(soundButton.gameObject);

        curDepth = 0;
    }

    public void DefaultButton_Sound()
    {
        //setting.wholesound = defaultTotal;
        //setting.backgroundSound = defaultBgm;
        //setting.effectSound = defaultEffect;

        ButtonReset(soundButtons);

        EventSystem.current.SetSelectedGameObject(soundButton.gameObject);

        curDepth = 0;
    }
    void Init()
    {
        binding = GetComponentInParent<MainSceneBinding>();

        optionButtons.Add(gamePlayButton = GetUI<Button>("GamePlay"));
        optionButtons.Add(soundButton = GetUI<Button>("Sound"));
        optionButtons.Add(inputButton = GetUI<Button>("Input"));
        optionButtons.Add(exitButton = GetUI<Button>("Exit"));


        gameplayPannel = GetUI("GamePlayPackage");
        soundPannel = GetUI("SoundPackage");
        inputPannel = GetUI("InputGuide");

        gameplayButtons.Add(minimapAct = GetUI<Button>("MinimapAct"));
        gameplayButtons.Add(minimapFix = GetUI<Button>("MinimapFix"));
        gameplayButtons.Add(LanguageChange = GetUI<Button>("Language"));
        gameplayButtons.Add(sens = GetUI<Button>("Sens"));
        gameplayButtons.Add(accept_Gameplay = GetUI<Button>("GamePlay_Accept"));
        gameplayButtons.Add(cancel_Gameplay = GetUI<Button>("GamePlay_Cancel"));
        gameplayButtons.Add(default_Gameplay = GetUI<Button>("GamePlay_Default"));

        actChecked = GetUI("ActChecked");
        fixChecked = GetUI("FixChecked");
        actUnChecked = GetUI("ActUnChecked");
        fixUnChecked = GetUI("FixUnChecked");

        soundButtons.Add(totalVolume = GetUI<Button>("TotalSound"));
        soundButtons.Add(bgmVolume = GetUI<Button>("BGMSound"));
        soundButtons.Add(sfxVolume = GetUI<Button>("SFXSound"));
        soundButtons.Add(accept_Sound = GetUI<Button>("Sound_Accept"));
        soundButtons.Add(cancel_Sound = GetUI<Button>("Sound_Cancel"));
        soundButtons.Add(default_Sound = GetUI<Button>("Sound_Default"));

        gamePlayButton.onClick.AddListener(GamePlayButton);
        soundButton.onClick.AddListener(SoundButton);
        inputButton.onClick.AddListener(InputButton);


        // ���ξ��� ���� ���� > �ɼ��� ��� ����
        if(SceneManager.GetActiveScene().name == SceneName.MainScene)
            exitButton.onClick.AddListener(ExitButton);
        else
            exitButton.onClick.AddListener(ExitButton_Pause);

        minimapAct.onClick.AddListener(MinimapAct);
        minimapFix.onClick.AddListener(MinimapFix);
        actChecked.GetComponent<Button>().onClick.AddListener(MinimapAct);
        fixChecked.GetComponent<Button>().onClick.AddListener(MinimapFix);
        actUnChecked.GetComponent<Button>().onClick.AddListener(MinimapAct);
        fixUnChecked.GetComponent<Button>().onClick.AddListener(MinimapFix);

        preAct = setting.miniMapOnBool;
        preFix = setting.miniMapFixBool;

        accept_Gameplay.onClick.AddListener(AcceptButton_Gameplay);
        cancel_Gameplay.onClick.AddListener(CancelButton_Gameplay);
        default_Gameplay.onClick.AddListener(DefaultButton_Gameplay);





        accept_Sound.onClick.AddListener(AcceptButton_Sound);
        cancel_Sound.onClick.AddListener(CancelButton_Sound);
        default_Sound.onClick.AddListener(DefaultButton_Sound);

    }
}
