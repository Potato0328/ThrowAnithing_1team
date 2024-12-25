using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyState
{
    [Range(50, 3000)] public int MaxHp;  // ü��
    [Range(0, 20)] public int Atk;       // ���ݷ�
    [Range(0, 10)] public float Def;    // ����
    [Range(0, 10)] public float Speed;    // �̵� �ӵ�
    [Range(0, 10)] public float AtkDelay;   // ���� �ӵ�
    [Range(0, 10)] public float AttackDis;  // ���� ��Ÿ�
    [Range(0, 10)] public float TraceDis;   // �ν� ��Ÿ�
}

[System.Serializable]
public class EliteEnemyState
{

}

[System.Serializable]
public class BossEnemyState
{
    
}
