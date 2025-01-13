using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace MKH
{
    public class BlueChipChoiceController : MonoBehaviour
    {
        [Inject]
        PlayerData playerData;

        [SerializeField] Button[] removeButtons;
        [SerializeField] GameObject popUp;
        [SerializeField] GameObject errorPopUp;
        [SerializeField] BlueChipPanel blueChipPanel;
        [SerializeField] BlueChipChoicePanel blueChipChoicePanel;
        int popUpChoice;
        PlayerController _player;

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        public void Canecel()
        {
            gameObject.SetActive(false);
            blueChipPanel = playerData.Inventory.BlueChipPanel;
            blueChipChoicePanel = playerData.Inventory.BlueChipChoicePanel;
        }

        public void Choice(int number)
        {
            for (int i = 0; i < blueChipPanel.mSlots.Length; i++)
            {
                if (blueChipPanel.mSlots[i].Effect == null)
                {
                    blueChipPanel.mSlots[i].AddEffect(blueChipChoicePanel.choiceSlots[number].Effect);
                    blueChipChoicePanel.blueChipSlots[i].AddEffect(blueChipChoicePanel.choiceSlots[number].Effect);
                    _player.AddAdditional(blueChipChoicePanel.choiceSlots[number].Effect);

                    gameObject.SetActive(false);

                    return;
                }
                else if (blueChipPanel.mSlots[0].Effect != null
                && blueChipPanel.mSlots[1].Effect != null
                && blueChipPanel.mSlots[2].Effect != null
                && blueChipPanel.mSlots[3].Effect != null)
                {
                    ErrorPopUp();
                }
            }
        }

        public void PopUp(int number)
        {
            popUp.SetActive(true);
            popUpChoice = number;
        }

        public void ErrorPopUp()
        {
            StartCoroutine(Error());
        }

        public void Remove()
        {
            _player.RemoveAdditional(blueChipPanel.mSlots[popUpChoice].Effect);
            blueChipPanel.mSlots[popUpChoice].ClearSlot();
            blueChipChoicePanel.blueChipSlots[popUpChoice].ClearSlot();
            popUp.SetActive(false);
            EventSystem.current.SetSelectedGameObject(blueChipChoicePanel.button.gameObject);
        }

        public void ClosePopUp()
        {
            popUp.SetActive(false);
            EventSystem.current.SetSelectedGameObject(blueChipChoicePanel.button.gameObject);
        }

        IEnumerator Error()
        {
            errorPopUp.SetActive(true);
            yield return 1f.GetDelay();
            errorPopUp.SetActive(false);
        }
    }
}
