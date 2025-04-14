using System;
using UnityEngine;

namespace MKH
{
    public enum ItemType { Helmet, Shirts, Glasses, Gloves, Pants, Earring, Ring, Shoes, Necklace }

    public enum RateType { Nomal, Magic, Rare }

    [Serializable]
    public struct EquipmentEffect
    {
        [Header("공격력"), SerializeField] private float mDamage;
        [Header("방어력"), SerializeField] private float mDefense;
        [Header("체력"), SerializeField] private float mHP;
        [Header("치명타 확률"), SerializeField] private float mCritical;
        [Header("공격 속도"), SerializeField] private float mAttackSpeed;
        [Header("스테미나"), SerializeField] private float mStemina;
        [Header("장비획득률"), SerializeField] private float mEquipRate;
        [Header("이동 속도"), SerializeField] private float mSpeed;
        [Header("마나"), SerializeField] private float mMana;

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

    public abstract class Item : ScriptableObject
    {
        [Header("이름"), SerializeField] private string mName;
        [Header("설명"), Multiline, SerializeField] private string mDescription;
        [Header("아이템 타입"), SerializeField] private ItemType mItemType;
        [Header("아이템 등급"), SerializeField] private RateType mRateType;
        [Header("이미지"), SerializeField] private Sprite mItemImage;
        [Header("아이템의 프리팹"), SerializeField] private GameObject mItemPrefab;
        [Space(50)]
        [Header("장비 아이템 효과"), SerializeField] public EquipmentEffect mEffect;

        public string Name { get => mName; set => mName = value; }
        public string Description { get => mDescription; set => mDescription = value; }
        public ItemType Type { get => mItemType; set => mItemType = value; }
        public RateType Rate { get => mRateType; set => mRateType = value; }
        public Sprite Image { get => mItemImage; set => mItemImage = value; }
        public GameObject Prefab { get => mItemPrefab; set => mItemPrefab = value; }
        public EquipmentEffect Effect { get => mEffect; set => mEffect = value; }

        public virtual Item Create()
        {
            return Instantiate(this);
        }
    }
}
