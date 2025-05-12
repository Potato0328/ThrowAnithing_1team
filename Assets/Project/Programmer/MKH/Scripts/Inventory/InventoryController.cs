using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MKH
{
    [RequireComponent(typeof(SaveSystem))]
    public class InventoryController : MonoBehaviour
    {
        [Header("�г�")]
        [SerializeField] GameObject blueChipPanel;              // ���Ĩ �г�
        [SerializeField] GameObject inventory;                  // �κ��丮 ������Ʈ

        [Header("�κ��丮, ���")]
        [SerializeField] InventoryMain mInventory;              // �κ��丮
        [SerializeField] EquipmentInventory mEquipInventory;    // ���


        [Header("������ ����")]
        [SerializeField] TMP_Text ivName;                       // �κ��丮 ������ �̸�
        [SerializeField] TMP_Text ivDescription;                // �κ��丮 ������ ����
        [SerializeField] TMP_Text eqName;                       // ��� ������ �̸�
        [SerializeField] TMP_Text eqDescription;                // ��� ������ ����

        [Header("ȿ����")]
        [SerializeField] public AudioClip ivChoice;             // ���� ȿ����
        [SerializeField] public AudioClip ivBreak;              // ���� ȿ����
        [SerializeField] public AudioClip emptyClick;           // �� ���� ȿ����
        [SerializeField] public AudioClip clickMove;            // ���� �̵� ȿ����

        [Header("���� ����")]
        [SerializeField] SaveSystem saveSystem;                 // ������ ���� ���� ��Ȱ

        [Header("����Ʈ")]
        [SerializeField] GameObject effectUI;                   // ����Ʈ 
        [SerializeField] GameObject clickEffect;                // Ŭ�� ����Ʈ
        [SerializeField] GameObject choiceEffect;               // ���� ����Ʈ
        [SerializeField] GameObject breakEffect;                // ���� ����Ʈ

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

        #region ������ ��ư ����
        // ��� ����, ��ü
        public void Choice()
        {
            GameObject obj = EventSystem.current.currentSelectedGameObject;
            // ����ó��
            if (obj == null)
                return;

            InventorySlot slot = obj.GetComponentInParent<InventorySlot>();
            // ����ó��
            if (slot == null || slot.isEquip == true)
                return;

            if (slot.Item != null)
            {
                SoundManager.PlaySFX(ivChoice);
                slot.UseItem();
                GameObject obj1 = ObjectPool.Get(choiceEffect, new Vector3(slot.transform.position.x, slot.transform.position.y, 0), Quaternion.identity, 0.5f);
                obj1.transform.SetParent(effectUI.transform);
                Debug.Log("��� ����");
            }
            else if (slot.Item == null)
            {
                Debug.Log("���� �� ��� �����ϴ�.");
            }
        }

        // ��� ����
        public void Break()
        {
            GameObject obj = EventSystem.current.currentSelectedGameObject;
            // ����ó��
            if (obj == null)
                return;

            InventorySlot slot = obj.GetComponentInParent<InventorySlot>();
            // ����ó��
            if (slot == null || slot.isEquip == true)
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

                SoundManager.PlaySFX(ivBreak);
                slot.RemoveItem();
                GameObject obj1 = ObjectPool.Get(breakEffect, new Vector3(slot.transform.position.x, slot.transform.position.y, 0), Quaternion.identity, 1f);
                obj1.transform.SetParent(effectUI.transform);
                Debug.Log($"��� ����");

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

            if (slot.isEquip) // ��� ����
            {
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
            else if (!slot.isEquip) //�κ��丮 ����
            {
                if (slot.Item != null)
                {
                    ivName.text = slot.Item.Name;
                    ivDescription.text = slot.Item.Description;

                    // ������ Ÿ�� �� �κ��丮�� ��� ��
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

            Debug.Log("������ �ʱ�ȭ");
        }
    }
}
