using System.Collections;
using UnityEngine;

public class FallState : PlayerState
{

    private Vector3 _inertia; // ������

    Coroutine _jumpRoutine;
    public FallState(PlayerController controller) : base(controller)
    {
    }
    public override void Enter()
    {
        if (Player.PrevState != PlayerController.State.Jump)
        {
            View.SetTrigger(PlayerView.Parameter.Fall);
        }

        _inertia = Rb.velocity;
        if (_jumpRoutine == null)
        {
            _jumpRoutine = CoroutineHandler.StartRoutine(JumpRoutine());
        }
    }

    public override void Exit()
    {
        if (_jumpRoutine != null)
        {
            CoroutineHandler.StopRoutine(_jumpRoutine);
            _jumpRoutine = null;
        }
    }

    public override void OnDrawGizmos()
    {
        Vector3 CheckPos = new Vector3(transform.position.x, transform.position.y + 0.31f, transform.position.z);
        if (Physics.SphereCast(CheckPos, 0.3f, Vector3.down, out RaycastHit hit, 1f))
        {
            Gizmos.DrawLine(CheckPos, hit.point);
            Gizmos.DrawWireSphere(CheckPos + Vector3.down * hit.distance, 0.3f);
        }
    }

    IEnumerator JumpRoutine()
    {

        yield return 0.1f.GetDelay();
        while (Player.IsGround == false)
        {
            // ���� ����, ���� �ھƼ� �ӵ��� ������� ���� �����
            if(Rb.velocity.x > 0.01f && Rb.velocity.z > 0.01f)
            {
                Rb.velocity = new Vector3(_inertia.x, Rb.velocity.y, _inertia.z);
            }   
            // �������� ���������� ����� ����� ��������ٸ� ���� üũ ���� ����
            if (Rb.velocity.y < 0)
            {
                Vector3 CheckPos = new Vector3(transform.position.x, transform.position.y + 0.31f, transform.position.z);
                if (Physics.SphereCast(CheckPos, 0.3f, Vector3.down, out RaycastHit hit, 1f))
                    break;
            }
            yield return 0.02f.GetDelay();
        }

        // ���� �ִϸ��̼� ����
        View.SetTrigger(PlayerView.Parameter.Landing);

        while (true)
        {
            // ���� �ִϸ��̼��� �������� Idle ���� ��ȯ
            if (View.IsAnimationFinish == true)
            {
                ChangeState(PlayerController.State.Idle);
                break;
            }
            yield return null;
        }
    }
}
