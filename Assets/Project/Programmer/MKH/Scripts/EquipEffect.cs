using UnityEngine;

[System.Serializable]
public struct EquipmentEffect
{
    [Header("공격력")] public float mDamage;
    [Header("방어력")] public float mDefense;
    [Header("체력")] public float mHP;
    [Header("치명타 확률")] public float mCritical;
    [Header("공격 속도")] public float mAttackSpeed;
    [Header("스테미나")] public float mStemina;
    [Header("장비획득률")] public float mEquipRate;
    [Header("이동 속도")] public float mSpeed;
    [Header("마나")] public float mMana;

    public float Damage { get => mDamage; set => mDamage = value; }
    public float Defense { get => mDefense; set => mDefense = value; }
    public float HP { get => mHP; set => mHP = value; }
    public float Critical { get => mCritical; set => mCritical = value; }
    public float AttackSpeed { get => mAttackSpeed; set => mAttackSpeed = value; }
    public float Stemina { get => mStemina; set => mStemina = value; }
    public float EquipRate { get => mEquipRate; set => mEquipRate = value; }
    public float Speed { get => mSpeed; set => mSpeed = value; }
    public float Mana { get => mMana; set => mMana = value; }

    public static EquipmentEffect operator +(EquipmentEffect param1, EquipmentEffect param2)
    {
        EquipmentEffect effect = new EquipmentEffect();

        effect.mDamage = param1.mDamage + param2.mDamage;
        effect.mDefense = param1.mDefense + param2.mDefense;
        effect.mHP = param1.mHP + param2.mHP;
        effect.mCritical = param1.mCritical + param2.mCritical;
        effect.mAttackSpeed = param1.mAttackSpeed + param2.mAttackSpeed;
        effect.mStemina = param1.mStemina + param2.mStemina;
        effect.mEquipRate = param1.mEquipRate + param2.mEquipRate;
        effect.mSpeed = param1.mSpeed + param2.mSpeed;
        effect.mMana = param1.mMana + param2.mMana;

        return effect;
    }
}

