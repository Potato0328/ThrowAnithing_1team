using System.Collections;
using UnityEngine;

public class FallState : PlayerState
{

    private Vector3 _inertia; // ������
    private bool _isInertia;
    Coroutine _fallRoutine;
    Coroutine _checkInputRoutine;
    Coroutine _checkLandingRoutine;
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

        _inertia = new Vector3(Rb.velocity.x, 0,Rb.velocity.z);
        StartCoroutine();
    }

    public override void Exit()
    {
        StopCoroutine();
    }
    public override void Update()
    {
        
    }

    IEnumerator FallRoutine()
    {
        yield return 0.1f.GetDelay();
        while (Player.IsGround == false)
        {
            if (MoveDir == Vector3.zero)
            {
                Rb.velocity = new Vector3(_inertia.x, Rb.velocity.y, _inertia.z);
            }
            else
            {
                Player.LookAtMoveDir();

                Vector3 moveDir = transform.forward * Model.MoveSpeed;
                //Vector3 moveDir = transform.forward * MoveDir.z * Model.MoveSpeed + transform.right * MoveDir.x * Model.MoveSpeed;
                Rb.velocity = new Vector3(moveDir.x, Rb.velocity.y, moveDir.z);
                _inertia = Rb.velocity;
            }

            // ���� ����, ���� ���˽� �̵�����
            if (Player.IsWall == true)
            {
                Rb.velocity = new Vector3(0, Rb.velocity.y, 0);
            }
            // �������� ���������� ����� ����� ��������ٸ� ���� üũ ���� ����
            if (Rb.velocity.y <= 0)
            {
                Vector3 CheckPos = new Vector3(transform.position.x, transform.position.y + 0.31f, transform.position.z);
                if (Player.IsNearGround == true && Rb.velocity.y <=  0)
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
        View.SetTrigger(PlayerView.Parameter.Landing);
    }

    IEnumerator CheckInputRoutine()
    {
        while (true)
        {
            if (InputKey.GetButtonDown(InputKey.PrevJump) && Player.IsDoubleJump == false)
            {
                Player.IsDoubleJump = true;
                ChangeState(PlayerController.State.DoubleJump);
                break;
            }
            if (InputKey.GetButtonDown(InputKey.PrevThrow) && Player.IsJumpAttack == false)
            {
                Player.IsJumpAttack = true;
                ChangeState(PlayerController.State.JumpAttack);
                break;
            }
            if (InputKey.GetButtonDown(InputKey.PrevMelee) && Player.IsDoubleJump == true)
            {
                Player.IsDoubleJump = false;
                ChangeState(PlayerController.State.JumpDown);
                break;
            }

         


            yield return null;
        }
        _checkInputRoutine = null;
    }
    IEnumerator CheckLandingRoutine()
    {
        while (true)
        {
            if (Player.IsGround == true && Rb.velocity.y < 0)
            {
                Player.IsDoubleJump = false;
                Player.IsJumpAttack = false;
                ChangeState(PlayerController.State.Idle);
                yield break;
            }
            yield return null;
        }
    }

    private void StartCoroutine()
    {
        if (_fallRoutine == null)
        {
            _fallRoutine = CoroutineHandler.StartRoutine(FallRoutine());
        }
        // Fall ���¿��� ���� �� �ִ� �Է� ���
        if (_checkInputRoutine == null)
            _checkInputRoutine = CoroutineHandler.StartRoutine(CheckInputRoutine());
        if (_checkLandingRoutine == null)
            _checkLandingRoutine = CoroutineHandler.StartRoutine(CheckLandingRoutine());
    }

    private void StopCoroutine()
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
        if (_checkLandingRoutine != null)
        {
            CoroutineHandler.StopRoutine(_checkLandingRoutine);
            _checkLandingRoutine = null;
        }
    }
}
