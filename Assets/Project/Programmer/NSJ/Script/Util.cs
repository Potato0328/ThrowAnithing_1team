using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public static partial class Util
{
    public static StringBuilder Sb = new StringBuilder();

    private static Dictionary<float, WaitForSeconds> _delayDic = new Dictionary<float, WaitForSeconds>();
    private static Dictionary<float, WaitForSecondsRealtime> _delayRealTimeDic = new Dictionary<float, WaitForSecondsRealtime>();

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
    /// �ڷ�ƾ ������ WaitForSeconds ��������
    /// </summary>
    public static WaitForSecondsRealtime GetRealTimeDelay(this float delay)
    {
        if (_delayRealTimeDic.ContainsKey(delay) == false)
        {
            _delayRealTimeDic.Add(delay, new WaitForSecondsRealtime(delay));
        }

        return _delayRealTimeDic[delay];
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

    /// <summary>
    /// �÷����� ���İ�(����) ������ ���
    /// </summary>
    public static Color GetColor(this Color color, float a)
    {
        color.a = a;
        return color;
    }

}
