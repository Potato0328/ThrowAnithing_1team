using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[System.Serializable]
public class OptionSetting : MonoBehaviour
{
    // ������ ����
    // ȿ���� �����
    [Range(0, 100)]
    public float effectSound;
    [Range(0, 100)]
    public float backgroundSound;

    // ȭ�� �ӵ� ���� 1~5
    [Range(0.1f, 5f)]
    public float cameraSpeed;

    // �̴ϸ� �¿��� ��� ���� 1 = on , 0 = off
    public int miniMapOn;

    public void Awake()
    {
        OptionLode();
    }

    // �ɼ� ���� ��ġ ���̺�
    public void OptionSave()
    {
        PlayerPrefs.SetFloat("EffectSound", effectSound);
        PlayerPrefs.SetFloat("BackgroundSound", backgroundSound);
        PlayerPrefs.SetFloat("CameraSpeed", cameraSpeed);
        PlayerPrefs.SetInt("MiniMapOn", miniMapOn);
        Debug.Log("�ɼ� ���� ����");
    }

    // �ɼ� ���� ��ġ �ε�
    public void OptionLode()
    {
        if (!PlayerPrefs.HasKey("EffectSound") || !PlayerPrefs.HasKey("BackgroundSound") || !PlayerPrefs.HasKey("CameraSpeed") || !PlayerPrefs.HasKey("miniMapOn"))
        {
            Debug.Log("�⺻ ���� �Ϸ�");
            effectSound = 100;
            backgroundSound = 100;
            cameraSpeed = 5;
            miniMapOn = 1;
            return;
        }
        Debug.Log("�ɼ� ���� �ҷ�����");
        effectSound = PlayerPrefs.GetFloat("EffectSound");
        backgroundSound = PlayerPrefs.GetFloat("BackgroundSound");
        cameraSpeed = PlayerPrefs.GetFloat("CameraSpeed");
        miniMapOn = PlayerPrefs.GetInt("MiniMapOn");
    }
}
