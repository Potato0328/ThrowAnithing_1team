using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MKH
{
    public class InventoryController : MonoBehaviour
    {
        [Header("�κ��丮 �� ���")]
        [SerializeField] GameObject inventory;                  // �κ��丮
        [SerializeField] GameObject mInventorySlotsParent;      // �κ��丮 ���� ������
        [SerializeField] InventorySlot[] ivSlots;               // �κ��丮 ���Ե�
        [SerializeField] GameObject mEquipmentSlotsParent;      // ��� ���� ������
        [SerializeField] InventorySlot[] eqSlots;               // ��� ���Ե�

        [Header("���� ��ư")]
        [SerializeField] GameObject[] buttons;                  // ���� ��ư
        int selectedButtonsIndex = 9;                           // ���� ���� ��ġ
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

        private void Awake()
        {
            // �κ��丮, ��� ���Ե� �ҷ�����
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

            ButtonsControl();               // Ű ����
            Use(selectedButtonsIndex);      // Ű ��ư ����
            Info();                         // ������ ����
        }

        #region Ű ����
        private void ButtonsControl()
        {
            float x = Input.GetAxisRaw("Horizontal");       // �� �� ����
            float y = Input.GetAxisRaw("Vertical");         // �� �� ����

            // �κ��丮 ���� ���� ���� ���� ����

            // ����
            if (x == -1)
            {
                if (selectedButtonsIndex > 0 && axisInUse == false)
                {
                    axisInUse = true;
                    selectedButtonsIndex -= 1;
                    Debug.Log("����");
                }
            }
            // ������
            else if (x == 1)
            {
                if (selectedButtonsIndex < buttons.Length - 1 && axisInUse == false)
                {
                    axisInUse = true;
                    selectedButtonsIndex += 1;
                    Debug.Log("������");
                }
            }
            // ��
            else if (y == 1)
            {
                if (selectedButtonsIndex > 2 && axisInUse == false)
                {
                    axisInUse = true;
                    selectedButtonsIndex -= 3;
                    Debug.Log("��");
                }
            }
            // �Ʒ�
            else if (y == -1)
            {
                if (selectedButtonsIndex < buttons.Length - 3 && axisInUse == false)
                {
                    axisInUse = true;
                    selectedButtonsIndex += 3;
                    Debug.Log("�Ʒ�");
                }
            }
            // Ű ���� ����
            else
            {
                axisInUse = false;
            }


            // ���� ���� �� ������
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
        #endregion

        #region ������ ��ư ����
        private void Use(int index)
        {
            // ������ ���� - �κ��丮�� ���� ����
            if (Input.GetButtonDown("Interaction"))
            {
                // ���
                if (index < 9)
                {
                    return;
                }
                // �κ��丮
                else if (index >= 9)
                {
                    ivSlots[index - 9].UseItem();
                    Debug.Log($"�κ��丮 {index - 9}��ư ����");
                    Debug.Log($"�κ��丮 {index - 9}�� ����");
                }
            }

            // ������ ���� - �κ��丮, ��� �� �� ���� ����
            if (Input.GetButtonDown("Negative"))
            {
                // ���
                if (index < 9)
                {
                    eqSlots[index].RemoveEquipmentSlot();
                    Debug.Log($"��� {index}��ư ����");
                    Debug.Log($"��� {index}�� ����");

                }
                // �κ��丮
                else if (index >= 9)
                {
                    ivSlots[index - 9].ClearSlot();
                    Debug.Log($"�κ��丮 {index - 9}��ư ����");
                    Debug.Log($"�κ��丮 {index - 9}�� ����");
                }
            }
        }
        #endregion

        #region ������ ����
        private void Info()
        {
            for (int i = 0; i < buttons.Length; i++)
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
                            ivName.text = null;
                            ivDescription.text = null;
                        }
                        // ��� ���Կ� �������� ���� ����
                        else if (eqSlots[i].Item == null)
                        {
                            eqName.text = null;
                            eqDescription.text = null;
                            ivName.text = null;
                            ivDescription.text = null;
                        }
                    }

                    // �κ��丮 ����
                    if (i >= 9)
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
                                    else
                                    {
                                        eqName.text = null;
                                        eqDescription.text = null;
                                    }
                                    break;
                                case ItemType.Shirts:
                                    if (eqSlots[1].Item != null)
                                    {
                                        eqName.text = eqSlots[1].Item.Name;
                                        eqDescription.text = eqSlots[1].Item.Description;
                                    }
                                    else
                                    {
                                        eqName.text = null;
                                        eqDescription.text = null;
                                    }
                                    break;
                                case ItemType.Glasses:
                                    if (eqSlots[2].Item != null)
                                    {
                                        eqName.text = eqSlots[2].Item.Name;
                                        eqDescription.text = eqSlots[2].Item.Description;
                                    }
                                    else
                                    {
                                        eqName.text = null;
                                        eqDescription.text = null;
                                    }
                                    break;
                                case ItemType.Gloves:
                                    if (eqSlots[3].Item != null)
                                    {
                                        eqName.text = eqSlots[3].Item.Name;
                                        eqDescription.text = eqSlots[3].Item.Description;
                                    }
                                    else
                                    {
                                        eqName.text = null;
                                        eqDescription.text = null;
                                    }
                                    break;
                                case ItemType.Pants:
                                    if (eqSlots[4].Item != null)
                                    {
                                        eqName.text = eqSlots[4].Item.Name;
                                        eqDescription.text = eqSlots[4].Item.Description;
                                    }
                                    else
                                    {
                                        eqName.text = null;
                                        eqDescription.text = null;
                                    }
                                    break;
                                case ItemType.Earring:
                                    if (eqSlots[5].Item != null)
                                    {
                                        eqName.text = eqSlots[5].Item.Name;
                                        eqDescription.text = eqSlots[5].Item.Description;
                                    }
                                    else
                                    {
                                        eqName.text = null;
                                        eqDescription.text = null;
                                    }
                                    break;
                                case ItemType.Ring:
                                    if (eqSlots[6].Item != null)
                                    {
                                        eqName.text = eqSlots[6].Item.Name;
                                        eqDescription.text = eqSlots[6].Item.Description;
                                    }
                                    else
                                    {
                                        eqName.text = null;
                                        eqDescription.text = null;
                                    }
                                    break;
                                case ItemType.Shoes:
                                    if (eqSlots[7].Item != null)
                                    {
                                        eqName.text = eqSlots[7].Item.Name;
                                        eqDescription.text = eqSlots[7].Item.Description;
                                    }
                                    else
                                    {
                                        eqName.text = null;
                                        eqDescription.text = null;
                                    }
                                    break;
                                case ItemType.Necklace:
                                    if (eqSlots[8].Item != null)
                                    {
                                        eqName.text = eqSlots[8].Item.Name;
                                        eqDescription.text = eqSlots[8].Item.Description;
                                    }
                                    else
                                    {
                                        eqName.text = null;
                                        eqDescription.text = null;
                                    }
                                    break;
                            }
                        }
                        // �κ��丮 ���Կ� �������� ���� ����
                        else if (ivSlots[i - 9].Item == null)
                        {
                            ivName.text = null;
                            ivDescription.text = null;
                            eqName.text = null;
                            eqDescription.text = null;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
