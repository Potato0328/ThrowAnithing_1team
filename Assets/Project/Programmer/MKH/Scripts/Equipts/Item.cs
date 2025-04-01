using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKH
{
    public enum ItemType
    {
        None, Helmet, Shirts, Glasses, Gloves, Pants, Earring, Ring, Shoes, Necklace
    }

    public enum RateType
    {
        Nomal, Magic, Rare
    }

    [CreateAssetMenu(fileName = "Item", menuName = "Add Item/Item")]
    public class Item : ScriptableObject
    {
        #region 사용 X
        //[Header("고유 아이디 (중복X)")]
        //[SerializeField] private int mItmeID;
        //public int ItemID => mItmeID;
        #endregion

        [Header("이름")]
        [SerializeField] private string mName;
        public string Name { get { return mName; } set { mName = value; } }

        [Header("설명")]
        [Multiline]
        [SerializeField] private string mDescription;
        public string Description { get { return mDescription; } set { mDescription = value; } }

        [Header("아이템 타입")]
        [SerializeField] private ItemType mItemType;
        public ItemType Type { get { return mItemType; } }

        [Header("아이템 등급")]
        [SerializeField] private RateType mRateType;
        public RateType Rate { get { return mRateType; } }


        [Header("인벤토리에서 보여지는 이미지")]
        [SerializeField] private Sprite mItemImage;
        public Sprite Image { get { return mItemImage; } }

        [Header("씬에서 오브젝트로 보여질 아이템의 프리팹")]
        [SerializeField] private GameObject mItemPrefab;
        public GameObject Prefab { get { return mItemPrefab; } }

        public virtual Item Create()
        {
            return null;
        }
    }
}
