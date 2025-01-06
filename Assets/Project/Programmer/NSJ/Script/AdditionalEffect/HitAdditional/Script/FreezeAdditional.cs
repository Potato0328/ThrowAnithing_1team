using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "AdditionalEffect/Hit/Freeze")]
public class FreezeAdditional : HitAdditional
{
    private float _decreasedMoveSpeed;
    public override void Enter()
    {
        Debug.Log($"{gameObject.name} ����");

        if (_debuffRoutine == null)
        {
            _debuffRoutine = CoroutineHandler.StartRoutine(FreezeRoutine());
        }
        _decreasedMoveSpeed = Battle.Debuff.MoveSpeed;

    }

    public override void Update()
    {
        Debug.Log($"{transform.name} {Battle.Debuff.MoveSpeed}");
        Battle.Debuff.MoveSpeed = 0;
    }
    public override void Exit()
    {
        if (_debuffRoutine != null)
        {
            CoroutineHandler.StopRoutine(_debuffRoutine);
            _debuffRoutine = null;
        }
        Battle.Debuff.MoveSpeed += _decreasedMoveSpeed;
    }

    IEnumerator FreezeRoutine()
    {
        RemainDuraiton = Duration;
        while (RemainDuraiton > 0)
        {
            RemainDuraiton -= Time.deltaTime;
            yield return null;
        }

        Battle.EndDebuff(this);
    }
}
