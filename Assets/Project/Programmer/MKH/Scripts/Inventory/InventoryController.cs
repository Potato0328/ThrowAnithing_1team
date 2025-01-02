using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MKH
{
    public class InventoryController : MonoBehaviour
    {
        [Header("인벤토리 및 장비")]
        [SerializeField] GameObject inventory;                  // 인벤토리
        [SerializeField] GameObject mInventorySlotsParent;      // 인벤토리 슬롯 모음집
        [SerializeField] InventorySlot[] ivSlots;               // 인벤토리 슬롯들
        [SerializeField] GameObject mEquipmentSlotsParent;      // 장비 슬롯 모음집
        [SerializeField] InventorySlot[] eqSlots;               // 장비 슬롯들
        [SerializeField] BlueChipPanel blueChipPanel;

        [Header("슬롯 버튼")]
        [SerializeField] GameObject[] buttons;                  // 슬롯 버튼
        int selectedButtonsIndex = 9;                           // 슬롯 시작 위치
        int buttonCount;                                        // 슬롯 개수
        private bool axisInUse;                                 // 키 연속 조작 방지

        [Header("슬롯 색")]
        [SerializeField] Color HighlightedColor;                // 선택 슬롯 색
        [SerializeField] Color color;                           // 미선택 슬롯 색

        [Header("아이템 설명")]
        [SerializeField] TMP_Text ivName;                       // 인벤토리 아이템 이름
        [SerializeField] TMP_Text ivDescription;                // 인벤토리 아이템 설명
        [SerializeField] TMP_Text eqName;                       // 장비 아이템 이름
        [SerializeField] TMP_Text eqDescription;                // 장비 아이템 설명

        private void Awake()
        {
            // 인벤토리, 장비 슬롯들 불러오기
            ivSlots = mInventorySlotsParent.GetComponentsInChildren<InventorySlot>();
            eqSlots = mEquipmentSlotsParent.GetComponentsInChildren<InventorySlot>();
        }

        private void Start()
        {
            buttonCount = buttons.Length;
        }

        private void Update()
        {
            if (inventory.activeSelf == false)
                return;

            ButtonsControl();               // 키 조작
            Use(selectedButtonsIndex);      // 키 버튼 조작
            Info();                         // 아이템 정보
        }

        #region 키 조작
        private void ButtonsControl()
        {
            float x = InputKey.GetAxisRaw("Horizontal");       // 좌 우 조작
            float y = InputKey.GetAxisRaw("Vertical");         // 상 하 조작

            // 인벤토리만 켜져있을 때
            if (inventory.activeSelf == true && blueChipPanel == false)
            {
                // 왼쪽
                if (x == -1)
                {
                    if (selectedButtonsIndex > 0 && axisInUse == false)
                    {
                        axisInUse = true;
                        selectedButtonsIndex -= 1;
                    }
                }
                // 오른쪽
                else if (x == 1)
                {
                    if (selectedButtonsIndex < buttons.Length - 1 && axisInUse == false)
                    {
                        axisInUse = true;
                        selectedButtonsIndex += 1;
                    }
                }
                // 위
                else if (y == 1)
                {
                    if (selectedButtonsIndex > 2 && axisInUse == false)
                    {
                        axisInUse = true;
                        selectedButtonsIndex -= 3;
                    }
                }
                // 아래
                else if (y == -1)
                {
                    if (selectedButtonsIndex < buttons.Length - 3 && axisInUse == false)
                    {
                        axisInUse = true;
                        selectedButtonsIndex += 3;
                    }
                }
                // 키 멈춤 방지
                else
                {
                    axisInUse = false;
                }


                // 선택 슬롯 색 입히기
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (i == selectedButtonsIndex)
                    {
                        buttons[i].GetComponent<Image>().color = HighlightedColor;
                    }
                    else
                    {
                        buttons[i].GetComponent<Image>().color = color;
                    }
                }
            }
        }
        #endregion

        #region 아이템 버튼 조작
        private void Use(int index)
        {
            // 인벤토리만 켜져있을 때
            if (inventory.activeSelf == true && blueChipPanel == false)
            {
                // 아이템 장착
                if (InputKey.GetButtonDown("InventoryEquip"))
                {
                    // 인벤토리
                    if (index >= 9)
                    {
                        if (ivSlots[index - 9].Item != null)
                        {
                            ivSlots[index - 9].UseItem();
                            Debug.Log($"인벤토리 {index - 9}번 장비 장착");
                        }
                        else if (ivSlots[index - 9].Item == null)
                        {
                            Debug.Log("장착 할 장비가 없습니다.");
                            return;
                        }
                    }
                }

                // 아이템 분해
                if (InputKey.GetButtonDown("Decomposition"))
                {
                    // 인벤토리
                    if (index >= 9)
                    {
                        if (ivSlots[index - 9].Item != null)
                        {
                            ivSlots[index - 9].ClearSlot();
                            Debug.Log($"인벤토리 {index - 9}번 장비 분해");
                        }
                        else if (ivSlots[index - 9].Item == null)
                        {
                            Debug.Log("분해 할 장비가 없습니다.");
                            return;
                        }
                    }
                }
            }
        }
        #endregion

        #region 아이템 정보
        private void Info()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == selectedButtonsIndex)
                {
                    // 장비창 설명
                    if (i < 9)
                    {
                        // 장비 슬롯에 아이템이 있는 상태
                        if (eqSlots[i].Item != null)
                        {
                            eqName.text = eqSlots[i].Item.Name;
                            eqDescription.text = eqSlots[i].Item.Description;
                            ivName.text = "-";
                            ivDescription.text = "";
                        }
                        // 장비 슬롯에 아이템이 없는 상태
                        else if (eqSlots[i].Item == null)
                        {
                            eqName.text = "-";
                            eqDescription.text = "";
                            ivName.text = "-";
                            ivDescription.text = "";
                        }
                    }
                    // 인벤토리 설명
                    else if (i >= 9)
                    {
                        // 인벤토리 슬롯에 아이템 있는 상태
                        if (ivSlots[i - 9].Item != null)
                        {
                            ivName.text = ivSlots[i - 9].Item.Name;
                            ivDescription.text = ivSlots[i - 9].Item.Description;

                            // 아이템 타입 별 인벤토리와 장비 비교
                            switch (ivSlots[i - 9].Item.Type)
                            {
                                case ItemType.Helmet:
                                    if (eqSlots[0].Item != null)
                                    {
                                        eqName.text = eqSlots[0].Item.Name;
                                        eqDescription.text = eqSlots[0].Item.Description;
                                    }
                                    else if (eqSlots[0].Item == null)
                                    {
                                        eqName.text = "-";
                                        eqDescription.text = "";
                                    }
                                    break;
                                case ItemType.Shirts:
                                    if (eqSlots[1].Item != null)
                                    {
                                        eqName.text = eqSlots[1].Item.Name;
                                        eqDescription.text = eqSlots[1].Item.Description;
                                    }
                                    else if (eqSlots[1].Item == null)
                                    {
                                        eqName.text = "-";
                                        eqDescription.text = "";
                                    }
                                    break;
                                case ItemType.Glasses:
                                    if (eqSlots[2].Item != null)
                                    {
                                        eqName.text = eqSlots[2].Item.Name;
                                        eqDescription.text = eqSlots[2].Item.Description;
                                    }
                                    else if (eqSlots[2].Item == null)
                                    {
                                        eqName.text = "-";
                                        eqDescription.text = "";
                                    }
                                    break;
                                case ItemType.Gloves:
                                    if (eqSlots[3].Item != null)
                                    {
                                        eqName.text = eqSlots[3].Item.Name;
                                        eqDescription.text = eqSlots[3].Item.Description;
                                    }
                                    else if (eqSlots[3].Item == null)
                                    {
                                        eqName.text = "-";
                                        eqDescription.text = "";
                                    }
                                    break;
                                case ItemType.Pants:
                                    if (eqSlots[4].Item != null)
                                    {
                                        eqName.text = eqSlots[4].Item.Name;
                                        eqDescription.text = eqSlots[4].Item.Description;
                                    }
                                    else if (eqSlots[4].Item == null)
                                    {
                                        eqName.text = "-";
                                        eqDescription.text = "";
                                    }
                                    break;
                                case ItemType.Earring:
                                    if (eqSlots[5].Item != null)
                                    {
                                        eqName.text = eqSlots[5].Item.Name;
                                        eqDescription.text = eqSlots[5].Item.Description;
                                    }
                                    else if (eqSlots[5].Item == null)
                                    {
                                        eqName.text = "-";
                                        eqDescription.text = "";
                                    }
                                    break;
                                case ItemType.Ring:
                                    if (eqSlots[6].Item != null)
                                    {
                                        eqName.text = eqSlots[6].Item.Name;
                                        eqDescription.text = eqSlots[6].Item.Description;
                                    }
                                    else if (eqSlots[6].Item == null)
                                    {
                                        eqName.text = "-";
                                        eqDescription.text = "";
                                    }
                                    break;
                                case ItemType.Shoes:
                                    if (eqSlots[7].Item != null)
                                    {
                                        eqName.text = eqSlots[7].Item.Name;
                                        eqDescription.text = eqSlots[7].Item.Description;
                                    }
                                    else if (eqSlots[7].Item == null)
                                    {
                                        eqName.text = "-";
                                        eqDescription.text = "";
                                    }
                                    break;
                                case ItemType.Necklace:
                                    if (eqSlots[8].Item != null)
                                    {
                                        eqName.text = eqSlots[8].Item.Name;
                                        eqDescription.text = eqSlots[8].Item.Description;
                                    }
                                    else if (eqSlots[8].Item == null)
                                    {
                                        eqName.text = "-";
                                        eqDescription.text = "";
                                    }
                                    break;
                            }
                        }
                        // 인벤토리 슬롯에 아이템이 없는 상태
                        else if (ivSlots[i - 9].Item == null)
                        {
                            ivName.text = "-";
                            ivDescription.text = "";
                            eqName.text = "-";
                            eqDescription.text = "";
                        }
                    }
                }
            }
        }
        #endregion


        // 아이템 모두 삭제 (인게임 끝날 시 초기화용)
        public void ItemReset()
        {
            for (int i = 0; i < buttonCount; i++)
            {
                if (i < 9)
                {
                    if (eqSlots[i].Item != null)
                    {
                        eqSlots[i].RemoveEquipmentSlot();
                    }
                }
                else if (i >= 9)
                {
                    if (ivSlots[i - 9].Item != null)
                    {
                        ivSlots[i - 9].ClearSlot();
                    }
                }
            }
            Debug.Log("아이템 초기화");
        }
    }
}
