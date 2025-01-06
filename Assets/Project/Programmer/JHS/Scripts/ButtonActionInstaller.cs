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

    [SerializeField] public Button MinimapOn;
    [SerializeField] public Button MinimapOff;
    [SerializeField] public Button MiniMapFixOn;
    [SerializeField] public Button MiniMapFixOff;
    [Inject]
    public OptionSetting setting;

    private void Start()
    {
        saveButton.onClick.AddListener(setting.OptionSave);
        cancelButton.onClick.AddListener(setting.OptionLode);
        resetButton.onClick.AddListener(setting.OptionReset);
        MinimapOn.onClick.AddListener(setting.MinimapOn);
        MinimapOff.onClick.AddListener(setting.MinimapOff);
        MiniMapFixOn.onClick.AddListener(setting.MiniMapFixOn);
        MiniMapFixOff.onClick.AddListener(setting.MiniMapFixOff);
    }
}
