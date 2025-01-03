using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[System.Serializable]
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

    public void Awake()
    {
        OptionLode();
    }

    // �ɼ� ���� ��ġ ���̺�
    public void OptionSave()
    {
        PlayerPrefs.SetFloat("EffectSound", effectSound);
        PlayerPrefs.SetFloat("BackgroundSound", backgroundSound);
        PlayerPrefs.SetFloat("WholeSound", wholesound);
        PlayerPrefs.SetFloat("CameraSpeed", cameraSpeed);
        PlayerPrefs.SetInt("MiniMapOn", miniMapOn);
        PlayerPrefs.SetInt("MiniMapFix", miniMapFix);
        Debug.Log("�ɼ� ���� ����");
    }

    // �ɼ� ���� ��ġ �ε�
    public void OptionLode()
    {
        if (!PlayerPrefs.HasKey("EffectSound") || !PlayerPrefs.HasKey("BackgroundSound") || !PlayerPrefs.HasKey("CameraSpeed") || !PlayerPrefs.HasKey("miniMapOn") 
            || !PlayerPrefs.HasKey("WholeSound") || !PlayerPrefs.HasKey("MiniMapFix"))
        {
            Debug.Log("�⺻ ���� �Ϸ�");
            effectSound = 100;
            backgroundSound = 100;
            wholesound = 100;
            cameraSpeed = 5;
            miniMapOn = 1;
            miniMapFix = 1;
            return;
        }
        Debug.Log("�ɼ� ���� �ҷ�����");
        effectSound = PlayerPrefs.GetFloat("EffectSound");
        backgroundSound = PlayerPrefs.GetFloat("BackgroundSound");
        wholesound = PlayerPrefs.GetFloat("WholeSound");
        cameraSpeed = PlayerPrefs.GetFloat("CameraSpeed");
        miniMapOn = PlayerPrefs.GetInt("MiniMapOn");
        miniMapFix = PlayerPrefs.GetInt("MiniMapFix");
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
