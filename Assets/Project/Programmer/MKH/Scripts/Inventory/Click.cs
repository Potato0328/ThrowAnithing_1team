using UnityEngine;
using UnityEngine.UI;

namespace MKH
{
    public class Click : MonoBehaviour
    {
        [SerializeField] GameObject[] buttons;
        [SerializeField] GameObject mInventorySlotsParent;
        [SerializeField] InventorySlot[] slots;
        int selectedButtonsIndex = 0;
        int buttonCount;

        [SerializeField] Color HighlightedColor;
        [SerializeField] Color color;

        private void Awake()
        {
            slots = mInventorySlotsParent.GetComponentsInChildren<InventorySlot>();
        }

        private void Start()
        {
            buttonCount = buttons.Length;
        }

        private void Update()
        {
            ButtonsControl();
        }

        private void ButtonsControl()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (selectedButtonsIndex > 0)
                {
                    selectedButtonsIndex = (selectedButtonsIndex - 1 + buttonCount) % buttonCount;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (selectedButtonsIndex < buttons.Length - 1)
                {
                    selectedButtonsIndex = (selectedButtonsIndex + 1) % buttonCount;
                }
            }

            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == selectedButtonsIndex)
                    buttons[i].GetComponent<Image>().color = HighlightedColor;
                else
                    buttons[i].GetComponent<Image>().color = color;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Function();
            }
        }

        #region �Է�
        private void Function()
        {


            switch (selectedButtonsIndex)
            {
                case 0:
                    print("1�� ��ư");
                    slots[0].UseItem();
                    break;
                case 1:
                    print("2�� ��ư");
                    slots[1].UseItem();
                    break;
                case 2:
                    print("3�� ��ư");
                    slots[2].UseItem();
                    break;
                case 3:
                    print("4�� ��ư");
                    slots[3].UseItem();
                    break;
                case 4:
                    print("5�� ��ư");
                    slots[4].UseItem();
                    break;
                case 5:
                    print("6�� ��ư");
                    slots[5].UseItem();
                    break;
                case 6:
                    print("7�� ��ư");
                    slots[6].UseItem();
                    break;
                case 7:
                    print("8�� ��ư");
                    slots[7].UseItem();
                    break;
                case 8:
                    print("9�� ��ư");
                    slots[8].UseItem();
                    break;
                case 9:
                    print("10�� ��ư");
                    slots[9].UseItem();
                    break;
                case 10:
                    print("11�� ��ư");
                    slots[10].UseItem();
                    break;
                case 11:
                    print("12�� ��ư");
                    slots[11].UseItem();
                    break;
            }
        }
    }
    #endregion
}
