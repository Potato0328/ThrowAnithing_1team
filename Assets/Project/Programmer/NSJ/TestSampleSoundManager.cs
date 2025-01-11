using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class TestSampleSoundManager : BaseUI
{
    Slider master => GetUI<Slider>("Master");
    Slider bgm => GetUI<Slider>("BGM");
    Slider sfx => GetUI<Slider>("SFX");
    Slider loopSfx => GetUI<Slider>("LoopSFX");


    [SerializeField] private AudioClip _sampleBGM;
    [SerializeField] private AudioClip _sampleSFX;

    private void Awake()
    {
        Bind();
    }

    private void Start()
    {
        // ������ ���� �����̴� �̺�Ʈ ����
        master.onValueChanged.AddListener((value) => SoundManager.SetVolumeMaster(value));
        SoundManager.SetVolumeMaster(master.value);
        // BGM ���� �����̴� �̺�Ʈ ����
        bgm.onValueChanged.AddListener((value) => SoundManager.SetVolumeBGM(value));
        SoundManager.SetVolumeBGM(bgm.value);
        // SFX ���� �����̴� �̺�Ʈ ����
        sfx.onValueChanged.AddListener((value) => SoundManager.SetVolumeSFX(value));
        SoundManager.SetVolumeSFX(sfx.value);
        // LoopSFX ���� �����̴� �̺�Ʈ ����
        master.onValueChanged.AddListener((value) => SoundManager.SetVolumeLoopSFX(value));
        SoundManager.SetVolumeLoopSFX(loopSfx.value);

    }

    private void Update()
    {
        Debug.Log(SoundManager.GetVolumeBGM());
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SoundManager.PlayBGM(_sampleBGM);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SoundManager.StopBGM();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SoundManager.PlaySFX(_sampleSFX);
        }
    }
}
