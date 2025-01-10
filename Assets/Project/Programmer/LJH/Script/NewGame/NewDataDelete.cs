using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NewDataDelete : MonoBehaviour
{
    [SerializeField] SlotManager slotManager;

    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;

    void Start()
    {
        Debug.Log(yesButton.onClick.ToString());
        Debug.Log(noButton.onClick.ToString());
    }

    void Update()
    {
        if (InputKey.GetButtonDown(InputKey.PrevInteraction))
        {
            Debug.Log("��������");
            yesButton.onClick.Invoke();
        }
        if (InputKey.GetButtonDown(InputKey.PopUpClose))
        {
            Debug.Log("�������");
            noButton.onClick.Invoke();
        }
    }

    

    
}
