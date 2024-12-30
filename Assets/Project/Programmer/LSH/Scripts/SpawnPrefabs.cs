using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SpawnPrefabs : MonoBehaviour
{
    [SerializeField] GameObject prefabs; //������ ������
    [SerializeField] List<Transform> spawnPos; //���� ������
    [SerializeField] int spwanCount; //������ �߿��� ��� ������ų��
    Transform[] childCube; //prefabs�� Tag.Trash�϶��� �ڽĵ�
    [SerializeField] bool isAlreadySpawn;


    private void Start()
    {

        childCube = prefabs.GetComponentsInChildren<Transform>(true);
                
        SpawnPrefabPos();        
    

    }


    public void SpawnPrefabPos()
    {

        if (spawnPos.Count < spwanCount)
        {
            return;
        }
        else
        {                      

            while (spwanCount != 0)
            {
                
                int num = Random.Range(0, spawnPos.Count);
                Instantiate(prefabs, spawnPos[num]);
                
                if (prefabs.tag == Tag.Trash)
                {
                    SpawnTrashPos();
                }

                spawnPos.Remove(spawnPos[num]);
                spwanCount -= 1;

            }
        }


    }


    public void SpawnTrashPos()
    {

        for (int i = 0; i < childCube.Length; i++)
        {
            if (childCube[i] == prefabs.transform)
                childCube[i].gameObject.SetActive(true);
            else
                childCube[i].gameObject.SetActive(Random.value > 0.5f);

        }

        
    }

}




