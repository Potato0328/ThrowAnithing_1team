using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CameraSpeedSlider : MonoBehaviour
{
    [SerializeField, Range(0.1f, 5f)]
    public Slider cameraSpeedSlider;

    [Inject]
    public OptionSetting setting;

    private void Start()
    {
        cameraSpeedSlider = GetComponent<Slider>();

        cameraSpeedSlider.minValue = 0.1f;
        cameraSpeedSlider.maxValue = 5f;
        cameraSpeedSlider.value = Mathf.Clamp(setting.cameraSpeed, cameraSpeedSlider.minValue, cameraSpeedSlider.maxValue);

        // �����̴� �� ���� �� SettingCameraSpeed �޼��� ȣ��
        cameraSpeedSlider.onValueChanged.AddListener(SettingCameraSpeed);
    }
    public void SettingCameraSpeed(float value)
    {
        setting.cameraSpeed = value;
    }

    private void OnDestroy()
    {
        // �̺�Ʈ ������ ����
        cameraSpeedSlider.onValueChanged.RemoveListener(SettingCameraSpeed);
    }
}
