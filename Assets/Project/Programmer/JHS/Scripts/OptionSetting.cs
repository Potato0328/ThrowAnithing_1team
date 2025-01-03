using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OptionSetting : MonoBehaviour
{
    // ������ ����
    // ȿ���� ����� ��ü����
    [Range(0, 100)]
    public float effectSound;
    [Range(0, 100)]
    public float backgroundSound;
    [Range(0, 100)]
    public float wholesound;

    // ȭ�� �ӵ� ���� 1~5
    [Range(0.1f, 5f)]
    public float cameraSpeed;

    // �̴ϸ� �¿��� ��� ���� 1 = on , 0 = off
    public int miniMapOn;
    // �̴ϸ� ���� ��� ���� 1 = on , 0 = off
    public int miniMapFix;

    private const string EffectSoundKey = "Option_EffectSound";
    private const string BackgroundSoundKey = "Option_BackgroundSound";
    private const string WholeSoundKey = "Option_WholeSound";
    private const string CameraSpeedKey = "Option_CameraSpeed";
    private const string MiniMapOnKey = "Option_MiniMapOn";
    private const string MiniMapFixKey = "Option_MiniMapFix";

    public void Start()
    {
        OptionLode();
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs �ʱ�ȭ �Ϸ�");
    }

    // �ɼ� ���� ��ġ ���̺�
    public void OptionSave()
    {      
        PlayerPrefs.SetFloat(EffectSoundKey, effectSound);
        PlayerPrefs.SetFloat(BackgroundSoundKey, backgroundSound);
        PlayerPrefs.SetFloat(WholeSoundKey, wholesound);
        PlayerPrefs.SetFloat(CameraSpeedKey, cameraSpeed);
        PlayerPrefs.SetInt(MiniMapOnKey, miniMapOn);
        PlayerPrefs.SetInt(MiniMapFixKey, miniMapFix);
        Debug.Log($"After OptionSave - EffectSound: {effectSound}, BackgroundSound: {backgroundSound}, WholeSound: {wholesound}, CameraSpeed: {cameraSpeed}, MiniMapOn: {miniMapOn}, MiniMapFix: {miniMapFix}");
        PlayerPrefs.Save();
        Debug.Log("�ɼ� ���� ����");
    }

    // �ɼ� ���� ��ġ �ε�
    public void OptionLode()
    {
        if (!PlayerPrefs.HasKey(EffectSoundKey) || !PlayerPrefs.HasKey(BackgroundSoundKey) || !PlayerPrefs.HasKey(WholeSoundKey) || !PlayerPrefs.HasKey(CameraSpeedKey) 
            || !PlayerPrefs.HasKey(MiniMapOnKey) || !PlayerPrefs.HasKey(MiniMapFixKey))
        {
            Debug.Log("�⺻ ���� �Ϸ�");
            effectSound = 100;
            backgroundSound = 100;
            wholesound = 100;
            cameraSpeed = 5;
            miniMapOn = 1;
            miniMapFix = 1;
            OptionSave();
            return;
        }
        Debug.Log("�ɼ� ���� �ҷ�����");       
        effectSound = PlayerPrefs.GetFloat(EffectSoundKey);
        backgroundSound = PlayerPrefs.GetFloat(BackgroundSoundKey);
        wholesound = PlayerPrefs.GetFloat(WholeSoundKey);
        cameraSpeed = PlayerPrefs.GetFloat(CameraSpeedKey);
        miniMapOn = PlayerPrefs.GetInt(MiniMapOnKey);
        miniMapFix = PlayerPrefs.GetInt(MiniMapFixKey);
        Debug.Log($"After OptionLode - EffectSound: {effectSound}, BackgroundSound: {backgroundSound}, WholeSound: {wholesound}, CameraSpeed: {cameraSpeed}, MiniMapOn: {miniMapOn}, MiniMapFix: {miniMapFix}");
    }

    public void OptionReset()
    {
        effectSound = 100;
        backgroundSound = 100;
        wholesound = 100;
        cameraSpeed = 5;
        miniMapOn = 1;
        miniMapFix = 1;       
        OptionSave();
    }
}
