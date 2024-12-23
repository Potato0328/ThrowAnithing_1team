using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "_power Special", menuName = "Arm/AttackType/_power/Special")]
public class PowerSpecialAttack : ArmSpecialAttack
{
    [System.Serializable]
    struct ChargeStruct
    {
        public GameObject DropObject;
        public float ChargeTime;
        public int ObjectCount;
        public int Damage;
    }
    [SerializeField] private ChargeStruct[] _charges;

    private int _index;
    private float _maxChargeTime => _charges[_charges.Length - 1].ChargeTime;
    private int _triggerIndex;
    private GameObject _instanceDropObject;
    Coroutine _chargeRoutine;
    public override void Enter()
    {
        if(Model.ThrowObjectStack.Count < _charges[_index].ObjectCount)
        {
            EndAnimation();
            return;
        }
            

        // ��¡ ��� ����
        View.SetTrigger(PlayerView.Parameter.PowerSpecial);
        if (_chargeRoutine == null)
        {
            _chargeRoutine = CoroutineHandler.StartRoutine(ChargeRoutine());
        }
    }
    public override void Exit()
    {
        if (_chargeRoutine != null)
        {
            CoroutineHandler.StopRoutine(_chargeRoutine);
            _chargeRoutine = null;
        }
        Model.SpecialChargeGage = 0;
        _index = 0;
        _triggerIndex = 0;

    }
    public override void Update()
    {

    }
    public override void OnTrigger()
    {
        if (_triggerIndex == 0) 
        {
            CreateSpecialObject();
            _triggerIndex++;
        }
        else
        {
            AttackSpecial();
        }
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

            if (Input.GetButtonUp("Fire2"))
            {         
                Model.SpecialChargeGage = 0;
                if (_index != 0)
                {
                    _index--;
                    Model.CurSpecialGage -= (_charges[_index].ChargeTime / _maxChargeTime) * Model.MaxSpecialGage;
                    View.SetTrigger(PlayerView.Parameter.ChargeEnd);
                }
                else
                {
                    ChangeState(PlayerController.State.Idle);
                }
                _chargeRoutine = null;
                break;
            }
            yield return null;
        }
    }
    private void ProcessCharge()
    {
        // �����ð� ���
        Model.SpecialChargeGage += Time.deltaTime / _maxChargeTime;
        // �ε����� �迭 ũ�⺸�� ��������
        if (_index < _charges.Length)
        {
            // �Ҹ� ������Ʈ�� ������ ���
            if (Model.ThrowObjectStack.Count >= _charges[_index].ObjectCount)
            {
                // ���� �ð��� ���� �ܰ� ��¡ ���ǽð��� �ѱ� ���
                if (Model.SpecialChargeGage >= _charges[_index].ChargeTime / _maxChargeTime)
                {
                    _index++;
                }
                // ���� Ư���ڿ������� �������� �� ���� ���
                else if (Model.SpecialChargeGage > Model.CurSpecialGage / Model.MaxSpecialGage)
                {
                    Model.SpecialChargeGage = Model.CurSpecialGage / Model.MaxSpecialGage;
                }
            }
            else
            {
                Model.SpecialChargeGage = _index == 0 ? 0 : _charges[_index - 1].ChargeTime / _maxChargeTime;
            }
        }
    }

    private void CreateSpecialObject()
    {
        Vector3 dropPos = new Vector3(transform.localPosition.x, transform.localPosition.y+ 4f, transform.localPosition.z + 2f);
        _instanceDropObject = Instantiate(_charges[_index].DropObject, dropPos, transform.rotation);
    }

    private void AttackSpecial()
    {

    }
}
