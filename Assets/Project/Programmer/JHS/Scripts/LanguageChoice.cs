using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LanguageChoce : MonoBehaviour
{
    [SerializeField] public TMP_Dropdown langaugeDropdown;

    [Inject]
    public OptionSetting setting;

    private void Awake()
    {
        // langaugeDropdown ���� ����� �� �̺�Ʈ ����
        langaugeDropdown.onValueChanged.AddListener(ChangeLanguage);
    }

    private void ChangeLanguage(int value)
    {
        // langaugeDropdown.value ���� setting.language�� ����
        setting.language = value;
    }

    private void OnDisable()
    {
        // �̺�Ʈ ���� ����
        langaugeDropdown.onValueChanged.RemoveListener(ChangeLanguage);
    }
}
