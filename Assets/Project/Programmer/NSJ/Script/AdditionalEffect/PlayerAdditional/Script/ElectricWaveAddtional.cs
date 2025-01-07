using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElectricWave", menuName = "AdditionalEffect/Player/ElectricWave")]
public class ElectricWaveAddtional : PlayerAdditional
{
    [Header("�ߵ� �ð� ����")]
    [SerializeField] private float _intervalTime;
    [Header("������")]
    [SerializeField] private int _damage;
    [Header("����")]
    [SerializeField] private float _range;
}
