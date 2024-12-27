using UnityEngine;
using UnityEngine.UI;

namespace MKH
{
    public class Click : MonoBehaviour
    {
        [SerializeField] GameObject[] buttons;
        [SerializeField] GameObject mInventorySlotsParent;
        [SerializeField] InventorySlot[] ivSlots;
        [SerializeField] GameObject mEquipmentSlotsParent;
        [SerializeField] InventorySlot[] eqSlots;
        int selectedButtonsIndex = 9;
        int buttonCount;
        private bool axisInUse = false;

        [SerializeField] Color HighlightedColor;
        [SerializeField] Color color;

        private void Awake()
        {
            ivSlots = mInventorySlotsParent.GetComponentsInChildren<InventorySlot>();
            eqSlots = mEquipmentSlotsParent.GetComponentsInChildren<InventorySlot>();
        }

        private void Start()
        {
            buttonCount = buttons.Length;
            axisInUse = false;
        }

        private void Update()
        {
            ButtonsControl();
            Function();
        }

        #region Ű ����
        // Ű���� ����
        private void ButtonsControl()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            if (x == -1)        // ����
            {
                if (selectedButtonsIndex > 0 && axisInUse == false)
                {
                    axisInUse = true;
                    selectedButtonsIndex -= 1;
                    Debug.Log("��");
                }
            }
            else if (x == 1)    // ����
            {
                if (selectedButtonsIndex < buttons.Length - 1 && axisInUse == false)
                {
                    axisInUse = true;
                    selectedButtonsIndex += 1;
                    Debug.Log("��");
                }
            }
            else if (y == 1)    // ��
            {
                if (selectedButtonsIndex > 2 && axisInUse == false)
                {
                    axisInUse = true;
                    selectedButtonsIndex -= 3;
                    Debug.Log("��");
                }
            }
            else if (y == -1)   // �Ʒ�
            {
                if (selectedButtonsIndex < buttons.Length - 3 && axisInUse == false)
                {
                    axisInUse = true;
                    selectedButtonsIndex += 3;
                    Debug.Log("�Ʒ�");
                }
            }
            else
            {
                axisInUse = false;
            }

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

        #region ������ ��� ��ư
        private void Use(int index)
        {
            // ������ ����
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (index < 9)
                {
                    return;
                }
                else if(index >= 9)
                {
                    ivSlots[index - 9].UseItem();
                    Debug.Log($"{index - 9}��ư ����");
                    Debug.Log($"{index - 9}�� ����");
                }
            }

            // ������ ����
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (index < 9)
                {
                    eqSlots[index].RemoveEquipmentSlot();
                    Debug.Log($"{index}��ư ����");
                    Debug.Log($"{index}�� ��� ����");

                }
                else if (index >= 9)
                {
                    ivSlots[index - 9].ClearSlot();
                    Debug.Log($"{index - 9}��ư ����");
                    Debug.Log($"{index - 9}�� ����");
                }
            }
        }

        private void Function()
        {
            Use(selectedButtonsIndex);
        }
        #endregion
    }
}
