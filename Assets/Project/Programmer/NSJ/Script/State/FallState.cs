using System.Collections;
using UnityEngine;

public class FallState : PlayerState
{

    private Vector3 _inertia; // ������

    private bool _isDoubleJump;
    private bool _isLanding;
    Coroutine _fallRoutine;
    Coroutine _checkInputRoutine;
    public FallState(PlayerController controller) : base(controller)
    {
    }
    public override void Enter()
    {
        if (Player.PrevState != PlayerController.State.Jump &&
            Player.PrevState != PlayerController.State.DoubleJump)
        {
            View.SetTrigger(PlayerView.Parameter.Fall);
        }

        _inertia = new Vector3(Rb.velocity.x, Rb.velocity.y, Rb.velocity.z);
        if (_fallRoutine == null)
        {
            _fallRoutine = CoroutineHandler.StartRoutine(FallRoutine());
        }
    }

    public override void Exit()
    {
        if (_fallRoutine != null)
        {
            CoroutineHandler.StopRoutine(_fallRoutine);
            _fallRoutine = null;
        }

        if (_checkInputRoutine != null) 
        {
            CoroutineHandler.StopRoutine(_checkInputRoutine);
            _checkInputRoutine = null;
        }
    }

    public override void FixedUpdate()
    {
        CheckIsNearGround();
    }
    public override void OnDrawGizmos()
    {
        Vector3 CheckPos = new Vector3(transform.position.x, transform.position.y + 0.31f, transform.position.z);
        if (Physics.SphereCast(CheckPos, 0.3f, Vector3.down, out RaycastHit hit, 1))
        {
            Gizmos.DrawLine(CheckPos, hit.point);
            Gizmos.DrawWireSphere(CheckPos + Vector3.down * hit.distance, 0.3f);
        }
    }
    public override void EndAnimation()
    {
        if(_isLanding == true)
        {
            _isDoubleJump = false;
            _isLanding = false;
            ChangeState(PlayerController.State.Idle);
        }
        else
        {
            View.SetTrigger(PlayerView.Parameter.Fall);
        }
    }

    IEnumerator FallRoutine()
    {
        // Fall ���¿��� ���� �� �ִ� �Է� ���
        if (_checkInputRoutine == null)
            _checkInputRoutine = CoroutineHandler.StartRoutine(CheckInputRoutine());

        yield return 0.1f.GetDelay();
        while (Player.IsGround == false)
        {
            // ���� ����, ���� ���˽� �̵�����
            if (Player.IsWall == false)
            {
                Rb.velocity = new Vector3(_inertia.x, Rb.velocity.y, _inertia.z);
            }
            else
            {
                Rb.velocity = new Vector3(0, Rb.velocity.y, 0);
            }
            // �������� ���������� ����� ����� ��������ٸ� ���� üũ ���� ����
            if (Rb.velocity.y < 0)
            {
                Vector3 CheckPos = new Vector3(transform.position.x, transform.position.y + 0.31f, transform.position.z);
                if (CheckIsNearGround() && Rb.velocity.y < 0)
                    break;
            }
            yield return 0.02f.GetDelay();
        }

        if (_checkInputRoutine != null)
        {
             CoroutineHandler.StopRoutine(_checkInputRoutine);
            _checkInputRoutine = null;
        }
        // ���� �ִϸ��̼� ����
        _isLanding = true;
        View.SetTrigger(PlayerView.Parameter.Landing);
    }

    IEnumerator CheckInputRoutine()
    {
        while (true)
        {
            if (Input.GetButtonDown("Jump") && _isDoubleJump == false)
            {
                _isDoubleJump = true;
                ChangeState(PlayerController.State.DoubleJump);
                break;
            }
            if (Input.GetKeyDown(KeyCode.V) && _isDoubleJump == true)
            {
                _isDoubleJump = false;
                ChangeState(PlayerController.State.JumpDown);
                break;
            }
            yield return null;
        }
        _checkInputRoutine = null;
    }

    /// <summary>
    /// ���鿡 ������� üũ
    /// </summary>
    private bool CheckIsNearGround()
    {
        Vector3 CheckPos = new Vector3(transform.position.x, transform.position.y + 0.31f, transform.position.z);
        if (Physics.SphereCast(CheckPos, 0.3f, Vector3.down, out RaycastHit hit, 1f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
