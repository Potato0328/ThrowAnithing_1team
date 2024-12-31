using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CameraSpeedSlider : MonoBehaviour
{
    
    [SerializeField] private Slider cameraSpeedSlider;

    [Inject]
    private OptionSetting setting;

    private void Start()
    {
        // �ʱⰪ ���� 
        cameraSpeedSlider = GetComponent<Slider>();

        // json���� ��ܿ� �ӵ� ������ ����
        cameraSpeedSlider.value = setting.cameraSpeed;
    }
}
