using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    [Range(2f, 5f)] [SerializeField] float overlapSphereRadius; //�������� ��ó ����
    [Range(2f, 5f)] [SerializeField] float playerAreaRadius; //�÷��̾� ��ó ���������ɶ� ��ó ����
    Collider[] hitColliders; //�ش� ��ü�� ��� ��� �ݶ��̴��� �־�� �迭
    Collider playerCollider; //���߿��� �÷��̾� �ݶ��̴��� ������ ����

    [SerializeField] bool isInSphere; //T���������Ǿ������� F�ƴ�
    [SerializeField] GameObject pressKeyUI;

    [SerializeField] GameObject[] itemPrefabs;
    Vector3 itemRandomSpawnArea;

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
                playerCollider = hitColliders[i];
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
                SettingSpawnArea();
                DropRandomItems();
                SetFalseObject(); 
            }
        }

    }


    //�������� ������ ���� ����
    public void SettingSpawnArea()
    {
        float range_x = playerCollider.bounds.size.x * playerAreaRadius;
        float range_z = playerCollider.bounds.size.z * playerAreaRadius;
        range_x = Random.Range((range_x / 2) * -1, range_x / 2);
        range_z = Random.Range((range_z / 2) * -1, range_z / 2);
        itemRandomSpawnArea = new Vector3(range_x, 0.5f, range_z);
    }

    //�������� ������ ������ ���� �� ����
    public void DropRandomItems()
    {        
        int itemRandom = Random.Range(0, itemPrefabs.Length);
        Instantiate(itemPrefabs[itemRandom], itemRandomSpawnArea, Quaternion.identity);


    }

    //������ �������� �������� �����
    public void SetFalseObject()
    {
        gameObject.SetActive(false);
    }
}
