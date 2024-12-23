using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalEffect : ScriptableObject
{
    public AdditionalEffect Origin;
    public enum Type { Throw, Hit, Player }

    public Type AdditionalType;

    [Space(20)]
    public string Name;
    [TextArea]
    public string Description;

    protected bool _isTriggerFirst;

    /// <summary>
    /// ���� �� ȣ��
    /// </summary>
    public virtual void Enter() { }
    /// <summary>
    /// ���� �� ȣ��
    /// </summary>
    public virtual void Exit() { }
    /// <summary>
    /// �����Ӹ��� ȣ��
    /// </summary>
    public virtual void Update() { }
    /// <summary>
    /// 0.02�ʸ��� ȣ��
    /// </summary>
    public virtual void FixedUpdate() { }
    /// <summary>
    /// Ʈ���� ���� ���� ȣ��
    /// </summary>
    public virtual void Trigger() { }
    /// <summary>
    /// Ʈ���� ���� �� ù��°�� ȣ��
    /// </summary>
    public virtual void TriggerFirst() { }

}
