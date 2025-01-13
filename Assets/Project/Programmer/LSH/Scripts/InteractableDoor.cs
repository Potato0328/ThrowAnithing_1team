using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : MonoBehaviour
{
    [SerializeField] GameObject randomBox;
    [SerializeField] GameObject portal;

    [Range(2f, 5f)][SerializeField] float overlapSphereRadius; //�������� ��ó ����
    Collider[] hitColliders; //�ش� ��ü�� ��� ��� �ݶ��̴��� �־�� �迭
    [SerializeField] bool isInSphere; //T���������Ǿ������� F�ƴ�


    private void Start()
    {
        portal.SetActive(false);
    }


    private void Update()
    {

        hitColliders = Physics.OverlapSphere(transform.position, overlapSphereRadius);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag == Tag.Player)
            {
                isInSphere = true;
            }
            else
            {
                isInSphere = false;
            }

        }


        if (isInSphere)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                portal.SetActive(true);
            }
        }



    }

    }
