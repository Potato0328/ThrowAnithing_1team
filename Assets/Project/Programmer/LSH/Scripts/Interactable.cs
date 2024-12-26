using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Range(2f, 5f)] [SerializeField] float overlapSphereRadius;
    Collider[] hitColliders;

    [SerializeField] bool isInSphere; //T���������Ǿ������� F�ƴ�
    [SerializeField] GameObject pressKeyUI;

    private void Start()
    {
        isInSphere = false;
        pressKeyUI.SetActive(false);
    }


    private void Update()
    {
        
        hitColliders = Physics.OverlapSphere(transform.position, overlapSphereRadius);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag == Tag.Player)
            {
                isInSphere = true;
                pressKeyUI.SetActive(true);
            }
            else
            {
                isInSphere = false;
                pressKeyUI.SetActive(false);
            }

        }

        if (isInSphere)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("�������� �� �ֺ��� �������� Ƣ���");
            }
        }

    }


}
