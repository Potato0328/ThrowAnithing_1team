using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ButtonActionInstaller : MonoBehaviour
{
    // �켱 ��ư�� �����ͼ� �� ��ư�� ��ɾ �־��ִµ�
    // ���� ������ ������Ʈ ���ؽ�Ʈ�� OptionSetting��  OptionSave() OptionReset()�� �־�����Ѵ�

    // OptionSave()
    [SerializeField] public Button saveButton;
    // OptionLode()
    [SerializeField] public Button cancelButton;
    // OptionReset()
    [SerializeField] public Button resetButton;

    [Inject]
    public OptionSetting setting;

    private void Start()
    {
        saveButton.onClick.AddListener(setting.OptionSave);
        cancelButton.onClick.AddListener(setting.OptionLode);
        resetButton.onClick.AddListener(setting.OptionReset);
    }
}
