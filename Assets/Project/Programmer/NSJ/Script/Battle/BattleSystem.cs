using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CrowdControlType { None, Stiff, Stun, Size}
public class BattleSystem : MonoBehaviour, IBattle
{
    public IHit Hit { get; set; }
    public IDebuff Debuff { get; set; }

    public Transform HitPoint;
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

        if (HitPoint == null)
        {
            HitPoint = new GameObject("HitTextPoint").transform;
            HitPoint.SetParent(transform, true);
            HitPoint.localPosition = new Vector3(0, 1f, 0);
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
    public int TargetAttack<T>(T target, int damage, bool isIgnoreDef) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamage(damage, isIgnoreDef); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// ����� ���ִ� ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public int TargetAttack<T>(T target, int damage,  bool isCritical, bool isIgnoreDef) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamage(damage, isCritical, isIgnoreDef); // ��븦 ����
        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// ����� ���ִ� ���� (Ÿ��)
    /// </summary>
    public int TargetAttack<T>(T target, int damage, CrowdControlType type, bool isIgnoreDef) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamage(damage, type, isIgnoreDef); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// ����� ���ִ� ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public int TargetAttack<T>(T target, int damage, CrowdControlType type, bool isCritical, bool isIgnoreDef) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamage(damage, type, isCritical, isIgnoreDef); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// ���� ��� ����� �ָ鼭 ����
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, bool isIgnoreDef) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamageWithDebuff(damage, CrowdControlType.None, HitAdditionalList, isIgnoreDef); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// ���� ��� ����� �ָ鼭 ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage,  bool isCritical, bool isIgnoreDef) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage =  battle.TakeDamageWithDebuff(damage, HitAdditionalList, isCritical, isIgnoreDef); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// ���� ��� ����� �ָ鼭 ���� (Ÿ��)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, CrowdControlType type, bool isIgnoreDef) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamageWithDebuff(damage, type, HitAdditionalList, isIgnoreDef); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// ���� ��� ����� �ָ鼭 ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, CrowdControlType type,bool isCritical, bool isIgnoreDef) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamageWithDebuff(damage, type, HitAdditionalList, isCritical, isIgnoreDef);

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ������� �ָ鼭 ����
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, HitAdditional debuff, bool isIgnoreDef) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamageWithDebuff(damage, debuff, isIgnoreDef);

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;; // ��븦 ����
    }
    /// <summary>
    /// Ư�� ������� �ָ鼭 ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, HitAdditional debuff, bool isCritical, bool isIgnoreDef) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamageWithDebuff(damage, debuff, isCritical, isIgnoreDef);

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ������� �ָ鼭 ���� (Ÿ��)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, CrowdControlType type, HitAdditional debuff, bool isIgnoreDef) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamageWithDebuff(damage, type, debuff, isIgnoreDef); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ������� �ָ鼭 ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, CrowdControlType type, HitAdditional debuff, bool isCritical, bool isIgnoreDef) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamageWithDebuff(damage, type, debuff, isCritical, isIgnoreDef); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ��������� �ָ鼭 ���� ����
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, List<HitAdditional> debuffs, bool isIgnoreDef) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage = battle.TakeDamageWithDebuff(damage, debuffs, isIgnoreDef); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ��������� �ָ鼭 ���� ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, List<HitAdditional> debuffs, bool isCritical, bool isIgnoreDef) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage =battle.TakeDamageWithDebuff(damage, debuffs, isCritical, isIgnoreDef); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ��������� �ָ鼭 ���� ���� (Ÿ��)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, CrowdControlType type, List<HitAdditional> debuffs, bool isIgnoreDef) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage =battle.TakeDamageWithDebuff(damage, type, debuffs, isIgnoreDef); // ��븦 ����

        OnTargetAttackEvent?.Invoke(hitDamage, false);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ��������� �ָ鼭 ���� ���� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public int TargetAttackWithDebuff<T>(T target, int damage, CrowdControlType type, List<HitAdditional> debuffs,  bool isCritical, bool isIgnoreDef) where T : Component
    {
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        int hitDamage= battle.TakeDamageWithDebuff(damage, type, debuffs, isCritical, isIgnoreDef); // ��븦 ����

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
    public int TakeDamage(int damage, bool isIgnoreDef)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isIgnoreDef, CrowdControlType.None);
        CreateDamageText(hitDamage);

        OnTakeDamageEvent?.Invoke(damage, false);
        return hitDamage;
    }
    /// <summary>
    /// ����� ���ִ� ���� �±� (Ÿ��, ġ��Ÿ üũ)
    /// </summary>
    public int TakeDamage(int damage, bool isCritical, bool isIgnoreDef)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isIgnoreDef, CrowdControlType.None);
        CreateDamageText(hitDamage, isCritical);

        OnTakeDamageEvent?.Invoke(damage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// ����� ���ִ� ���� �±� (Ÿ��)
    /// </summary>
    public int TakeDamage(int damage, CrowdControlType type, bool isIgnoreDef)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isIgnoreDef, type);
        CreateDamageText(hitDamage,type);

        OnTakeDamageEvent?.Invoke(damage, false);
        return hitDamage;
    }
    /// <summary>
    /// ����� ���ִ� ���� �±� (Ÿ��, ġ��Ÿ üũ)
    /// </summary>
    public int TakeDamage(int damage, CrowdControlType type, bool isCritical, bool isIgnoreDef)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isIgnoreDef, type);
        CreateDamageText(hitDamage, type, isCritical);

        OnTakeDamageEvent?.Invoke(damage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// ���ݹ����鼭 ����� ���� �ޱ�
    /// </summary>
    public int TakeDamageWithDebuff(int damage, List<HitAdditional> debuffs, bool isIgnoreDef)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isIgnoreDef, CrowdControlType.None);
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
    public int TakeDamageWithDebuff(int damage, List<HitAdditional> debuffs, bool isCritical, bool isIgnoreDef)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isIgnoreDef,CrowdControlType.None);
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
    public int TakeDamageWithDebuff(int damage, CrowdControlType type, List<HitAdditional> debuffs,  bool isIgnoreDef)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isIgnoreDef, type);
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
    public int TakeDamageWithDebuff(int damage,  CrowdControlType type,List<HitAdditional> debuffs, bool isCritical, bool isIgnoreDef)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isIgnoreDef, type);
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
    public int TakeDamageWithDebuff(int damage,  HitAdditional debuff, bool isIgnoreDef)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isIgnoreDef, CrowdControlType.None);
        CreateDamageText(hitDamage);

        AddDebuff(debuff, hitDamage, false);
        OnTakeDamageEvent?.Invoke(damage, false);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ������� �ޱ� (ġ��Ÿ)
    /// </summary>
    public int TakeDamageWithDebuff(int damage,  HitAdditional debuff, bool isCritical, bool isIgnoreDef)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isIgnoreDef, CrowdControlType.None);
        CreateDamageText(hitDamage, isCritical);

        AddDebuff(debuff, hitDamage, isCritical);
        OnTakeDamageEvent?.Invoke(damage, isCritical);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ������� �ޱ� (Ÿ��)
    /// </summary>
    public int TakeDamageWithDebuff(int damage, CrowdControlType type, HitAdditional debuff, bool isIgnoreDef)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isIgnoreDef, type);
        CreateDamageText(hitDamage,type);

        AddDebuff(debuff, hitDamage, false);
        OnTakeDamageEvent?.Invoke(damage, false);
        return hitDamage;
    }
    /// <summary>
    /// Ư�� ������� �ޱ� (Ÿ��, ġ��Ÿ)
    /// </summary>
    public int TakeDamageWithDebuff(int damage, CrowdControlType type, HitAdditional debuff,  bool isCritical, bool isIgnoreDef)
    {
        // ������ �ֱ�
        int hitDamage = Hit.TakeDamage(damage, isIgnoreDef, type);
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
        DamageText text = Instantiate(DataContainer.GetDamageText(CrowdControlType.None), transform.position, Quaternion.identity);
        bool isPlayer = gameObject.tag == Tag.Player;
        text.SetDamageText(damage, HitPoint, isPlayer);
    }
    /// <summary>
    /// ������ UI ����
    /// </summary>
    private void CreateDamageText(int damage, bool isCritical)
    {
        DamageText text = Instantiate(DataContainer.GetDamageText(CrowdControlType.None), transform.position, Quaternion.identity);
        bool isPlayer = gameObject.tag == Tag.Player;
        text.SetDamageText(damage, HitPoint, CrowdControlType.None, isCritical, isPlayer);
    }
    /// <summary>
    /// ������ UI ����
    /// </summary>
    private void CreateDamageText(int damage, CrowdControlType type)
    {
        DamageText text = Instantiate(DataContainer.GetDamageText(CrowdControlType.None), transform.position, Quaternion.identity);
        bool isPlayer = gameObject.tag == Tag.Player;
        text.SetDamageText(damage, HitPoint, type, false, isPlayer);
    }
    /// <summary>
    /// ������ UI ����
    /// </summary>
    private void CreateDamageText(int damage, CrowdControlType type, bool isCritical)
    {
        DamageText text = Instantiate(DataContainer.GetDamageText(CrowdControlType.None), transform.position, Quaternion.identity);
        bool isPlayer = gameObject.tag == Tag.Player;
        text.SetDamageText(damage, HitPoint, type, isCritical, isPlayer);
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
