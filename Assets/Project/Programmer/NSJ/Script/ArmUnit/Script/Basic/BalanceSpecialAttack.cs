using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

[CreateAssetMenu(fileName = "Balance PrevSpecial", menuName = "Arm/AttackType/Balance/PrevSpecial")]
public class BalanceSpecialAttack : ArmSpecialAttack
{
    [System.Serializable]
    struct ChargeStruct
    {
        public float ChargeTime;
        public float ChargeMana;
        public int ObjectCount;
    }
    [System.Serializable]
    struct FirstStruct
    {
        public float AttackSpeed;
        public float Duration;
    }
    [System.Serializable]
    struct SecondStruct
    {
        public SpecialObject SpecialObject;
        public float Damage;
        public float MiddleDamage;
        public float Range;
        public float MiddleRange;
    }
    [System.Serializable]
    struct ThirdStruct
    {
        public GameObject Effect;
        public GameObject BeforeEffect;
        public ElectricShockAdditonal ElectricShock;
        public float AttackDelay;
        public Vector3 AttackOffset;
        public float Damage;
        public float MiddleDamage;
        public float Range;
        public float MiddleRange;
        public float ElectricShockDuration;
    }

    [SerializeField] private ChargeStruct[] _charges;
    [SerializeField] private FirstStruct _first;
    [SerializeField] private SecondStruct _second;
    [SerializeField] private ThirdStruct _third;

    private List<Transform> MiddleHittargets = new List<Transform>();
    private float _maxChargeTime => _charges[_charges.Length - 1].ChargeTime;
    private float _maxChargeMana => _charges[_charges.Length - 1].ChargeMana;
    public override void Init(PlayerController player)
    {
        base.Init(player);
        View.Panel.SetChargingMpVarMaxValue(Model.MaxMana);
        for (int i = 0; i < _charges.Length; i++)
        {
            View.Panel.StepTexts[i].SetText(_charges[i].ObjectCount.GetText());
            View.Panel.SetChargingMpHandle(i, _charges[i].ChargeMana);
        }

        // 감전 효과 클론으로 제작(수치 변경하기 위함)
        _third.ElectricShock = Instantiate(_third.ElectricShock);
        _third.ElectricShock.Duration = _third.ElectricShockDuration;
    }
    public override void Enter()
    {
        if (_isSpecialCharge == false
            && (Model.ThrowObjectStack.Count < _charges[_index].ObjectCount || Model.CurMana < Model.ManaConsumption[0]))
        {
            _isSpecialCharge = false;
            ChangeState(Player.PrevState);
            return;
        }

        if (_isSpecialCharge == false)
        {
            ChangeState(Player.PrevState);
            _isSpecialCharge = true;
            CoroutineHandler.StartRoutine(ChargeRoutine());
        }
        else
        {
            _index--;
            if (_index < 0)
            {
                Model.SpecialChargeGage = 0;
                _isSpecialCharge = false;
                ChangeState(Player.PrevState);
                return;
            }
            SelectSpecialAttack(_index);
        }
    }
    public override void Exit()
    {
        if (_isSpecialCharge == false)
        {

        }
        else
        {

            _isSpecialCharge = false;
        }
        _index = 0;
    }
    public override void OnTrigger()
    {
        SelectTrigger(_index);
    }
    public override void EndAnimation()
    {
        ChangeState(PlayerController.State.Idle);
    }
    IEnumerator ChargeRoutine()
    {
        while (true)
        {
            ProcessCharge();
            if (InputKey.GetButtonUp(InputKey.Special))
            {
                ChangeState(PlayerController.State.SpecialAttack);
                yield break;
            }
            yield return null;
        }
    }

    private void ProcessCharge()
    {
        // 차지시간 계산
        Model.SpecialChargeGage += Time.deltaTime * 100 / _maxChargeTime;
        // 인덱스가 배열 크기보다 작을떄만
        if (_index < _charges.Length)
        {
            // 소모 오브젝트가 부족한 경우
            if (Model.ThrowObjectStack.Count >= _charges[_index].ObjectCount)
            {
                // 차지 시간이 다음 단계 차징 조건시간을 넘긴 경우
                if (Model.SpecialChargeGage >= _charges[_index].ChargeMana)
                {

                    _index++;
                }
                // 현재 특수자원량보다 차지량이 더 많은 경우
                else if (Model.SpecialChargeGage > Model.CurMana)
                {
                    Model.SpecialChargeGage = Model.CurMana;
                }
            }
            else
            {
                Model.SpecialChargeGage = _charges[_index - 1].ChargeMana;
            }
        }
        else
        {
            Model.SpecialChargeGage = _charges[_index - 1].ChargeMana;
        }
    }

