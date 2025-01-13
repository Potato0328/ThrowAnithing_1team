using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractableDoor : MonoBehaviour
{
    [SerializeField] GameObject randomBox;
    [SerializeField] GameObject portal;
    GameObject[] monsterCount;

    [Range(20f, 50f)][SerializeField] float overlapSphereRadius; //�������� ��ó ����
    Collider[] hitColliders; //�ش� ��ü�� ��� ��� �ݶ��̴��� �־�� �迭
    [SerializeField] bool isInSphere; //T���������Ǿ������� F�ƴ�
    [SerializeField] bool isMonsterAllDead; //T�������Ǹ��Ͱ����׾��� F�ƴ�


    private void Start()
    {
        monsterCount = GameObject.FindGameObjectsWithTag(Tag.Monster);
        randomBox.SetActive(false);
        portal.SetActive(false);
        isInSphere = false;
        isMonsterAllDead = false;
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

        foreach (Collider col in hitColliders)
        {
            if (col.gameObject.tag != Tag.Monster)
            {
                isMonsterAllDead = true;
            }
            else
            {
                return;
            }
                
        }

        if (isMonsterAllDead)
        {
            randomBox.SetActive(true);

            if (isInSphere)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    portal.SetActive(true);
                }
            }

        }



    }

    }
