using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeText : MonoBehaviour
{
    [SerializeField] Color textCol;
    [SerializeField] Canvas canvas;

    Coroutine textCo;

    private void OnEnable()
    {
        // �ӽ÷� EditorOnly �±� �ھƳ���
        canvas = GameObject.FindWithTag(Tag.EditorOnly).GetComponent<Canvas>();
        gameObject.transform.SetParent(canvas.transform);
        textCol = gameObject.GetComponent<Image>().color;
    }

    private void OnDisable()
    {
        StopCoroutine(textCo);
        textCo = null;
    }

    private void Update()
    {
        TextHide();
        if(textCo == null)
        textCo = StartCoroutine(TextDelete());
    }

    void TextHide()
    {
        textCol.a -= 1f * Time.deltaTime;
        gameObject.GetComponent<Image>().color = textCol;
    }

    IEnumerator TextDelete()
    {
        yield return 1f.GetRealTimeDelay();
        
        // �׽�Ʈ �ڵ�
        //gameObject.SetActive(false);
        
        Destroy(gameObject);
    }
}
