using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroText : Intro
{
    TMP_Text myText;

    private void OnEnable()
    {
        for (int i = 0; i < textArray.Length; i++)
        {
            //Comment �ش� ��ũ��Ʈ�� textArary[i] �� ������ myText ������ textArray[i]�� �Ҵ�
            if (this == textArray[i])
                myText = textArray[i].GetComponent<TMP_Text>();
        }


        textStorage = myText.text;
        myText.text = " ";
        StartCoroutine(TextWriteCo(myText, textStorage));
    }

}
