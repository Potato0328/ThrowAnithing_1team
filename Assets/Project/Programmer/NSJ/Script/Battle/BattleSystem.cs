using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleSystem : MonoBehaviour, IBattle
{
    public IHit Hit { get; set; }
    public IDebuff Debuff { get; set; }

    [SerializeField] private Transform _hitTextPoint;
    public List<HitAdditional> HitAdditionalList;
    public List<HitAdditional> DebuffList;
    public event UnityAction<int, bool> OnTargetAttackEvent;
    public event UnityAction<int, bool> OnTakeDamageEvent;
    public event UnityAction OnDieEvent;


    [HideInInspector]public bool IsDie;
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

    private void OnDisable()
    {
        ClearDebuff();
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
        battle.TakeDebuff(HitAdditionalList);
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
    public int TargetAttack<T>(T target, int damage, bool isStun) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamage(damage, isStun); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// ����� ���ִ� ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public int TargetAttack<T>(T target, int damage, bool isStun, bool isCritical) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamage(damage, isStun, isCritical); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// ����� ���ִ� ���� (Ÿ��)
    /// </summary>
    public int TargetAttack<T>(T target, int damage, bool isStun , DamageType type) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamage(damage, isStun, type); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// ����� ���ִ� ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public int TargetAttack<T>(T target, int damage, bool isStun, DamageType type, bool isCritical) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamage(damage, isStun, type, isCritical); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// ���� ��� ����� �ָ鼭 ����
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamageWithDebuff(damage, isStun, HitAdditionalList); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// ���� ��� ����� �ָ鼭 ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage =  battle.TakeDamageWithDebuff(damage, isStun, HitAdditionalList, isCritical); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// ���� ��� ����� �ָ鼭 ���� (Ÿ��)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, DamageType type) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamageWithDebuff(damage, isStun, HitAdditionalList, type); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// ���� ��� ����� �ָ鼭 ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, DamageType type, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamageWithDebuff(damage, isStun, HitAdditionalList, type, isCritical);

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ������� �ָ鼭 ����
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, HitAdditional debuff) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamageWithDebuff(damage, isStun, debuff);

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;; // ��븦 ����
    }
    /// <summary>
    /// Ư�� ������� �ָ鼭 ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, HitAdditional debuff, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamageWithDebuff(damage, isStun, debuff, isCritical);

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ������� �ָ鼭 ���� (Ÿ��)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, HitAdditional debuff, DamageType type) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamageWithDebuff(damage, isStun, debuff, type); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ������� �ָ鼭 ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, HitAdditional debuff, DamageType type, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamageWithDebuff(damage, isStun, debuff, type, isCritical); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ��������� �ָ鼭 ���� ����
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, List<HitAdditional> debuffs) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamageWithDebuff(damage, isStun, debuffs); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ��������� �ָ鼭 ���� ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, List<HitAdditional> debuffs, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage =battle.TakeDamageWithDebuff(damage, isStun, debuffs, isCritical); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ��������� �ָ鼭 ���� ���� (Ÿ��)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, List<HitAdditional> debuffs, DamageType type) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage =battle.TakeDamageWithDebuff(damage, isStun, debuffs, type); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ��������� �ָ鼭 ���� ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isStun, List<HitAdditional> debuffs, DamageType type, bool isCritical) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage= battle.TakeDamageWithDebuff(damage, isStun, debuffs, type, isCritical); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
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
    public int TakeDamage(int damage, bool isStun)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage);

        OnTakeDamageEvent?.Invoke(damage, false);
        return hitDamage;
    }
    /// <summary>
    /// ����� ���ִ� ���� �±� (Ÿ��, ġ��Ÿ üũ)
    /// </summary>
    public int TakeDamage(int damage, bool isStun, bool isCritical)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, isCritical);

        OnTakeDamageEvent?.Invoke(damage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// ����� ���ִ� ���� �±� (Ÿ��)
    /// </summary>
    public int TakeDamage(int damage, bool isStun, DamageType type)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage,type);

        OnTakeDamageEvent?.Invoke(damage, false);
        return hitDamage;
    }
    /// <summary>
    /// ����� ���ִ� ���� �±� (Ÿ��, ġ��Ÿ üũ)
    /// </summary>
    public int TakeDamage(int damage, bool isStun, DamageType type, bool isCritical)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, type, isCritical);

        OnTakeDamageEvent?.Invoke(damage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// ���ݹ����鼭 ����� ���� �ޱ�
    /// </summary>
    public int TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> debuffs)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage);
        // ����� �߰�
        foreach (HitAdditional hitAdditional in debuffs) 
        {
            AddDebuff(hitAdditional, hitDamage, false);
        }
        OnTakeDamageEvent?.Invoke(damage, false);
        return hitDamage;
    }
    /// <summary>
    /// ���ݹ����鼭 ����� ���� �ޱ� (Ÿ��)
    /// </summary>
    public int TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> debuffs, bool isCritical)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, isCritical);
        // ����� �߰�
        foreach (HitAdditional hitAdditional in debuffs)
        {
            AddDebuff(hitAdditional, hitDamage, isCritical);
        }
        OnTakeDamageEvent?.Invoke(damage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// ���ݹ����鼭 ����� ���� �ޱ� (Ÿ��)
    /// </summary>
    public int TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> debuffs, DamageType type)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, type);
        // ����� �߰�
        foreach (HitAdditional hitAdditional in debuffs)
        {
            AddDebuff(hitAdditional, hitDamage, false);
        }
        OnTakeDamageEvent?.Invoke(damage, false);
        return hitDamage;
    }
    /// <summary>
    /// ���ݹ����鼭 ����� ���� �ޱ� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public int TakeDamageWithDebuff(int damage, bool isStun, List<HitAdditional> debuffs, DamageType type, bool isCritical)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, type, isCritical);
        // ����� �߰�
        foreach (HitAdditional hitAdditional in debuffs)
        {
            AddDebuff(hitAdditional, hitDamage, isCritical);
        }
        OnTakeDamageEvent?.Invoke(damage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ������� �ޱ�
    /// </summary>
    public int TakeDamageWithDebuff(int damage, bool isStun, HitAdditional debuff)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage);

        AddDebuff(debuff, hitDamage, false);
        OnTakeDamageEvent?.Invoke(damage, false);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ������� �ޱ� (ġ��Ÿ)
    /// </summary>
    public int TakeDamageWithDebuff(int damage, bool isStun, HitAdditional debuff, bool isCritical)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, isCritical);

        AddDebuff(debuff, hitDamage, isCritical);
        OnTakeDamageEvent?.Invoke(damage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ������� �ޱ� (Ÿ��)
    /// </summary>
    public int TakeDamageWithDebuff(int damage, bool isStun, HitAdditional debuff, DamageType type)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage,type);

        AddDebuff(debuff, hitDamage, false);
        OnTakeDamageEvent?.Invoke(damage, false);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ������� �ޱ� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public int TakeDamageWithDebuff(int damage, bool isStun, HitAdditional debuff, DamageType type, bool isCritical)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isStun);
        CreateDamageText(hitDamage, type, isCritical);

        AddDebuff(debuff, hitDamage, isCritical);
        OnTakeDamageEvent?.Invoke(damage, isCritical);
        return hitDamage;
    }
    #endregion
    #region ������ UI 
    /// <summary>
    /// ������ UI ����
    /// </summary>
    private void CreateDamageText(int damage)
    {
        DamageText text = Instantiate(DataContainer.GetDamageText(DamageType.Default), transform.position, Quaternion.identity);
        bool isPlayer = gameObject.tag == Tag.Player;
        text.SetDamageText(damage, _hitTextPoint, isPlayer);
    }
    /// <summary>
    /// ������ UI ����
    /// </summary>
    private void CreateDamageText(int damage, bool isCritical)
    {
        DamageText text = Instantiate(DataContainer.GetDamageText(DamageType.Default), transform.position, Quaternion.identity);
        bool isPlayer = gameObject.tag == Tag.Player;
        text.SetDamageText(damage, _hitTextPoint, DamageType.Default, isCritical, isPlayer);
    }
    /// <summary>
    /// ������ UI ����
    /// </summary>
    private void CreateDamageText(int damage, DamageType type)
    {
        DamageText text = Instantiate(DataContainer.GetDamageText(DamageType.Default), transform.position, Quaternion.identity);
        bool isPlayer = gameObject.tag == Tag.Player;
        text.SetDamageText(damage, _hitTextPoint, type, false, isPlayer);
    }
    /// <summary>
    /// ������ UI ����
    /// </summary>
    private void CreateDamageText(int damage, DamageType type, bool isCritical)
    {
        DamageText text = Instantiate(DataContainer.GetDamageText(DamageType.Default), transform.position, Quaternion.identity);
        bool isPlayer = gameObject.tag == Tag.Player;
        text.SetDamageText(damage, _hitTextPoint, type, isCritical, isPlayer);
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
        int index = HitAdditionalList.FindIndex(origin => origin.Origin.Equals(hitAdditional.Origin));
        // �ߺ��� ��Ͼ���
        if (index != -1)
            return;
        HitAdditionalList.Add(hitAdditional);
    }
    /// <summary>
    /// ���� ȿ�� ����
    /// </summary>
    /// <param name="hitAdditional"></param>
    public void RemoveHitAdditionalList(HitAdditional hitAdditional)
    {
        // �ߺ�üũ
        int index = HitAdditionalList.FindIndex(origin => origin.Origin.Equals(hitAdditional.Origin));
        // �ߺ� ���� �� ���� ����
        if (index == -1)
            return;
        HitAdditionalList.Remove(hitAdditional);
    }
    #endregion
    #region ����� �߰�/����
    /// <summary>
    /// ����� �߰�
    /// </summary>
    /// <param name="debuff"></param>
    private void AddDebuff(HitAdditional debuff)
    {
        if (IsDie == true)
            return;

        int index = DebuffList.FindIndex(origin => origin.Origin.Equals(debuff.Origin));
        if (index >= DebuffList.Count)
            return;
        // ����� �ߺ� ��
        if (index != -1)
        {
            // ���� ����� ���ӽð� ����
            DebuffList[index].Init(0, false, DebuffList[index].Duration);
            // ����� �� �ߵ�
            DebuffList[index].Enter();
        }
        else
        {
            HitAdditional cloneDebuff = Instantiate(debuff);
            // ����� �߰� �� �ߵ�
            DebuffList.Add(cloneDebuff);
            cloneDebuff.Origin = debuff.Origin;
            cloneDebuff.Battle = this;
            cloneDebuff.transform = transform;
            cloneDebuff.Init(0, false, cloneDebuff.Duration);
            cloneDebuff.Enter(); // ����� �ߵ�
        }
    }
    /// <summary>
    /// ����� �߰�
    /// </summary>
    /// <param name="debuff"></param>
    private void AddDebuff(HitAdditional debuff, int damage, bool isCritical)
    {
        if (IsDie == true)
            return;

        int index = DebuffList.FindIndex(origin => origin.Origin.Equals(debuff.Origin));
        if (index >= DebuffList.Count)
            return;
        // ����� �ߺ� ��
        if (index != -1)
        {
            // ���� ����� ���ӽð� ����
            DebuffList[index].Init(damage, isCritical, DebuffList[index].Duration);
            // ����� �� �ߵ�
            DebuffList[index].Enter();
        }
        else
        {
            HitAdditional cloneDebuff = Instantiate(debuff);
            // ����� �߰� �� �ߵ�
            DebuffList.Add(cloneDebuff);
            cloneDebuff.Origin = debuff.Origin;
            cloneDebuff.Battle = this;
            cloneDebuff.transform = transform;
            cloneDebuff.Init(damage, isCritical, cloneDebuff.Duration);
            cloneDebuff.Enter(); // ����� �ߵ�
        }
    }
    /// <summary>
    /// ����� ����
    /// </summary>
    /// <param name="debuff"></param>
    private void RemoveDebuff(HitAdditional debuff)
    {
        debuff.Exit();
        DebuffList.Remove(debuff);
        Destroy(debuff);
    }
    /// <summary>
    /// ����� ��� ����(����)
    /// </summary>
    private void ClearDebuff()
    {
        foreach (HitAdditional debuff in DebuffList)
        {
            debuff.Exit();
            Destroy(debuff);
        }
        DebuffList.Clear();
    }
    #endregion
    #region �ݹ�
    public void Enter()
    {
        foreach (HitAdditional debuff in DebuffList) 
        {
            debuff.Enter();
        }
    }

    public void Exit()
    {
        foreach (HitAdditional debuff in DebuffList)
        {
            debuff.Exit();
        }
    }

    public void Update()
    {
        foreach (HitAdditional debuff in DebuffList)
        {
            debuff.Update();
        }
    }

    public void FixedUpdate()
    {
        foreach (HitAdditional debuff in DebuffList)
        {
            debuff.FixedUpdate();
        }
    }

    public void Trigger()
    {
        foreach (HitAdditional debuff in DebuffList)
        {
            debuff.Trigger();
        }
    }
    #endregion
    /// <summary>
    /// ����� ���� ȣ��
    /// </summary>
    public void EndDebuff(HitAdditional debuff)
    {
        RemoveDebuff(debuff);
    }
    public void Die()
    {
        IsDie = true;
        OnDieEvent?.Invoke();
        ClearDebuff();
    }
}
