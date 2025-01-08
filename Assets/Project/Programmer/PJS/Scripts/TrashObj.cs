using Assets.Project.Programmer.NSJ.RND.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashObj : MonoBehaviour, IHit
{
    [SerializeField] int atk;

    public int Atk { get { return atk; } }

    [SerializeField] EnemyBullet bulletPref;
    [SerializeField] Transform pos;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(bulletPref, pos);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == Tag.Monster)
        {
            Debug.Log(other.transform);
            IBattle hit = other.transform.GetComponentInParent<IBattle>();
            hit.TakeDamage(atk, CrowdControlType.Stiff, false);
        }

        //gameObject.SetActive(false);
    }

    public int TakeDamage(int damage, bool isIgnoreDef, CrowdControlType type)
    {
        //Debug.Log($"{gameObject} �� TakeDamage");
        return damage;
    }
}
