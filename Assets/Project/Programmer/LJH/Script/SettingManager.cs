using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    [Header("�̴ϸ� ����")]
    [SerializeField] bool miniMapActivated;
    [SerializeField] bool miniMapFixed;

    //Language ���� �ʿ�

    //
    [Header("���� ����")]
    [SerializeField] public AudioSource[] totalSoundSources;

    [SerializeField] public AudioSource bgmSource;

    [SerializeField] public AudioSource[] effectSources;

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Init()
    {

    }
}
