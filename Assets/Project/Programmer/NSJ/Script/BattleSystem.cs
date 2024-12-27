using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BattleSystem : MonoBehaviour, IBattle
{
    public IHit Hitable { get; set; }
    public IDebuff Debuffable { get; set; }

    [SerializeField] private List<HitAdditional> _hitAdditionalList;
    [SerializeField] private List<HitAdditional> _debuffList;

    private void Awake()
    {
        Hitable = GetComponent<IHit>();
        Debuffable = GetComponent<IDebuff>();
    }
    public void TakeAttack<T>(T target, int damage, bool isStun) where T : Component
    {
        // ��Ʋ �ý����� ��Ʋ �ý��� ���� ��� 
        // �÷��̾� <-> ��Ʋ�ý��� <-> ��Ʋ�ý��� <->����
        IBattle battle = target.gameObject.GetComponent<IBattle>(); // ��� ��Ʋ�ý��� ����
        battle.TakeAttack(damage, isStun, _hitAdditionalList); // ��� ����
    }

    public void TakeAttack(int damage, bool isStun, List<HitAdditional> hitAdditionals)
    {
        // ������ �ֱ�
        Hitable.TakeDamage(damage, isStun);
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
        int index = _debuffList.FindIndex(origin => origin.Origin.Equals(hitAdditional.Origin));
        // �ߺ��� ��Ͼ���
        if (index != -1)
            return;
        _hitAdditionalList.Add(hitAdditional);
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
        cloneDebuff.Target = gameObject;
        cloneDebuff.Battle = this;
        cloneDebuff.Enter();
    }
    /// <summary>
    /// ����� ����
    /// </summary>
    /// <param name="debuff"></param>
    private void RemoveDebuff(HitAdditional debuff)
    {
        _debuffList.Remove(debuff);
    }


}
