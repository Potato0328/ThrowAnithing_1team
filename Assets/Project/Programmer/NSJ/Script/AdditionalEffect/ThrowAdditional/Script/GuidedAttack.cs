using System.Collections;
using UnityEngine;

/// <summary>
/// ���� ���
/// </summary>
[CreateAssetMenu(fileName = "GuidedAttack", menuName = "AdditionalEffect/Throw/GuidedAttack")]
public class GuidedAttack : ThrowAdditional
{
    [SerializeField] private float _guidedDistance = 5f;

    private bool _isDetect;

    Collider[] _targets = new Collider[1];
    Coroutine _guidedRoutien;
    public override void Enter()
    {
       
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {
        // �� ���� �ȵ�
        if(_isDetect == false)
        {
            // �� ����
            int hitCount = Physics.OverlapSphereNonAlloc(_throwObject.transform.position, _guidedDistance, _targets, 1 << Layer.Monster);
            if (hitCount > 0)
            {

                _isDetect = true;
            }
        }
        // �� �����ϸ� ������ ���ư�
        else
        {
            float guidedSpeed = _player.ThrowPower;

            Vector3 targetPos = new Vector3(_targets[0].transform.position.x, _targets[0].transform.position.y + 0.7f, _targets[0].transform.position.z);

            _throwObject.transform.LookAt(targetPos);

            _throwObject.Rb.velocity = _throwObject.transform.forward * guidedSpeed;
        }
    }
}
