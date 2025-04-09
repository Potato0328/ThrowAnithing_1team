using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MKH
{
    public class BlueChipChoicePanel : MonoBehaviour
    {
        [SerializeField] public Button button;
        [SerializeField] GameObject blueChipSlotsParent;
        [SerializeField] public BlueChipSlot[] blueChipSlots;

        [SerializeField] GameObject choiceSlotsParnet;
        [SerializeField] public BlueChipSlot[] choiceSlots;

        [SerializeField] BlueChipList blueChip;

        [SerializeField] public List<AdditionalEffect> blueChipList;

        [SerializeField] AudioClip clickMove;
        [SerializeField] public GameObject effect;
        [SerializeField] GameObject clickEffect;


        private void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(button.gameObject);
            if (EventSystem.current.currentSelectedGameObject.transform.position == Vector3.zero)
            {
                effect.transform.position = new Vector3(460, 300, 0);
            }
            else
            {
                effect.transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
            }
            RandomBlueChip();
        }

        private void Awake()
        {
            blueChipSlots = blueChipSlotsParent.GetComponentsInChildren<BlueChipSlot>();
            choiceSlots = choiceSlotsParnet.GetComponentsInChildren<BlueChipSlot>();
            blueChipList = new List<AdditionalEffect>(blueChip.blueChipList);
            Setting();
        }

        private void Update()
        {
            ChoiceMove();
        }

        public bool AcquireEffect(AdditionalEffect effect)
        {
            for (int i = 0; i < blueChipSlots.Length; i++)
            {
                if (blueChipSlots[i].Effect == null)
                {
                    blueChipSlots[i].AddEffect(effect);
                    return true;
                }
            }
            return false;
        }

        public void Setting()
        {
            for (int i = 0; i < blueChipSlots.Length; i++)
            {
                blueChipSlots[i].SetSlot();
            }
        }

        public void RandomBlueChip()
        {
            List<AdditionalEffect> shuffled = new List<AdditionalEffect>(blueChipList);

            // ÇÇ¼Å-¿¹ÀÌÃ÷ ¼ÅÇÃ
            for (int i = shuffled.Count - 1; i > 0; i--)
            {
                int rand = Random.Range(0, i + 1); // 0ºÎÅÍ i±îÁö Æ÷ÇÔ
                (shuffled[i], shuffled[rand]) = (shuffled[rand], shuffled[i]);
            }

            for (int i = 0; i < choiceSlots.Length; i++)
            {
                AdditionalEffect effect = shuffled[i];
                choiceSlots[i].AddEffect(effect);
                choiceSlots[i].ListIndex = blueChipList.IndexOf(effect);
            }
        }

        private void ChoiceMove()
        {
            GameObject obj = EventSystem.current.currentSelectedGameObject;

            if (obj == null)
                return;
            if (obj != null)
            {
                if (InputKey.PlayerInput.actions["UIMove"].WasPressedThisFrame())
                {
                    SoundManager.PlaySFX(clickMove);
                    effect.transform.position = obj.transform.position;
                }
                else if (InputKey.PlayerInput.actions["LeftClick"].WasPressedThisFrame())
                {
                    effect.transform.position = obj.transform.position;
                }
            }
        }
    }
}

