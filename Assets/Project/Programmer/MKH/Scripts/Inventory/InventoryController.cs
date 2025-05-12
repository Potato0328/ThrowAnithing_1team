using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MKH
{
    [RequireComponent(typeof(SaveSystem))]
    public class InventoryController : MonoBehaviour
    {
        [Header("패널")]
        [SerializeField] GameObject blueChipPanel;              // 블루칩 패널
        [SerializeField] GameObject inventory;                  // 인벤토리 오브젝트

        [Header("인벤토리, 장비")]
        [SerializeField] InventoryMain mInventory;              // 인벤토리
        [SerializeField] EquipmentInventory mEquipInventory;    // 장비


        [Header("아이템 설명")]
        [SerializeField] TMP_Text ivName;                       // 인벤토리 아이템 이름
        [SerializeField] TMP_Text ivDescription;                // 인벤토리 아이템 설명
        [SerializeField] TMP_Text eqName;                       // 장비 아이템 이름
        [SerializeField] TMP_Text eqDescription;                // 장비 아이템 설명

        [Header("효과음")]
        [SerializeField] public AudioClip ivChoice;             // 장착 효과음
        [SerializeField] public AudioClip ivBreak;              // 분해 효과음
        [SerializeField] public AudioClip emptyClick;           // 빈 공간 효과음
        [SerializeField] public AudioClip clickMove;            // 슬롯 이동 효과음

        [Header("코인 저장")]
        [SerializeField] SaveSystem saveSystem;                 // 분해한 코인 저장 역활

        [Header("이펙트")]
        [SerializeField] GameObject effectUI;                   // 이펙트 
        [SerializeField] GameObject clickEffect;                // 클릭 이펙트
        [SerializeField] GameObject choiceEffect;               // 선택 이펙트
        [SerializeField] GameObject breakEffect;                // 분해 이펙트

        private void Awake()
        {
            mInventory = GetComponent<InventoryMain>();
            mEquipInventory = GetComponent<EquipmentInventory>();

        }

        private void Update()
        {
            if (blueChipPanel.activeSelf || !inventory.activeSelf)
                return;

            if (InputKey.PlayerInput.actions["Choice"].WasPressedThisFrame())
            {
                Choice();
            }
            else if (InputKey.PlayerInput.actions["Break"].WasPressedThisFrame())
            {
                Break();
            }

            Info();
        }

        #region 아이템 버튼 조작
        // 장비 장착, 교체
        public void Choice()
        {
            GameObject obj = EventSystem.current.currentSelectedGameObject;
            // 예외처리
            if (obj == null)
                return;

            InventorySlot slot = obj.GetComponentInParent<InventorySlot>();
            // 예외처리
            if (slot == null || slot.isEquip == true)
                return;

            if (slot.Item != null)
            {
                SoundManager.PlaySFX(ivChoice);
                slot.UseItem();
                GameObject obj1 = ObjectPool.Get(choiceEffect, new Vector3(slot.transform.position.x, slot.transform.position.y, 0), Quaternion.identity, 0.5f);
                obj1.transform.SetParent(effectUI.transform);
                Debug.Log("장비 장착");
            }
            else if (slot.Item == null)
            {
                Debug.Log("장착 할 장비가 없습니다.");
            }
        }

        // 장비 분해
        public void Break()
        {
            GameObject obj = EventSystem.current.currentSelectedGameObject;
            // 예외처리
            if (obj == null)
                return;

            InventorySlot slot = obj.GetComponentInParent<InventorySlot>();
            // 예외처리
            if (slot == null || slot.isEquip == true)
                return;

            if (slot.Item != null)
            {
                // 습득 코인 변수
                int coinsEarned = 0;
                // 등급에 따라 습득 코인 수 변경
                switch (slot.Item.Rate)
                {
                    case RateType.Nomal:
                        coinsEarned = 10; // 일반 등급
                        break;
                    case RateType.Magic:
                        coinsEarned = 50; // 마법 등급
                        break;
                    case RateType.Rare:
                        coinsEarned = 200; // 희귀 등급
                        break;
                    default:
                        coinsEarned = 0;
                        break;
                }
                saveSystem.GetCoin(coinsEarned);

                SoundManager.PlaySFX(ivBreak);
                slot.RemoveItem();
                GameObject obj1 = ObjectPool.Get(breakEffect, new Vector3(slot.transform.position.x, slot.transform.position.y, 0), Quaternion.identity, 1f);
                obj1.transform.SetParent(effectUI.transform);
                Debug.Log($"장비 분해");

            }
            else if (slot.Item == null)
            {
                Debug.Log("분해 할 장비가 없습니다.");
            }
        }
        #endregion

        #region 아이템 정보
        private void Info()
        {
            GameObject obj = EventSystem.current.currentSelectedGameObject;
            if (obj == null)
            {
                if (inventory.activeSelf && InputKey.PlayerInput.actions["LeftClick"].WasPressedThisFrame())
                {
                    SoundManager.PlaySFX(emptyClick);
                    Vector2 pos = Input.mousePosition;
                    GameObject obj1 = ObjectPool.Get(clickEffect, pos, Quaternion.identity, 1f);
                    obj1.transform.SetParent(effectUI.transform);
                }
                return;
            }

            if (InputKey.PlayerInput.actions["UIMove"].WasPressedThisFrame())
            {
                SoundManager.PlaySFX(clickMove);
            }

            InventorySlot slot = obj.GetComponentInParent<InventorySlot>();
            if (slot == null)
            {
                return;
            }

            if (slot.isEquip) // 장비 슬롯
            {
                // 장비 슬롯에 아이템이 있는 상태
                if (slot.Item != null)
                {
                    eqName.text = slot.Item.Name;
                    eqDescription.text = slot.Item.Description;
                    ivName.text = "-";
                    ivDescription.text = "";
                }
                // 장비 슬롯에 아이템이 없는 상태
                else if (slot.Item == null)
                {
                    eqName.text = "-";
                    eqDescription.text = "";
                    ivName.text = "-";
                    ivDescription.text = "";
                }
            }
            else if (!slot.isEquip) //인벤토리 슬롯
            {
                if (slot.Item != null)
                {
                    ivName.text = slot.Item.Name;
                    ivDescription.text = slot.Item.Description;

                    // 아이템 타입 별 인벤토리와 장비 비교
                    int index = (int)slot.Item.Type;
                    if (index >= 0 && index < mEquipInventory.mSlots.Count)
                    {
                        Item equipItem = mEquipInventory.mSlots[index].Item;
                        if (equipItem != null)
                        {
                            eqName.text = equipItem.Name;
                            eqDescription.text = equipItem.Description;
                        }
                        else
                        {
                            eqName.text = "-";
                            eqDescription.text = "-";
                        }
                    }
                }
                // 인벤토리 슬롯에 아이템이 없는 상태
                else if (slot.Item == null)
                {
                    ivName.text = "-";
                    ivDescription.text = "";
                    eqName.text = "-";
                    eqDescription.text = "";
                }
            }
        }
        #endregion

        // 아이템 모두 삭제 (인게임 끝날 시 초기화용)
        public void ItemReset()
        {
            foreach (var equipSlot in mEquipInventory.mSlots)
            {
                if (equipSlot.Item != null)
                {
                    equipSlot.ClearSlot();
                }
            }

            foreach (var invenSlot in mInventory.mSlots)
            {
                if (invenSlot.Item != null)
                {
                    invenSlot.ClearSlot();
                }
            }
            mEquipInventory.CalculateEffect();

            Debug.Log("아이템 초기화");
        }
    }
}
