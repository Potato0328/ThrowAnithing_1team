using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Failure : MonoBehaviour
{
    [SerializeField] GameObject scoreBoard;
    Coroutine co;


    private void OnEnable()
    {
        //�ٸ� UI �� �ݾƾ���
        // .SetActive(false);
        co = StartCoroutine(GameOver());
    }

    private void OnDisable()
    {
        StopCoroutine(co);
    }


    IEnumerator GameOver()
    {
        Debug.Log("������");
        yield return 2f.GetRealTimeDelay();

        scoreBoard.SetActive(true);
        gameObject.SetActive(false);
    }


}
