using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public partial class OptionSetting
{
    // ������ ����
    // ȿ���� �����
    [Range(0, 100)]
    public float effectSound;
    [Range(0, 100)]
    public float backgroundSound;

    // ȭ�� �ӵ� ���� 1~5
    [Range(0.1f, 5)]
    public float cameraSpeed;

    // �̴ϸ� �¿��� ��� ����
    public bool minimapOn;
}
