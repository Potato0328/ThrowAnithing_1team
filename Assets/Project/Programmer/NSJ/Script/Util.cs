using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.Hardware;
using UnityEngine;


public static partial class Util
{
    public static StringBuilder Sb = new StringBuilder();

    private static Dictionary<float, WaitForSeconds> _delayDic = new Dictionary<float, WaitForSeconds>();

    /// <summary>
    /// �ڷ�ƾ ������ WaitForSeconds ��������
    /// </summary>
    public static WaitForSeconds GetDelay(this float delay)
    {
        if(_delayDic.ContainsKey(delay) == false)
        {
            _delayDic.Add(delay, new WaitForSeconds(delay));
        }
        
        return _delayDic[delay];
    }

    /// <summary>
    /// TMP�� ���� StringBuilder ��ȯ �Լ�
    /// </summary>
    public static StringBuilder GetText<T>(this T value)
    {
        Sb.Clear();
        Sb.Append(value);
        return Sb;
    }

}