    private void SelectSpecialAttack(int index)
    {
        Rb.velocity = Vector3.zero;
        // 차지 사용량만큼 제거
        Model.CurMana -= _charges[_index].ChargeMana;
        Model.SpecialChargeGage = 0;
        // 사용한 오브젝트만큼 제거
        for (int i = 0; i < _charges[_index].ObjectCount; i++)
        {
            Model.PopThrowObject();
        }
        switch (index)
        {
            case 0:
                ProcessFirstSpecial();
                break;
            case 1:
                ProcessSecondSpecial();
                break;
            case 2:
                ProcessThirdSpecial();
                break;
        }
    }

    private void SelectTrigger(int index)
    {
        switch (index)
        {
            case 0:
                break;
            case 1:
                ThrowSpecialObject();
                break;
            case 2:
                Bombard();
                break;
        }
    }


    /// <summary>
    /// 첫번째 특수
    /// </summary>
    private void ProcessFirstSpecial()
    {
        Debug.Log("첫번쨰 특수기");
        CoroutineHandler.StartRoutine(FirstSpecialBuffRoutine());
        ChangeState(Player.PrevState);
    }
    IEnumerator FirstSpecialBuffRoutine()
    {
        Model.AttackSpeedMultiplier += _first.AttackSpeed;
        yield return _first.Duration.GetDelay();
        Model.AttackSpeedMultiplier -= _first.AttackSpeed;
    }
    /// <summary>
    /// 두번째 특수
    /// </summary>
    private void ProcessSecondSpecial()
    {
        Debug.Log("두번쨰 특수기");
        View.SetTrigger(PlayerView.Parameter.BalanceSpecial2);
    }
    /// <summary>
    /// 특수 투척물 던지기
    /// </summary>
    private void ThrowSpecialObject()
    {
        SpecialObject specialObject = ObjectPool.GetPool(_second.SpecialObject, _muzzlePoint.position, _muzzlePoint.rotation);
        specialObject.Init(Player, CrowdControlType.None, Model.ThrowAdditionals);
        specialObject.InitSpecial(_second.Damage, _second.MiddleDamage, _second.Range, _second.MiddleRange);
        specialObject.Shoot(Player.ThrowPower);
    }

    /// <summary>
    /// 세번째 특수
    /// </summary>
    private void ProcessThirdSpecial()
    {
        Debug.Log("세번쨰 특수기");
        Player.LookAtAttackDir();
        View.SetTrigger(PlayerView.Parameter.BalanceSpecial3);
    }
    /// <summary>
    /// 포격하기
    /// </summary>
    private void Bombard()
    {
        Player.LookAtAttackDir();
        CoroutineHandler.StartRoutine(BombardRoutine());
    }

    IEnumerator BombardRoutine()
    {
        Vector3 attackPos = new Vector3(
                transform.position.x + (Player.transform.forward.x * _third.AttackOffset.x),
                transform.position.y + 0.01f,
                transform.position.z + (Player.transform.forward.z * _third.AttackOffset.z));
         
        GameObject beforeEffect = ObjectPool.GetPool(_third.BeforeEffect, attackPos, Quaternion.identity);
        yield return _third.AttackDelay.GetDelay();
        ObjectPool.ReturnPool(beforeEffect);
        //TODO 
        AttackBombard(attackPos);

        GameObject effect = ObjectPool.GetPool(_third.Effect, attackPos, _third.Effect.transform.rotation);
        yield return 2f.GetDelay();
        ObjectPool.ReturnPool(effect);
    }

    private void AttackBombard(Vector3 attackPos)
    {
        // 중앙 맞은 몬스터 걸러내기
        int hitCount = Physics.OverlapSphereNonAlloc(attackPos, _third.MiddleRange, Player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++)
        {
            MiddleHittargets.Add(Player.OverLapColliders[i].transform);
        }


        hitCount = Physics.OverlapSphereNonAlloc(attackPos, _third.Range, Player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++)
        {
            int finalDamage = 0;
            bool isCritical = false;
            // 중앙에 맞았을 경우
            if (MiddleHittargets.Contains(Player.OverLapColliders[i].transform))
            {
                finalDamage = Player.GetFinalDamage((int)_third.MiddleDamage, out isCritical);
            }
            else
            {
                finalDamage = Player.GetFinalDamage((int)_third.Damage, out isCritical);
            }

            Battle.TargetAttack(Player.OverLapColliders[i], isCritical, finalDamage);
            Battle.TargetDebuff(Player.OverLapColliders[i], _third.ElectricShock);
        }
        MiddleHittargets.Clear();
    }
}
