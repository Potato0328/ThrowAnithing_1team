using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour, IBattle
{
    public IHit Hit { get; set; }
    public IDebuff Debuff { get; set; }

    [SerializeField] private List<HitAdditional> _hitAdditionalList;
    [SerializeField] private List<HitAdditional> _debuffList;

    private void Awake()
    {
        Hit = GetComponent<IHit>();
        Debuff = GetComponent<IDebuff>();
    }
    public void TargetAttack<T>(T target, int damage, bool isStun) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeAttack(damage, isStun, _hitAdditionalList); // ��븦 ����
    }

    public void TakeAttack(int damage, bool isStun, List<HitAdditional> hitAdditionals)
    {
        // ������ �ֱ�
        Hit.TakeDamage(damage, isStun);
        // ����� �߰�
        foreach (HitAdditional hitAdditional in hitAdditionals) 
        {
            AddDebuff(hitAdditional);
        }
    }
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
    /// <summary>
    /// ����� ���� ȣ��
    /// </summary>
    public void EndDebuff(HitAdditional debuff)
    {
        RemoveDebuff(debuff);
    }

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
