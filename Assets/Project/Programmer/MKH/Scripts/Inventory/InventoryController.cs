using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MKH
{
    [RequireComponent(typeof(SaveSystem))]
    public class InventoryController : MonoBehaviour
    {
        [Header("�κ��丮 �� ���")]
        [SerializeField] GameObject inventory;                  // �κ��丮
        [SerializeField] GameObject mInventorySlotsParent;      // �κ��丮 ���� ������
        [SerializeField] InventorySlot[] ivSlots;               // �κ��丮 ���Ե�
        [SerializeField] GameObject mEquipmentSlotsParent;      // ��� ���� ������
        [SerializeField] InventorySlot[] eqSlots;               // ��� ���Ե�
        [SerializeField] GameObject blueChipPanel;              // ���Ĩ �г�


        [Header("���� ��ư")]
        [SerializeField] InventorySlot[] slots;                  // ���� ��ư
        int selectedButtonsIndex;                               // ���� ���� ��ġ
        int buttonCount;                                        // ���� ����
        private bool axisInUse;                                 // Ű ���� ���� ����

        [Header("���� ��")]
        [SerializeField] Color HighlightedColor;                // ���� ���� ��
        [SerializeField] Color color;                           // �̼��� ���� ��

        [Header("������ ����")]
        [SerializeField] TMP_Text ivName;                       // �κ��丮 ������ �̸�
        [SerializeField] TMP_Text ivDescription;                // �κ��丮 ������ ����
        [SerializeField] TMP_Text eqName;                       // ��� ������ �̸�
        [SerializeField] TMP_Text eqDescription;                // ��� ������ ����

        InventoryMain mInventory;
        [SerializeField] SaveSystem saveSystem;

        private void Awake()
        {
            // �κ��丮, ��� ���Ե� �ҷ�����
            ivSlots = mInventorySlotsParent.GetComponentsInChildren<InventorySlot>();
            eqSlots = mEquipmentSlotsParent.GetComponentsInChildren<InventorySlot>();
            mInventory = GetComponent<InventoryMain>();
        }

        private void Start()
        {
            buttonCount = slots.Length;
            selectedButtonsIndex = 9;
        }

        private void Update()
        {
            if (!inventory.activeSelf)
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

        #region Ű ����
        public void Move()
        {
            float x = 0;//InputKey.GetAxis(InputKey.Horizontal);       // �� �� ����
            float y = 0;//InputKey.GetAxis(InputKey.Vertical);         // �� �� ����

            // �κ��丮�� �������� ��
            if (inventory.activeSelf && !blueChipPanel.activeSelf)
            {
                // ����
                if (x < 0)
                {
                    if (selectedButtonsIndex > 0 && axisInUse == false)
                    {
                        axisInUse = true;
                        selectedButtonsIndex -= 1;
                    }
                }
                // ������
                else if (x > 0)
                {
                    if (selectedButtonsIndex < slots.Length - 1 && axisInUse == false)
                    {
                        axisInUse = true;
                        selectedButtonsIndex += 1;
                    }
                }
                // ��
                else if (y > 0)
                {
                    if (selectedButtonsIndex > 2 && axisInUse == false)
                    {
                        axisInUse = true;
                        selectedButtonsIndex -= 3;
                    }
                }
                // �Ʒ�
                else if (y < 0)
                {
                    if (selectedButtonsIndex < slots.Length - 3 && axisInUse == false)
                    {
                        axisInUse = true;
                        selectedButtonsIndex += 3;
                    }
                }
                // Ű ���� ����
                else
                {
                    axisInUse = false;
                }
            }


            // ���� ���� �� ������
            //for (int i = 0; i < slots.Length; i++)
            //{
            //    if (i == selectedButtonsIndex)
            //    {
            //        slots[i].GetComponent<Image>().color = HighlightedColor;
            //    }
            //    else
            //    {
            //        slots[i].GetComponent<Image>().color = color;
            //    }
            //}
        }
        public void ChangeSelectButton(InventorySlot slot)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] == slot)
                {
                    selectedButtonsIndex = i;
                    slots[i].SlotButton.Outline.SetActive(true);
                }
                else
                {
                    slots[i].SlotButton.Outline.SetActive(false);
                }
            }
            Info();
        }

        #endregion

        #region ������ ��ư ����
        public void Choice()
        {

            /* // �κ��丮�� �������� ��
             if (inventory.activeSelf && !blueChipPanel.activeSelf)
             {
                 int index = selectedButtonsIndex;
                 // ������ ����
                 // �κ��丮
                 if (index >= 9)
                 {
                     if (ivSlots[index - 9].Item != null)
                     {
                         ivSlots[index - 9].UseItem();
                         mInventory.Sorting();
                         Debug.Log($"�κ��丮 {index - 9}�� ��� ����");
                     }
                     else if (ivSlots[index - 9].Item == null)
                     {
                         Debug.Log("���� �� ��� �����ϴ�.");
                         return;
                     }
                 }
             }*/

            Debug.Log("���̽� Ű �Է�");
            GameObject obj = EventSystem.current.currentSelectedGameObject;
            InventorySlot slot = obj.GetComponentInParent<InventorySlot>();
            if (slot == null)
                return;

            if (slot.Item != null)
            {
                slot.UseItem();
                mInventory.Sorting();
                Debug.Log("��� ����");
            }
            else if (slot.Item == null)
            {
                Debug.Log("���� �� ��� �����ϴ�.");
            }
        }

        public void Break()
        {
            /*if (inventory.activeSelf && !blueChipPanel.activeSelf)
            {
                int index = selectedButtonsIndex;
                // ������ ����
                // �κ��丮
                if (index >= 9)
                {
                    if (ivSlots[index - 9].Item != null)
                    {
                        // ���� ���� ����
                        int coinsEarned = 0;
                        // ��޿� ���� ���� ���� �� ����
                        switch (ivSlots[index - 9].Item.Rate)
                        {
                            case RateType.Nomal:
                                coinsEarned = 10; // �Ϲ� ���
                                break;
                            case RateType.Magic:
                                coinsEarned = 50; // ���� ���
                                break;
                            case RateType.Rare:
                                coinsEarned = 200; // ��� ���
                                break;
                            default:
                                coinsEarned = 0;
                                break;
                        }
                        saveSystem.GetCoin(coinsEarned);

                        ivSlots[index - 9].ClearSlot();
                        mInventory.Sorting();
                        Debug.Log($"�κ��丮 {index - 9}�� ��� ����");
                        Debug.Log(coinsEarned);

                    }
                    else if (ivSlots[index - 9].Item == null)
                    {
                        Debug.Log($"{index - 9}���� �� ��� �����ϴ�.");
                        return;
                    }
                }
            }*/

            GameObject obj = EventSystem.current.currentSelectedGameObject;
            InventorySlot slot = obj.GetComponentInParent<InventorySlot>();
            if (slot == null)
                return;

            if (slot.Item != null)
            {
                // ���� ���� ����
                int coinsEarned = 0;
                // ��޿� ���� ���� ���� �� ����
                switch (slot.Item.Rate)
                {
                    case RateType.Nomal:
                        coinsEarned = 10; // �Ϲ� ���
                        break;
                    case RateType.Magic:
                        coinsEarned = 50; // ���� ���
                        break;
                    case RateType.Rare:
                        coinsEarned = 200; // ��� ���
                        break;
                    default:
                        coinsEarned = 0;
                        break;
                }
                saveSystem.GetCoin(coinsEarned);

                slot.ClearSlot();
                mInventory.Sorting();
                Debug.Log($"��� ����");
                Debug.Log(coinsEarned);

            }
            else if (slot.Item == null)
            {
                Debug.Log("���� �� ��� �����ϴ�.");
            }
        }
        #endregion

        #region ������ ����
        private void Info()
        {
            /* for (int i = 0; i < slots.Length; i++)
             {
                 if (i == selectedButtonsIndex)
                 {
                     // ���â ����
                     if (i < 9)
                     {
                         // ��� ���Կ� �������� �ִ� ����
                         if (eqSlots[i].Item != null)
                         {
                             eqName.text = eqSlots[i].Item.Name;
                             eqDescription.text = eqSlots[i].Item.Description;
                             ivName.text = "-";
                             ivDescription.text = "";
                         }
                         // ��� ���Կ� �������� ���� ����
                         else if (eqSlots[i].Item == null)
                         {
                             eqName.text = "-";
                             eqDescription.text = "";
                             ivName.text = "-";
                             ivDescription.text = "";
                         }
                     }
                     // �κ��丮 ����
                     else if (i >= 9)
                     {
                         // �κ��丮 ���Կ� ������ �ִ� ����
                         if (ivSlots[i - 9].Item != null)
                         {
                             ivName.text = ivSlots[i - 9].Item.Name;
                             ivDescription.text = ivSlots[i - 9].Item.Description;

                             // ������ Ÿ�� �� �κ��丮�� ��� ��
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
                         // �κ��丮 ���Կ� �������� ���� ����
                         else if (ivSlots[i - 9].Item == null)
                         {
                             ivName.text = "-";
                             ivDescription.text = "";
                             eqName.text = "-";
                             eqDescription.text = "";
                         }
                     }
                 }
             }*/


            GameObject obj = EventSystem.current.currentSelectedGameObject;
            InventorySlot slot = obj.GetComponentInParent<InventorySlot>();


            if (slot == null)
                return;


            //if (slot)
            //{
            //    slot.GetComponent<Image>().color = HighlightedColor;
            //}
            //else
            //{
            //    slot.GetComponent<Image>().color = color;
            //}
            
            if (slot.isEquip == true)
            {
                // ���â ����
                // ��� ���Կ� �������� �ִ� ����
                if (slot.Item != null)
                {
                    eqName.text = slot.Item.Name;
                    eqDescription.text = slot.Item.Description;
                    ivName.text = "-";
                    ivDescription.text = "";
                }
                // ��� ���Կ� �������� ���� ����
                else if (slot.Item == null)
                {
                    eqName.text = "-";
                    eqDescription.text = "";
                    ivName.text = "-";
                    ivDescription.text = "";
                }

            }
            else
            {
                if (slot.Item != null)
                {
                    ivName.text = slot.Item.Name;
                    ivDescription.text = slot.Item.Description;

                    // ������ Ÿ�� �� �κ��丮�� ��� ��
                    switch (slot.Item.Type)
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
                // �κ��丮 ���Կ� �������� ���� ����
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


        // ������ ��� ���� (�ΰ��� ���� �� �ʱ�ȭ��)
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
            Debug.Log("������ �ʱ�ȭ");
        }
    }
}
