using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CameraSpeedSlider : MonoBehaviour
{
    [Range(0.1f, 5f)]
    [SerializeField] private Slider cameraSpeedSlider;

    [Inject]
    private OptionSetting setting;

    private void Start()
    {
        cameraSpeedSlider = GetComponent<Slider>();

        // json���� ��ܿ� �ӵ� ������ ����
        cameraSpeedSlider.value = setting.cameraSpeed;

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
