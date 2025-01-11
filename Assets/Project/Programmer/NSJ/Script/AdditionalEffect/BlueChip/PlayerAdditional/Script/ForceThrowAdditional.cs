using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ForceThrow", menuName = "AdditionalEffect/Player/ForceThrow")]
public class ForceThrowAdditional : PlayerAdditional
{
    [SerializeField] DoubleDamageBuff _damageBuff;
    [Header("������ �߰� ���(%)")]
    [SerializeField] float _damageMultiplier;
    [Header("���� ī��Ʈ")]
    [SerializeField] float _attackCount;

    private int _curCount;
    private bool _onBuff;
    public override void Enter()
    {
        _curCount = 1;
        // ���ο� ���������� �ν��Ͻ��� ����
        _damageBuff = Instantiate(_damageBuff);
        _damageBuff.DamageMultyplier = _damageMultiplier;


        Player.OnThrowObjectResult += CheckCount;
    }
    public override void Exit()
    {
        Player.OnThrowObjectResult -= CheckCount;
    }

    public override void Trigger()
    {
        if (CurState != PlayerController.State.ThrowAttack)
            return;

        // ���� ���� �� ������ ���� ����
        if (_onBuff == true)
        {       
            OffBuff();
        }
    }
    private void CheckCount(ThrowObject throwObject, bool hitSuccess)
    {
        // �¾��� ��
        if(hitSuccess == true)
        {
            // ü�ε� ���� ���ʷ� ����ٸ�
            if(throwObject.IsChainHit == false)
            {
                // ī��Ʈ ���
                if (_curCount < _attackCount)
                {
                    _curCount++;
                }
                // ����ī��Ʈ�� �ִ뿡 �����߰� ������ ���� ��
                if (_curCount == _attackCount&& _onBuff == false)
                {
                    OnBuff();
                }
            }
        }
    }

    private void OnBuff()
    {
        Player.AddAdditional(_damageBuff);
        _onBuff = true;
    }
    private void OffBuff()
    {
        // 1������ �ڿ� ���� ����
        CoroutineHandler.StartRoutine(OffBuffRoutine());
        _curCount = 0;
        _onBuff = false;
    }
    IEnumerator OffBuffRoutine()
    {
        yield return null;
        Player.RemoveAdditional(_damageBuff);  
    }
}
