using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour, IBattle
{
    public IHit Hit { get; set; }
    public IDebuff Debuff { get; set; }

    [SerializeField] private Transform _hitTextPoint;

    [SerializeField] private List<HitAdditional> _hitAdditionalList;
    [SerializeField] private List<HitAdditional> _debuffList;



    private void Awake()
    {
        Hit = GetComponent<IHit>();
        Debuff = GetComponent<IDebuff>();

        if (_hitTextPoint == null)
        {
            _hitTextPoint = new GameObject("HitTextPoint").transform;
            _hitTextPoint.SetParent(transform, true);
            _hitTextPoint.localPosition = new Vector3(0, 1f, 0);
        }
    }
    #region ���� �޼���
    /// <summary>
    /// ���� ��� ������� �ִ� ����
    /// </summary>
    public void TargetDebuff<T>(T target) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeDebuff(_hitAdditionalList);
    }
    /// <summary>
    /// Ư�� ������� �ִ� ����
    /// </summary>
    public void TargetDebuff<T>(T target, HitAdditional debuff) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeDebuff(debuff);
    }
    /// <summary>
    /// Ư�� ������鸸 �ִ� ����
    /// </summary>
    public void TargetDebuff<T>(T target, List<HitAdditional> debuffs) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeDebuff(debuffs);
    }
    /// <summary>
    /// ����� ���ִ� ����
    /// </summary>
    public void TargetAttack<T>(T target, int damage, bool isStun) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeDamage(damage, isStun); // ��븦 ����
    }
    /// <summary>
    /// ����� ���ִ� ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public void TargetAttack<T>(T target, int damage, bool isStun, bool isCritical) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeDamage(damage, isStun, isCritical); // ��븦 ����
    }
    /// <summary>
    /// ����� ���ִ� ���� (Ÿ��)
    /// </summary>
    public void TargetAttack<T>(T target, int damage, bool isStun , DamageType type) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeDamage(damage, isStun, type); // ��븦 ����
    }
    /// <summary>
    /// ����� ���ִ� ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public void TargetAttack<T>(T target, int damage, bool isStun, DamageType type, bool isCritical) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeDamage(damage, isStun, type, isCritical); // ��븦 ����
    }
    /// <summary>
    /// ���� ��� ����� �ָ鼭 ����
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeDamageWithDebuff(damage, isStun, _hitAdditionalList); // ��븦 ����
    }
    /// <summary>
    /// ���� ��� ����� �ָ鼭 ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeDamageWithDebuff(damage, isStun, _hitAdditionalList, isCritical); // ��븦 ����
    }
    /// <summary>
    /// ���� ��� ����� �ָ鼭 ���� (Ÿ��)
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, DamageType type) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeDamageWithDebuff(damage, isStun, _hitAdditionalList, type); // ��븦 ����
    }
    /// <summary>
    /// ���� ��� ����� �ָ鼭 ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, DamageType type, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeDamageWithDebuff(damage, isStun, _hitAdditionalList, type, isCritical); // ��븦 ����
    }
    /// <summary>
    /// Ư�� ������� �ָ鼭 ����
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, HitAdditional debuff) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeDamageWithDebuff(damage, isStun, debuff); // ��븦 ����
    }
    /// <summary>
    /// Ư�� ������� �ָ鼭 ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, HitAdditional debuff, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeDamageWithDebuff(damage, isStun, debuff, isCritical); // ��븦 ����
    }
    /// <summary>
    /// Ư�� ������� �ָ鼭 ���� (Ÿ��)
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, HitAdditional debuff, DamageType type) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeDamageWithDebuff(damage, isStun, debuff, type); // ��븦 ����
    }
    /// <summary>
    /// Ư�� ������� �ָ鼭 ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, HitAdditional debuff, DamageType type, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeDamageWithDebuff(damage, isStun, debuff, type, isCritical); // ��븦 ����
    }
    /// <summary>
    /// Ư�� ��������� �ָ鼭 ���� ����
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, List<HitAdditional> debuffs) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeDamageWithDebuff(damage, isStun, debuffs); // ��븦 ����
    }
    /// <summary>
    /// Ư�� ��������� �ָ鼭 ���� ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, List<HitAdditional> debuffs, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeDamageWithDebuff(damage, isStun, debuffs, isCritical); // ��븦 ����
    }
    /// <summary>
    /// Ư�� ��������� �ָ鼭 ���� ���� (Ÿ��)
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, List<HitAdditional> debuffs, DamageType type) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeDamageWithDebuff(damage, isStun, debuffs, type); // ��븦 ����
    }
    /// <summary>
    /// Ư�� ��������� �ָ鼭 ���� ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public void TargetAttackWithDebuff<T>(T target, int damage, bool isStun, List<HitAdditional> debuffs, DamageType type, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeDamageWithDebuff(damage, isStun, debuffs, type, isCritical); // ��븦 ����
    }
    #endregion
    #region �ǰ� �޼���
    /// <summary>
    /// �ȶ����� Ư�� ������� �ֱ�
    /// </summary>
    public void TakeDebuff(HitAdditional debuff)
    {
        AddDebuff(debuff);
    }
    /// <summary>
    /// �ȶ����� Ư�� ������鸸 �ֱ�
    /// </summary>
    public void TakeDebuff(List<HitAdditional> debuffs)
    {
        // ����� �߰�
        foreach (HitAdditional debuff in debuffs)
        {
            AddDebuff(debuff);
        }
    }

    /// <summary>
    /// ����� ���ִ� ���� �±�
    /// </summary>
    public void TakeDamage(int damage, bool isStun)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage);
    }
    /// <summary>
    /// ����� ���ִ� ���� �±� (Ÿ��, ġ��Ÿ üũ)
    /// </summary>
    public void TakeDamage(int damage, bool isStun, bool isCritical)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, isCritical);
    }
    /// <summary>
    /// ����� ���ִ� ���� �±� (Ÿ��)
    /// </summary>
    public void TakeDamage(int damage, bool isStun, DamageType type)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage,type);
    }
    /// <summary>
    /// ����� ���ִ� ���� �±� (Ÿ��, ġ��Ÿ üũ)
    /// </summary>
    public void TakeDamage(int damage, bool isStun, DamageType type, bool isCritical)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, type, isCritical);
    }
    /// <summary>
    /// ���ݹ����鼭 ����� ���� �ޱ�
    /// </summary>
    public void TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> debuffs)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage);
        // ����� �߰�
        foreach (HitAdditional hitAdditional in debuffs) 
        {
            AddDebuff(hitAdditional);
        }
    }
    /// <summary>
    /// ���ݹ����鼭 ����� ���� �ޱ� (Ÿ��)
    /// </summary>
    public void TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> debuffs, bool isCritical)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, isCritical);
        // ����� �߰�
        foreach (HitAdditional hitAdditional in debuffs)
        {
            AddDebuff(hitAdditional);
        }
    }
    /// <summary>
    /// ���ݹ����鼭 ����� ���� �ޱ� (Ÿ��)
    /// </summary>
    public void TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> debuffs, DamageType type)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, type);
        // ����� �߰�
        foreach (HitAdditional hitAdditional in debuffs)
        {
            AddDebuff(hitAdditional);
        }
    }
    /// <summary>
    /// ���ݹ����鼭 ����� ���� �ޱ� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public void TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> debuffs, DamageType type, bool isCritical)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, type, isCritical);
        // ����� �߰�
        foreach (HitAdditional hitAdditional in debuffs)
        {
            AddDebuff(hitAdditional);
        }
    }
    /// <summary>
    /// Ư�� ������� �ޱ�
    /// </summary>
    public void TakeDamageWithDebuff(int damage, bool isStun, HitAdditional debuff)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage);

        AddDebuff(debuff);
    }
    /// <summary>
    /// Ư�� ������� �ޱ� (ġ��Ÿ)
    /// </summary>
    public void TakeDamageWithDebuff(int damage, bool isStun, HitAdditional debuff, bool isCritical)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, isCritical);

        AddDebuff(debuff);
    }
    /// <summary>
    /// Ư�� ������� �ޱ� (Ÿ��)
    /// </summary>
    public void TakeDamageWithDebuff(int damage, bool isStun, HitAdditional debuff, DamageType type)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage,type);

        AddDebuff(debuff);
    }
    /// <summary>
    /// Ư�� ������� �ޱ� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public void TakeDamageWithDebuff(int damage, bool isStun, HitAdditional debuff, DamageType type, bool isCritical)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, type, isCritical);

        AddDebuff(debuff);
    }
    #endregion
    #region ������ UI 
    /// <summary>
    /// ������ UI ����
    /// </summary>
    private void CreateDamageText(int damage)
    {
        if (gameObject.tag == Tag.Player)
            return;

        DamageText text = Instantiate(DataContainer.GetDamageText(DamageType.Default), transform.position, Quaternion.identity);
        text.SetDamageText(damage, _hitTextPoint);
    }
    /// <summary>
    /// ������ UI ����
    /// </summary>
    private void CreateDamageText(int damage, bool isCritical)
    {
        if (gameObject.tag == Tag.Player)
            return;

        DamageText text = Instantiate(DataContainer.GetDamageText(DamageType.Default), transform.position, Quaternion.identity);
        text.SetDamageText(damage, _hitTextPoint, DamageType.Default, isCritical);
    }
    /// <summary>
    /// ������ UI ����
    /// </summary>
    private void CreateDamageText(int damage, DamageType type)
    {
        if (gameObject.tag == Tag.Player)
            return;

        DamageText text = Instantiate(DataContainer.GetDamageText(DamageType.Default), transform.position, Quaternion.identity);
        text.SetDamageText(damage, _hitTextPoint, type, false);
    }
    /// <summary>
    /// ������ UI ����
    /// </summary>
    private void CreateDamageText(int damage, DamageType type, bool isCritical)
    {
        if (gameObject.tag == Tag.Player)
            return;

        DamageText text = Instantiate(DataContainer.GetDamageText(DamageType.Default), transform.position, Quaternion.identity);
        text.SetDamageText(damage, _hitTextPoint, type, isCritical);
    }
    #endregion
    #region ȿ�� ���
    /// <summary>
    /// ���� ȿ�� ���
    /// </summary>
    /// <param name="hitAdditional"></param>
    public void AddHitAdditionalList(HitAdditional hitAdditional)
    {
        // �ߺ�üũ
        int index = _hitAdditionalList.FindIndex(origin => origin.Origin.Equals(hitAdditional.Origin));
        // �ߺ��� ��Ͼ���
        if (index != -1)
            return;
        _hitAdditionalList.Add(hitAdditional);
    }
    /// <summary>
    /// ���� ȿ�� ����
    /// </summary>
    /// <param name="hitAdditional"></param>
    public void RemoveHitAdditionalList(HitAdditional hitAdditional)
    {
        // �ߺ�üũ
        int index = _hitAdditionalList.FindIndex(origin => origin.Origin.Equals(hitAdditional.Origin));
        // �ߺ� ���� �� ���� ����
        if (index == -1)
            return;
        _hitAdditionalList.Remove(hitAdditional);
    }
    #endregion
    #region ����� �߰�/����
    /// <summary>
    /// ����� �߰�
    /// </summary>
    /// <param name="debuff"></param>
    private void AddDebuff(HitAdditional debuff)
    {
        int index = _debuffList.FindIndex(origin => origin.Origin.Equals(debuff.Origin));
        if (index >= _debuffList.Count)
            return;

        HitAdditional cloneDebuff = Instantiate(debuff);
        // ����� �ߺ� ��
        if (index != -1)
        {
            // ���� ����� ����
            _debuffList[index].Exit();
            Destroy(_debuffList[index]);
            _debuffList.RemoveAt(index);
        }
        // ����� �߰� �� �ߵ�
        _debuffList.Add(cloneDebuff);
        cloneDebuff.Origin = debuff.Origin;
        cloneDebuff.Battle = this;
        cloneDebuff.transform = transform;
        cloneDebuff.Enter(); // ����� �ߵ�
    }
    /// <summary>
    /// ����� ����
    /// </summary>
    /// <param name="debuff"></param>
    private void RemoveDebuff(HitAdditional debuff)
    {
        debuff.Exit();
        _debuffList.Remove(debuff);
    }
    #endregion
    /// <summary>
    /// ����� ���� ȣ��
    /// </summary>
    public void EndDebuff(HitAdditional debuff)
    {
        RemoveDebuff(debuff);
    }
    #region �ݹ�
    public void Enter()
    {
        foreach (HitAdditional debuff in _debuffList) 
        {
            debuff.Enter();
        }
    }

    public void Exit()
    {
        foreach (HitAdditional debuff in _debuffList)
        {
            debuff.Exit();
        }
    }

    public void Update()
    {
        foreach (HitAdditional debuff in _debuffList)
        {
            debuff.Update();
        }
    }

    public void FixedUpdate()
    {
        foreach (HitAdditional debuff in _debuffList)
        {
            debuff.FixedUpdate();
        }
    }

    public void Trigger()
    {
        foreach (HitAdditional debuff in _debuffList)
        {
            debuff.Trigger();
        }
    }

    public void TriggerFirst()
    {
        foreach (HitAdditional debuff in _debuffList)
        {
            debuff.TriggerFirst();
        }
    }
    #endregion
}
