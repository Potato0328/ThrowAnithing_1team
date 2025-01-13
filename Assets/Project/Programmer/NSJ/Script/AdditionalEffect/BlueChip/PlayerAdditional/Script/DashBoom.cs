using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashBoom", menuName = "AdditionalEffect/Player/DashBoom")]
public class DashBoom : PlayerAdditional
{
    [System.Serializable]
    struct EffectStrcut
    {
        public GameObject EffectPrefab;
        [HideInInspector] public GameObject Effect;
        public float EffectDuration;
    }
    [SerializeField] EffectStrcut _effect;

    [Header("���� ����")]
    [SerializeField] float _range;
    [Header("���� �ð�")]
    [SerializeField] float _stunTime;
    public override void Trigger()
    {
        if(Player.CurState == PlayerController.State.Dash)
        {
            Attack();
        }
    }

    private void Attack()
    {
        CoroutineHandler.StartRoutine(CreateAttackEffectRoutien());

        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _range, Player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++)
        {
            // TODO : ���� ���� ��� 
            Debug.Log($"{Player.OverLapColliders[i].name} ����");
        }
    }

    IEnumerator CreateAttackEffectRoutien()
    {
        if (_effect.EffectPrefab == null)
            yield break;

        GameObject instance = Instantiate(_effect.EffectPrefab, transform.position, transform.rotation);
        while (true)
        {
            // ����Ʈ ���� Ŀ��
            instance.transform.localScale = new Vector3(
              instance.transform.localScale.x + _range * 2 * Time.deltaTime * (1 / _effect.EffectDuration),
              instance.transform.localScale.y + _range * 2 * Time.deltaTime * (1 / _effect.EffectDuration),
              instance.transform.localScale.z + _range * 2 * Time.deltaTime * (1 / _effect.EffectDuration));
            if (instance.transform.localScale.x > _range * 2)
            {
                break;
            }
            yield return null;
        }

        Destroy(instance);
    }

    //public override void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, _range);
    //}
}
