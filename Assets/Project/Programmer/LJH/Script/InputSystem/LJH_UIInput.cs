using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LJH_UIInput : MonoBehaviour
{
    PlayerInput playerInput;
    Button button;
    private void Start()
    {
        
    }
    void Update()
    {
        button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        changeColor();
    }

    void changeColor()
    {
        ColorBlock colorBlock = button.colors;

        //(r, g, b, a) ���� ���������� normal Color ����
        colorBlock.normalColor = Color.red;

        button.colors = colorBlock;
    }
}
