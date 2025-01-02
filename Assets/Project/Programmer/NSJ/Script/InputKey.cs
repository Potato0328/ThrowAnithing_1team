

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class InputKey : MonoBehaviour
{
    public static InputKey Instance;
    public enum Axis { None, Axis, AxisUp, AxisDown }
    public struct InputStruct
    {
        public string Name;
        public Axis Axis;
        public bool PrevPress;
        public bool CurPress;
    }

    /// <summary>
    /// a/d , LeftStick
    /// </summary>
    public static InputStruct Horizontal;
    /// <summary>
    /// w/s , LeftStick
    /// </summary>
    public static InputStruct Vertical;
    /// <summary>
    ///  Mouse, RightStick
    /// </summary>
    public static InputStruct MouseX;
    /// <summary>
    /// Mouse, RightStick
    /// </summary>
    public static InputStruct MouseY;
    /// <summary>
    /// Mouse 0, B
    /// </summary>
    public static InputStruct Throw;
    /// <summary>
    /// Mouse 1, Y
    /// </summary>
    public static InputStruct Special;
    /// <summary>
    /// Space , A
    /// </summary>
    public static InputStruct Jump;
    /// <summary>
    /// v , X
    /// </summary>
    public static InputStruct Melee;
    /// <summary>
    /// Tab , LT
    /// </summary>
    public static InputStruct RockOn;
    /// <summary>
    /// C , LB
    /// </summary>
    public static InputStruct RockCancel;
    /// <summary>
    ///  C , D-pad Up
    /// </summary>
    //public static InputStruct Negative;     �ӽ� ����
    /// <summary>
    ///  E , RB
    /// </summary>
    public static InputStruct Interaction;
    /// <summary>
    /// Left Shift , RT
    /// </summary>
    public static InputStruct Dash;
    /// <summary>
    ///  Q, LS
    /// </summary>
    public static InputStruct Drain;
    /// <summary>
    ///  esc, Start
    /// </summary>
    public static InputStruct Cancel;
    /// <summary>
    ///  B , D-pad Down
    /// </summary>
    public static InputStruct Inventory;
    /// <summary>
    /// F1 , ?
    /// </summary>
    public static InputStruct Cheat;
    /// <summary>
    ///  MouseWheel , ?
    /// </summary>
    public static InputStruct Mouse_ScrollWheel;
    /// <summary>
    ///  Q , X
    /// </summary>
    public static InputStruct Decomposition;
    /// <summary>
    ///  E , B
    /// </summary>
    public static InputStruct InventoryEquip;
    /// <summary>
    ///  c , D-Pad Up
    /// </summary>
    public static InputStruct PopUpClose;


    private static Dictionary<string, InputStruct> InputStructDic = new Dictionary<string, InputStruct>();
    private static List<InputStruct> inputStructs = new List<InputStruct>();

    private void Awake()
    {
        InitSingleTon();

        Init();
    }
    private void InitSingleTon()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        foreach(InputStruct inputStruct in inputStructs)
        {
            SetPrevPress(InputStructDic[inputStruct.Name], InputStructDic[inputStruct.Name].CurPress);
        }
    }
    private void Init()
    {
        InputStructDic.Clear();
        Horizontal = GetInputStruct("Horizontal", Axis.Axis);
        Vertical = GetInputStruct("Vertical", Axis.Axis);
        MouseX = GetInputStruct("Mouse X", Axis.Axis);
        MouseY = GetInputStruct("Mouse Y", Axis.Axis);
        Throw = GetInputStruct("Throw", Axis.None);
        Special = GetInputStruct("Special", Axis.None);
        Jump = GetInputStruct("Jump", Axis.None);
        Melee = GetInputStruct("Melee", Axis.None);
        RockOn = GetInputStruct("Rock On", Axis.AxisUp);
        //Negative = GetInputStruct("Negative", Axis.AxisUp);
        Interaction = GetInputStruct("Interaction", Axis.None);
        Dash = GetInputStruct("Dash", Axis.AxisUp);
        Drain = GetInputStruct("Drain", Axis.None);
        Cancel = GetInputStruct("Cancel", Axis.None);
        Inventory = GetInputStruct("Inventory", Axis.AxisDown);
        Cheat = GetInputStruct("Cheat", Axis.None);
        Mouse_ScrollWheel = GetInputStruct("Mouse ScrollWheel", Axis.None);
        Decomposition = GetInputStruct("Decomposition", Axis.None);
        InventoryEquip = GetInputStruct("InventoryEquip", Axis.None);
        PopUpClose = GetInputStruct("PopUp Close", Axis.AxisUp);
        RockCancel = GetInputStruct("Rock Cancel", Axis.None);
    }
    /// <summary>
    /// ��ư�� ������ ���� ȣ��
    /// </summary>
    public static bool GetButton(InputStruct inputStruct)
    {
        inputStruct = InputStructDic[inputStruct.Name];

        if (inputStruct.Axis == Axis.AxisUp)
        {
            float x = Input.GetAxisRaw(inputStruct.Name);
            if (x > 0)
                return true;
            else
                return false;
        }
        else if (inputStruct.Axis == Axis.AxisDown)
        {
            float x = Input.GetAxisRaw(inputStruct.Name);
            if (x < 0)
                return true;
            else
                return false;
        }
        else
        {
            return Input.GetButton(inputStruct.Name);
        }
    }

    public static bool GetButton(string name)
    {
        if (InputStructDic[name].Axis == Axis.AxisUp)
        {
            float x = Input.GetAxisRaw(InputStructDic[name].Name);
            if (x > 0)
                return true;
            else
                return false;
        }
        else if (InputStructDic[name].Axis == Axis.AxisDown)
        {
            float x = Input.GetAxisRaw(InputStructDic[name].Name);
            if (x < 0)
                return true;
            else
                return false;
        }
        else
        {
            return Input.GetButton(InputStructDic[name].Name);
        }
    }
    /// <summary>
    /// ��ư�� ������ �� ȣ��
    /// </summary>
    public static bool GetButtonDown(InputStruct inputStruct)
    {
        if (InputStructDic[inputStruct.Name].Axis == Axis.AxisUp)
        {
            float x = Input.GetAxisRaw(InputStructDic[inputStruct.Name].Name);
            // ��ư ������ ������
            if (x == 0 && InputStructDic[InputStructDic[inputStruct.Name].Name].PrevPress == true)
            {
                SetCurPress(InputStructDic[inputStruct.Name], false);
            }

            if (x > 0 && InputStructDic[inputStruct.Name].PrevPress == false)
            {
                SetCurPress(InputStructDic[inputStruct.Name], true);
                return true;
            }
            else
                return false;
        }
        else if (InputStructDic[inputStruct.Name].Axis == Axis.AxisDown)
        {
            float x = Input.GetAxisRaw(InputStructDic[inputStruct.Name].Name);
            if (x == 0 && InputStructDic[InputStructDic[inputStruct.Name].Name].PrevPress == true)
                SetCurPress(InputStructDic[inputStruct.Name], false);

            if (x < 0 && InputStructDic[inputStruct.Name].PrevPress == false)
            {
                SetCurPress(InputStructDic[inputStruct.Name], true);
                return true;
            }
            else
                return false;
        }
        else
        {
            return Input.GetButtonDown(inputStruct.Name);
        }
    }
    public static bool GetButtonDown(string name)
    {
        if (InputStructDic[name].Axis == Axis.AxisUp)
        {
            float x = Input.GetAxisRaw(InputStructDic[name].Name);
            if (x == 0 && InputStructDic[InputStructDic[name].Name].PrevPress == true)
            {
                SetCurPress(InputStructDic[name], false);
            }
            if (x > 0 && InputStructDic[name].PrevPress == false)
            {
                SetCurPress(InputStructDic[name], true);
                return true;
            }
            else
                return false;
        }
        else if (InputStructDic[name].Axis == Axis.AxisDown)
        {
            float x = Input.GetAxisRaw(InputStructDic[name].Name);
            if (x == 0 && InputStructDic[InputStructDic[name].Name].PrevPress == true)
                SetCurPress(InputStructDic[name], false);

            if (x < 0 && InputStructDic[name].PrevPress == false)
            {
                SetCurPress(InputStructDic[name], true);
                return true;
            }
            else
                return false;
        }
        else
        {
            return Input.GetButtonDown(name);
        }
    }

    //IEnumerator
    /// <summary>
    /// ��ư �����⸦ �׸��ы� ȣ��
    /// </summary>
    public static bool GetButtonUp(InputStruct inputStruct)
    {
        inputStruct = InputStructDic[inputStruct.Name];

        if (InputStructDic[inputStruct.Name].Axis == Axis.AxisUp)
        {
            float x = Input.GetAxisRaw(InputStructDic[inputStruct.Name].Name);
            if (x > 0 && InputStructDic[inputStruct.Name].PrevPress == false)
                SetCurPress(InputStructDic[inputStruct.Name], true);

            if (x == 0 && InputStructDic[inputStruct.Name].PrevPress == true)
            {
                SetCurPress(InputStructDic[inputStruct.Name], false);
                return true;
            }
            else
                return false;
        }
        else if (InputStructDic[inputStruct.Name].Axis == Axis.AxisDown)
        {
            float x = Input.GetAxisRaw(InputStructDic[inputStruct.Name].Name);
            if (x < 0 && InputStructDic[inputStruct.Name].PrevPress == false)
                SetCurPress(InputStructDic[inputStruct.Name], true);

            if (x == 0 && InputStructDic[inputStruct.Name].PrevPress == true)
            {
                SetCurPress(InputStructDic[inputStruct.Name], false);
                return true;
            }
            else
                return false;
        }
        else
        {
            return Input.GetButtonUp(inputStruct.Name);
        }
    }
    public static bool GetButtonUp(string name)
    {
        if (InputStructDic[name].Axis == Axis.AxisUp)
        {
            float x = Input.GetAxisRaw(InputStructDic[name].Name);
            if (x > 0 && InputStructDic[name].PrevPress == false)
                SetCurPress(InputStructDic[name], true);

            if (x == 0 && InputStructDic[name].PrevPress == true)
            {
                SetCurPress(InputStructDic[name], false);
                return true;
            }
            else
                return false;
        }
        else if (InputStructDic[name].Axis == Axis.AxisDown)
        {
            float x = Input.GetAxisRaw(InputStructDic[name].Name);
            if (x < 0 && InputStructDic[name].PrevPress == false)
                SetCurPress(InputStructDic[name], true);

            if (x == 0 && InputStructDic[name].PrevPress == true)
            {
                SetCurPress(InputStructDic[name],false);
                return true;
            }
            else
                return false;
        }
        else
        {
            return Input.GetButtonUp(name);
        }
    }
    /// <summary>
    /// Axis �� 1, 0 , -1 ��ȯ
    /// </summary>
    public static float GetAxisRaw(InputStruct inputStruct)
    {
        if (inputStruct.Axis == Axis.None)
            return 0;

        return Input.GetAxisRaw(inputStruct.Name);
    }
    public static float GetAxisRaw(string name)
    {
        if (InputStructDic[name].Axis == Axis.None)
            return 0;

        return Input.GetAxisRaw(name);
    }
    /// <summary>
    /// Axis �� -1 ~ 1 ��ȯ
    /// </summary>
    public static float GetAxis(InputStruct inputStruct)
    {
        if (inputStruct.Axis == Axis.None)
            return 0;

        return Input.GetAxis(inputStruct.Name);
    }
    public static float GetAxis(string name)
    {
        if (InputStructDic[name].Axis == Axis.None)
            return 0;

        return Input.GetAxis(name);
    }

    private static void SetCurPress(InputStruct inputStruct, bool isPress)
    {
        InputStruct newInputStruct = InputStructDic[inputStruct.Name];
        newInputStruct.CurPress = isPress;
        InputStructDic[inputStruct.Name] = newInputStruct;
    }

    private static void SetPrevPress(InputStruct inputStruct, bool isPress)
    {
        InputStruct newInputStruct = InputStructDic[inputStruct.Name];
        newInputStruct.PrevPress = isPress;
        InputStructDic[inputStruct.Name] = newInputStruct;
    }

    private static InputStruct GetInputStruct(string name, Axis axis)
    {
        InputStruct inputStruct = new InputStruct();
        inputStruct.Name = name;
        inputStruct.Axis = axis;
        inputStruct.CurPress = false;
        InputStructDic.Add(name, inputStruct);
        inputStructs.Add(inputStruct);
        return inputStruct;
    }
}
