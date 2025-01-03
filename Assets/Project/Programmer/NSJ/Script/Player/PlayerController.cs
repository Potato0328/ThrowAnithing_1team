
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PlayerModel))]
[RequireComponent(typeof(PlayerView))]
[RequireComponent(typeof(BattleSystem))]
public class PlayerController : MonoBehaviour, IHit
{
    [SerializeField] public Transform ArmPoint;
    [Inject]
    public OptionSetting setting;

    [HideInInspector] public PlayerModel Model;
    [HideInInspector] public PlayerView View;
    [HideInInspector] public Rigidbody Rb;
    [HideInInspector] public BattleSystem Battle;
    public enum State
    {
        Idle,
        Run,
        MeleeAttack,
        ThrowAttack,
        Jump,
        DoubleJump,
        Fall,
        DoubleJumpFall,
        JumpAttack,
        JumpDown,
        Dash,
        Drain,
        SpecialAttack,
        Hit,
        Dead,
        Size
    }

    private PlayerState[] _states = new PlayerState[(int)State.Size];
    public State CurState;
    public State PrevState;

    #region �̺�Ʈ
    public event UnityAction<int, bool> OnPlayerHitEvent;
    public event UnityAction OnPlayerDieEvent;
    #endregion
    #region ���� ���� �ʵ�
    [System.Serializable]
    struct AttackStruct
    {
        public float AttackHeight;
        public float ThrowPower;
        public Transform MuzzlePoint;
        [HideInInspector] public bool IsTargetHolding;
        [HideInInspector] public bool IsTargetToggle;
        [HideInInspector] public Vector3 TargetPos;
    }
    [Header("���� ���� �ʵ�")]
    [SerializeField] private AttackStruct _attackStruct;
    public Transform MuzzletPoint { get { return _attackStruct.MuzzlePoint; } set { _attackStruct.MuzzlePoint = value; } }
    public float AttackHeight { get { return _attackStruct.AttackHeight; } set { _attackStruct.AttackHeight = value; } }
    public float ThrowPower { get { return _attackStruct.ThrowPower; } set { _attackStruct.ThrowPower = value; } }
    public bool IsTargetHolding { get { return _attackStruct.IsTargetHolding; } set { _attackStruct.IsTargetHolding = value; } }
    public bool IsTargetToggle { get { return _attackStruct.IsTargetToggle; } set { _attackStruct.IsTargetToggle = value; } }
    public Vector3 TargetPos { get { return _attackStruct.TargetPos; } set { _attackStruct.TargetPos = value; } }
    #endregion
    #region Camera ���� �ʵ�
    /// <summary>
    /// ī�޶� ����
    /// </summary>
    [System.Serializable]
    struct CameraStruct
    {
        public Transform CamaraArm;
        public Transform CameraPos;
        [Range(0f, 50f)] public float CameraRotateAngle;
        [Range(0f, 5f)] public float CameraRotateSpeed;
        public PlayerCameraHold CameraHolder;
        public bool IsVerticalCameraMove;
    }
    [Header("ī�޶� ���� �ʵ�")]
    [SerializeField] private CameraStruct _cameraStruct;
    public Transform CamareArm { get { return _cameraStruct.CamaraArm; } set { _cameraStruct.CamaraArm = value; } }
    private Transform _cameraPos { get { return _cameraStruct.CameraPos; } set { _cameraStruct.CameraPos = value; } }
    private float _cameraRotateAngle { get { return _cameraStruct.CameraRotateAngle; } set { _cameraStruct.CameraRotateAngle = value; } }
    private float _cameraRotateSpeed { get { return _cameraStruct.CameraRotateSpeed; } set { _cameraStruct.CameraRotateSpeed = value; } }
    private PlayerCameraHold _cameraHolder { get { return _cameraStruct.CameraHolder; } set { _cameraStruct.CameraHolder = value; } }
    public bool IsVerticalCameraMove { get { return _cameraStruct.IsVerticalCameraMove; } set { _cameraStruct.IsVerticalCameraMove = value; } }
    #endregion
    #region ���� ���� �ʵ�
    [System.Serializable]
    struct CheckStruct
    {
        public Transform GroundCheckPos;
        [Range(0, 1)] public float SlopeAngle;
        public WallCheckStruct WallCheckPos;
        public float WallCheckDistance;
        [Space(10)]
        public bool IsGround; // ���� ���� ����
        public bool IsNearGround;
        public bool IsWall; // �� ���� ����
        public bool CanClimbSlope; // ���� �� �ִ� ���� ���� ���� üũ
    }
    [System.Serializable]
    public struct WallCheckStruct
    {
        public Transform Head;
        public Transform Foot;
    }
    [SerializeField] private CheckStruct _checkStruct;
    private Transform _groundCheckPos => _checkStruct.GroundCheckPos;
    public WallCheckStruct WallCheckPos => _checkStruct.WallCheckPos;

    private float _wallCheckDistance { get { return _checkStruct.WallCheckDistance; } set { _checkStruct.WallCheckDistance = value; } }
    private float _slopeAngle { get { return _checkStruct.SlopeAngle; } set { _checkStruct.SlopeAngle = value; } }

    #endregion
    #region �׽�Ʈ ���� �ʵ�
    [System.Serializable]
    public struct TestStruct
    {
        public bool IsAttackForward;
    }
    [Header("�׽�Ʈ ���� �ʵ�")]
    [SerializeField] private TestStruct _testStruct;
    public bool IsAttackFoward { get { return _testStruct.IsAttackForward; } }
    #endregion
    #region ����üũ Bool �ʵ�
    [System.Serializable]
    public struct BoolField
    {
        public bool IsDoubleJump; // �������� ����?
        public bool IsJumpAttack; // �������� ����?
        public bool IsInvincible; // ����������?
        public bool IsHit; // ����?
        public bool IsDead; // ����?
        public bool IsStaminaCool; // ���׹̳� ��� �� ��Ÿ������?
        public bool CanStaminaRecovery; // ���׹̳� ȸ�� �� �� �ִ���?
        public bool CantOperate; // ������ �� �ִ���?
    }
    [SerializeField]private BoolField _boolField;
    public bool IsDoubleJump { get { return _boolField.IsDoubleJump; } set { _boolField.IsDoubleJump = value; } }
    public bool IsJumpAttack { get { return _boolField.IsJumpAttack; } set { _boolField.IsJumpAttack = value; } }
    public bool IsInvincible { get { return _boolField.IsInvincible; } set { _boolField.IsInvincible = value; } }
    public bool IsHit { get { return _boolField.IsHit; } set { _boolField.IsHit = value; } }
    public bool IsDead { get { return _boolField.IsDead; } set { _boolField.IsDead = value; } }
    public bool IsStaminaCool { get { return _boolField.IsStaminaCool; } set { _boolField.IsStaminaCool = value; } }
    public bool CanStaminaRecovery { get { return _boolField.CanStaminaRecovery; } set { _boolField.CanStaminaRecovery = value; } }
    public bool CantOperate { get { return _boolField.CantOperate; } set { _boolField.CantOperate = value; TriggerCantOperate(); } }
    #endregion

    //TODO: �ν����� ���� �ʿ�
    public GameObject DrainField;

    public bool IsGround { get { return _checkStruct.IsGround; } set { _checkStruct.IsGround = value; } }// ���� ���� ����
    public bool IsNearGround { get { return _checkStruct.IsNearGround; } set { _checkStruct.IsNearGround = value; } }
    public bool IsWall { get { return _checkStruct.IsWall; } set { _checkStruct.IsWall = value; } } // �� ���� ����
    public bool CanClimbSlope { get { return _checkStruct.CanClimbSlope; } set { _checkStruct.CanClimbSlope = value; } } // ���� �� �ִ� ���� ���� ���� üũ

    [HideInInspector] public Collider[] OverLapColliders = new Collider[100];

    [HideInInspector] public Vector3 MoveDir;

    Quaternion _defaultMuzzlePointRot;
    private void Awake()
    {

    }

    private void Start()
    {
        Init();
        InitUIEvent();
        StartRoutine();
        InitAdditionnal();
        ChangeArmUnit(Model.NowWeapon);
        StartCoroutine(ControlMousePointer());
        //Camera.main.transform.SetParent(_cameraPos, true);
        _states[(int)CurState].Enter();
    }
    public bool IsMouseVisible;

    private void OnDisable()
    {
        ExitPlayerAdditional();
        _states[(int)CurState].Exit();
    }

    private void Update()
    {
        if (CantOperate == true)
            return;

        if (Time.timeScale == 0)
            return;

        _states[(int)CurState].Update();

        CheckAnyState();
        RotateCamera();
        ChackInput();
        UpdatePlayerAdditional();
    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 0)
            return;

        _states[(int)CurState].FixedUpdate();
        CheckGround();
        CheckWall();
        CheckIsNearGround();
        FixedPlayerAdditional();
    }

    private void OnDrawGizmos()
    {
        if (_states[(int)CurState] == null)
            return;
        _states[(int)CurState].OnDrawGizmos();

        DrawCheckGround();
        DrawWallCheck();
        DrawIsNearGround();
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void ChangeState(State state)
    {
        // ���׹̳� ���� ����
        if (_states[(int)state].UseStamina == true)
        {
            // ����Ҽ� ����?(�ּ� ���׹̳�)
            if (Model.CurStamina < _states[(int)state].StaminaAmount)
                return;
            // ��밡���ϸ� ���׹̳� ����
            Model.CurStamina -= _states[(int)state].StaminaAmount;
        }

        _states[(int)CurState].Exit();
        PrevState = CurState;
        CurState = state;
        _states[(int)CurState].Enter();

        //Debug.Log(CurState);
    }

    /// <summary>
    /// ������ �ޱ�
    /// </summary>
    public void TakeDamage(int damage, bool isStun)
    {
        OnPlayerHitEvent?.Invoke(damage, isStun);
    }

    /// <summary>
    /// ���
    /// </summary>
    public void Die()
    {
        OnPlayerDieEvent?.Invoke();
    }

    #region Instantiate �븮 �޼���
    public T InstantiateObject<T>(T instance) where T : Component
    {
        T instanceObject = Instantiate(instance);
        return instanceObject;
    }
    public T InstantiateObject<T>(T instance, Transform parent) where T : Component
    {
        T instanceObject = Instantiate(instance, parent);
        return instanceObject;
    }
    public T InstantiateObject<T>(T instance, Vector3 pos, Quaternion rot) where T : Component
    {
        T instanceObject = Instantiate(instance, pos, rot);
        return instanceObject;
    }
    #endregion
    #region �÷��̾� ���� ó��
    public void LookAtAttackDir()
    {
        if (IsTargetHolding == false && IsTargetToggle == false)
            LookAtCameraFoward();
        else
            LookAtTargetDir(TargetPos);
    }

    /// <summary>
    /// ī�޶� �������� �÷��̾� ���� ��ȯ
    /// </summary>
    public void LookAtCameraFoward()
    {
        // ī�޶� �������� �÷��̾ �ٶ󺸰�
        Quaternion cameraRot = Quaternion.Euler(0, CamareArm.eulerAngles.y, 0);
        transform.rotation = cameraRot;
        // ī�޶�� �ٽ� ���� ���� ���� ����
        if (CamareArm.parent != null)
        {
            CamareArm.localRotation = Quaternion.Euler(CamareArm.localRotation.eulerAngles.x, 0, 0);
        }
        MuzzletPoint.localRotation = _defaultMuzzlePointRot;
    }

    /// <summary>
    /// �Է��� ������ �÷��̾ �ٶ�
    /// </summary>
    /// <param name="moveDir"></param>
    public void LookAtMoveDir()
    {
        // ī�޶� �������� �÷��̾ �ٶ󺸰�
        Quaternion cameraRot = Quaternion.Euler(0, CamareArm.eulerAngles.y, 0);
        transform.rotation = cameraRot;
        // ī�޶�� �ٽ� ���� ���� ���� ����
        if (CamareArm.parent != null)
        {
            // ī�޶� ��鸲 ���� ����ִ� �ڵ�
            CamareArm.localPosition = new Vector3(0, CamareArm.localPosition.y, 0);
            CamareArm.localRotation = Quaternion.Euler(CamareArm.localRotation.eulerAngles.x, 0, 0);
        }

        Quaternion cameraTempRot = CamareArm.rotation;

        // �Է��� �������� �÷��̾ �ٶ�
        Vector3 dir = transform.forward * MoveDir.z + transform.right * MoveDir.x;
        if (dir == Vector3.zero)
        {
            if (CurState == State.Run)
                return;
            else
            {
                dir = transform.forward;
            }
        }
        transform.rotation = Quaternion.LookRotation(dir);
        CamareArm.rotation = cameraTempRot;
    }

    /// <summary>
    /// Ÿ�� ������ �÷��̾ �ٶ�
    /// </summary>
    public void LookAtTargetDir(Vector3 targetPos)
    {
        // ī�޶� �������� �÷��̾ �ٶ󺸰�
        Quaternion cameraRot = Quaternion.Euler(0, CamareArm.eulerAngles.y, 0);
        transform.rotation = cameraRot;
        // ī�޶�� �ٽ� ���� ���� ���� ����
        if (CamareArm.parent != null)
        {
            // ī�޶� ��鸲 ���� ����ִ� �ڵ�
            CamareArm.localPosition = new Vector3(0, CamareArm.localPosition.y, 0);
            CamareArm.localRotation = Quaternion.Euler(CamareArm.localRotation.eulerAngles.x, 0, 0);
        }

        Quaternion cameraTempRot = CamareArm.rotation;
        targetPos = new Vector3(targetPos.x, targetPos.y + 2f, targetPos.z);
        // �Է��� �������� �÷��̾ �ٶ�
        transform.LookAt(targetPos);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        CamareArm.rotation = cameraTempRot;
        MuzzletPoint.LookAt(targetPos);
    }

    /// <summary>
    ///  �÷��̾ �����ִ� �������� ������ �ٲٱ�
    /// </summary>
    public void ChangeVelocityPlayerFoward()
    {
        Vector3 tempVelocity = transform.forward * Rb.velocity.magnitude; // x,z ���� �������� ���
        Rb.velocity = tempVelocity; // ����
    }
    #endregion
    /// <summary>
    /// ������Ʈ �ݱ�
    /// </summary>
    public void AddThrowObject(ThrowObject throwObject)
    {

        if (Model.CurThrowables < Model.MaxThrowables)
        {
            Model.PushThrowObject(DataContainer.GetThrowObject(throwObject.Data.ID).Data);
            Destroy(throwObject.gameObject);
        }
    }

    public void ChangeArmUnit(ArmUnit armUnit)
    {
        Model.Arm = Instantiate(armUnit);
        Model.Arm.Init(this);
    }
    public void ChangeArmUnit(GlobalGameData.AmWeapon armUnit)
    {
        Model.Arm = Instantiate(DataContainer.GetArmUnit(armUnit));
        Model.Arm.Init(this);
    }
    #region �÷��̾� �߰�ȿ�� ����
    /// <summary>
    /// �߰�ȿ�� �߰�
    /// </summary>
    public void AddAdditional(AdditionalEffect addtionalEffect)
    {
        switch (addtionalEffect.AdditionalType)
        {
            case AdditionalEffect.Type.Hit:
                if (CheckForAddAdditionalDuplication(Model.HitAdditionals, addtionalEffect as HitAdditional))
                {
                    Model.HitAdditionals.Add(addtionalEffect as HitAdditional);
                    Model.AdditionalEffects.Add(addtionalEffect);
                    // ����ȿ���� ��Ʋ�ý��ۿ��� �߰� ���
                    Battle.AddHitAdditionalList(addtionalEffect as HitAdditional);
                }
                break;
            case AdditionalEffect.Type.Throw:
                if (CheckForAddAdditionalDuplication(Model.ThrowAdditionals, addtionalEffect as ThrowAdditional))
                {
                    Model.ThrowAdditionals.Add(addtionalEffect as ThrowAdditional);
                    Model.AdditionalEffects.Add(addtionalEffect);
                }
                break;
            // �÷��̾� �߰�ȿ���� �÷��̾ ���ӵǱ� ������ Clone�� ������
            case AdditionalEffect.Type.Player:
                if (CheckForAddAdditionalDuplication(Model.PlayerAdditionals, addtionalEffect as PlayerAdditional))
                {
                    PlayerAdditional instance = Instantiate(addtionalEffect as PlayerAdditional);
                    Model.PlayerAdditionals.Add(instance);
                    Model.AdditionalEffects.Add(instance);
                    instance.Init(this, addtionalEffect);
                    instance.Enter();
                }
                break;
        }
    }
    /// <summary>
    /// �߰�ȿ�� ����
    /// </summary>
    public void RemoveAdditional(AdditionalEffect addtionalEffect)
    {
        switch (addtionalEffect.AdditionalType)
        {
            case AdditionalEffect.Type.Hit:
                if (CheckForRemoveAdditionalDuplication(Model.HitAdditionals, addtionalEffect as HitAdditional))
                {
                    Model.HitAdditionals.Remove(addtionalEffect as HitAdditional);
                    // ��Ʋ�ý��ۿ��� ���� ȿ�� ����
                    Battle.RemoveHitAdditionalList(addtionalEffect as HitAdditional);
                }
                break;
            case AdditionalEffect.Type.Throw:
                if (CheckForRemoveAdditionalDuplication(Model.ThrowAdditionals, addtionalEffect as ThrowAdditional))
                {
                    Model.ThrowAdditionals.Remove(addtionalEffect as ThrowAdditional);
                }
                break;
            case AdditionalEffect.Type.Player:
                if (CheckForRemoveAdditionalDuplication(Model.PlayerAdditionals, addtionalEffect as PlayerAdditional))
                {

                    addtionalEffect.Exit();
                    Model.PlayerAdditionals.Remove(addtionalEffect as PlayerAdditional);
                }
                break;
        }
    }

    public void EnterPlayerAdditional()
    {
        foreach (PlayerAdditional playerAdditional in Model.PlayerAdditionals)
        {
            playerAdditional.Enter();
        }
    }
    public void ExitPlayerAdditional()
    {
        foreach (PlayerAdditional playerAdditional in Model.PlayerAdditionals)
        {
            playerAdditional.Exit();
        }
    }

    public void UpdatePlayerAdditional()
    {
        foreach (PlayerAdditional playerAdditional in Model.PlayerAdditionals)
        {
            playerAdditional.Update();
        }
    }

    public void FixedPlayerAdditional()
    {
        foreach (PlayerAdditional playerAdditional in Model.PlayerAdditionals)
        {
            playerAdditional.FixedUpdate();
        }
    }
    public void TriggerPlayerAdditional()
    {
        foreach (PlayerAdditional playerAdditional in Model.PlayerAdditionals)
        {
            playerAdditional.Trigger();
        }
    }
    public void TriggerFirstPlayerAdditional()
    {
        foreach (PlayerAdditional playerAdditional in Model.PlayerAdditionals)
        {
            playerAdditional.TriggerFirst();
        }
    }
    /// <summary>
    /// �߰�ȿ�� �߰� �� �ߺ� üũ
    /// </summary>
    private bool CheckForAddAdditionalDuplication<T>(List<T> additinalList, T additinal) where T : AdditionalEffect
    {
        int index = additinalList.FindIndex(origin => origin.Origin.Equals(additinal.Origin));
        if (index >= additinalList.Count)
            return false;
        // �ߺ� ��
        if (index != -1)
            return false;
        else
            return true;

    }
    /// <summary>
    /// �߰�ȿ�� ���� �� �ߺ� üũ
    /// </summary>
    private bool CheckForRemoveAdditionalDuplication<T>(List<T> additinalList, T additinal) where T : AdditionalEffect
    {
        int index = additinalList.FindIndex(origin => origin.Origin.Equals(additinal.Origin));
        if (index >= additinalList.Count)
            return false;
        // �ߺ� �� (���� �� ���� ��)
        if (index != -1)
        {
            Model.AdditionalEffects.Remove(additinal);
            return true;
        }
        else
            return false;
    }
    #endregion


    /// <summary>
    /// TPS ���� ī�޶� ȸ��
    /// </summary>
    private void RotateCamera()
    {
        float angleX = InputKey.GetAxis(InputKey.MouseX);
        float angleY = default;
        // üũ�� ���콺 ���ϵ� ����
        if (IsVerticalCameraMove == true)
            angleY = InputKey.GetAxis(InputKey.MouseY);
        _cameraRotateSpeed = setting.cameraSpeed;
        Vector2 mouseDelta = new Vector2(angleX, angleY) * _cameraRotateSpeed;
        Vector3 camAngle = CamareArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;
        x = x < 180 ? Mathf.Clamp(x, -10f, 50f) : Mathf.Clamp(x, 360f - _cameraRotateAngle, 361f);
        CamareArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);

        if (IsVerticalCameraMove)
        {
            // ��������Ʈ ��������
            MuzzletPoint.rotation = Quaternion.Euler(x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
    }
    #region ������ üũ ����
    /// <summary>
    /// ���� üũ
    /// </summary>
    private void CheckGround()
    {
        // ��¦������ ��
        Vector3 CheckPos = _groundCheckPos.position;
        if (Physics.SphereCast(
            CheckPos,
            0.25f,
            Vector3.down,
            out RaycastHit hit,
            0.4f,
            Layer.GetLayerMaskEveryThing(),
            QueryTriggerInteraction.Ignore))
        {
            IsGround = true;
            // ���� �� �ִ� ���� üũ
            Vector3 normal = hit.normal;
            if (normal.y > 1 - _slopeAngle)
            {
                CanClimbSlope = true;
            }
            else
            {
                CanClimbSlope = false;
            }
        }
        else
        {
            IsGround = false;
            CanClimbSlope = false;
        }
    }

    private void DrawCheckGround()
    {
        Gizmos.color = Color.yellow;

        Vector3 CheckPos = _groundCheckPos.position;

        if (Physics.SphereCast(CheckPos, 0.25f, Vector3.down, out RaycastHit hit, 0.4f, Layer.GetLayerMaskEveryThing(), QueryTriggerInteraction.Ignore))
        {
            Gizmos.DrawLine(CheckPos, hit.point);
            Gizmos.DrawWireSphere(CheckPos + Vector3.down * hit.distance, 0.3f);
        }
    }

    /// <summary>
    /// ���鿡 ������� üũ
    /// </summary>
    private void CheckIsNearGround()
    {
        Vector3 CheckPos = new Vector3(transform.position.x, transform.position.y + 0.31f, transform.position.z);
        if (Physics.SphereCast(CheckPos, 0.3f, Vector3.down, out RaycastHit hit, 1f, Layer.GetLayerMaskEveryThing(), QueryTriggerInteraction.Ignore))
        {
            IsNearGround = true;
        }
        else
        {
            IsNearGround = false;
        }
    }

    private void DrawIsNearGround()
    {
        Vector3 CheckPos = new Vector3(transform.position.x, transform.position.y + 0.31f, transform.position.z);
        if (Physics.SphereCast(CheckPos, 0.3f, Vector3.down, out RaycastHit hit, 1, Layer.GetLayerMaskEveryThing(), QueryTriggerInteraction.Ignore))
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(CheckPos, hit.point);
            Gizmos.DrawWireSphere(CheckPos + Vector3.down * hit.distance, 0.3f);
        }
    }

    /// <summary>
    /// ��üũ
    /// </summary>
    private void CheckWall()
    {
        int layerMask = 0;
        layerMask |= 1 << Layer.Wall;
        layerMask |= 1 << Layer.Monster;
        int hitCount = Physics.OverlapCapsuleNonAlloc(
            WallCheckPos.Foot.position,
            WallCheckPos.Head.position,
            _wallCheckDistance,
            OverLapColliders,
            layerMask);

        if (hitCount > 0)
        {
            IsWall = true;
        }
        else
        {
            IsWall = false;
        }

    }

    private void DrawWallCheck()
    {
        Gizmos.color = Color.green;

        Vector3 footPos = WallCheckPos.Foot.position + transform.forward * _wallCheckDistance;
        Vector3 headPos = WallCheckPos.Head.position + transform.forward * _wallCheckDistance;

        Gizmos.DrawLine(footPos, headPos);
    }
    #endregion

    /// <summary>
    /// ���׹̳� ȸ�� �ڷ�ƾ
    /// </summary>
    IEnumerator RecoveryStamina()
    {
        CanStaminaRecovery = true;
        while (true)
        {
            // �ʴ� MaxStamina / RegainStamina ��ŭ ȸ��
            // ���� ���׹̳��� ��á���� ���̻� ȸ������
            // ���� ���׹̳� ��� �� ��Ÿ�� ���¸� ��Ÿ�Ӹ�ŭ ȸ������
            if (CanStaminaRecovery == true)
            {
                Model.CurStamina += Model.RegainStamina * Time.deltaTime;
            }
            if (Model.CurStamina >= Model.MaxStamina)
            {
                Model.CurStamina = Model.MaxStamina;
            }

            if (IsStaminaCool == true)
            {
                IsStaminaCool = false;
                //yield return 1f.GetDelay();
                yield return Model.StaminaCoolTime.GetDelay();
            }

            yield return null;
        }
    }
    #region Ű�Է� ����
    /// <summary>
    /// Ű�Է� ����
    /// </summary>
    private void ChackInput()
    {
        float x = InputKey.GetAxisRaw(InputKey.Horizontal);
        float z = InputKey.GetAxisRaw(InputKey.Vertical);
        MoveDir = new Vector3(x, 0, z);

        if (IsTargetHolding == false && IsTargetToggle == false)
        {
            //if (Input.GetMouseButtonDown(2))
            //{
            //    //TODO: ī�޶� ���� Ȧ�� ���
            //    IsTargetHolding = true;
            //    _cameraHolder.gameObject.SetActive(true);
            //}
            if (InputKey.GetButtonDown(InputKey.RockOn) && IsTargetHolding == false)
            {
                //TODO: ī�޶� ���� Ȧ�� ���
                IsTargetToggle = true;
                _cameraHolder.gameObject.SetActive(true);
            }
        }
        else
        {
            //if (Input.GetMouseButtonUp(2) && IsTargetToggle == false)
            //{
            //    //TODO: ī�޶� ���� Ȧ�� Ǯ��
            //    IsTargetHolding = false;
            //    _cameraHolder.gameObject.SetActive(false);
            //}
            if (InputKey.GetButtonDown(InputKey.RockCancel) && IsTargetHolding == false)
            {
                //TODO: ī�޶� ���� Ȧ�� Ǯ��
                IsTargetToggle = false;
                _cameraHolder.gameObject.SetActive(false);
            }
        }
    }
    private void CheckAnyState()
    {
        if (IsDead == true || IsHit == true)
            return;

        if (InputKey.GetButtonDown(InputKey.Dash) && CurState != State.Dash && CurState != State.JumpDown)
        {
            ChangeState(PlayerController.State.Dash);
        }
    }
    #endregion

    #region  �˹�
    /// <summary>
    /// �˹� ����(��ġ ����)
    /// </summary>
    public void DontKnockBack(Transform target)
    {
        Rigidbody targetRb = target.GetComponent<Rigidbody>();
        targetRb.velocity = new(0, targetRb.velocity.y, 0);
    }

    /// <summary>
    /// �ش� �������� �Է� �Ÿ���ŭ �˹�
    /// </summary>
    public void DoKnockBack(Transform target, Vector3 dir, float distance)
    {
        Rigidbody targetRb = target.GetComponent<Rigidbody>();

        //targetRb.AddForce(dir * distance * 10f, ForceMode.Impulse);

        CoroutineHandler.StartRoutine(KnockBackRoutine(targetRb, dir, distance));
    }
    /// <summary>
    /// ������ �߽����� �Է°Ÿ���ŭ �˹�
    /// </summary>
    public void DoKnockBack(Transform target, Transform attacker, float distance)
    {
        Vector3 attackerPos = new(attacker.position.x, 0, attacker.position.z);
        Vector3 targetPos = new(target.position.x, 0, target.position.z);
        Vector3 knockBackDir = targetPos - attackerPos;
        Rigidbody targetRb = target.GetComponent<Rigidbody>();


        //targetRb.AddForce(knockBackDir.normalized * distance * 10f, ForceMode.Impulse);

        CoroutineHandler.StartRoutine(KnockBackRoutine(targetRb, knockBackDir, distance));
    }

    /// <summary>
    /// Ư�� ������ �߽����� �˹�
    /// </summary>
    /// <param name="target"></param>
    /// <param name="pos"></param>
    /// <param name="distance"></param>
    public void DoKnockBackFromPos(Transform target, Vector3 pos, float distance)
    {
        Vector3 attackerPos = new(pos.x, 0, pos.z);
        Vector3 targetPos = new(target.position.x, 0, target.position.z);
        Vector3 knockBackDir = targetPos - attackerPos;
        Rigidbody targetRb = target.GetComponent<Rigidbody>();

        //targetRb.AddForce(knockBackDir.normalized * distance * 10f, ForceMode.Impulse);

        CoroutineHandler.StartRoutine(KnockBackRoutine(targetRb, knockBackDir, distance));
    }

    IEnumerator KnockBackRoutine(Rigidbody targetRb, Vector3 knockBackDir, float distance)
    {
        Vector3 originPos = targetRb.position;

        targetRb.transform.LookAt(transform.position);
        targetRb.transform.rotation = Quaternion.Euler(0, targetRb.transform.eulerAngles.y, 0);
        // Ÿ���� �� �ٶ󺸵���
        while (true)
        {
            targetRb.transform.Translate(knockBackDir * Time.deltaTime * 30f, Space.World);

            if (Vector3.Distance(originPos, targetRb.position) > distance)
            {
                break;
            }

            Vector3 targetPos = new(targetRb.transform.position.x, targetRb.transform.position.y + 0.75f, targetRb.transform.position.z);
            if (Physics.SphereCast(targetRb.transform.position, 0.2f, knockBackDir, out RaycastHit hit, 0.3f, 1 << Layer.Wall, QueryTriggerInteraction.Ignore))
            {
                break;
            }
            yield return null;
        }
    }
    #endregion
    #region ������ ���
    /// <summary>
    /// �⺻ ���� ������
    /// </summary>
    public int GetFinalDamage()
    {
        int finalDamage = 0;
        finalDamage = GetCommonDamage(finalDamage);
        return finalDamage;
    }
    /// <summary>
    /// ������ �߰�
    /// </summary>
    public int GetFinalDamage(int addtionalDamage)
    {
        int finalDamage = 0;
        // �߰� ������
        finalDamage += addtionalDamage;
        finalDamage = GetCommonDamage(finalDamage);
        return finalDamage;
    }
    /// <summary>
    /// ������ ����
    /// </summary>
    public int GetFinalDamage(float multiplier)
    {
        int finalDamage = 0;
        finalDamage = GetCommonDamage(finalDamage);

        // ������ ���� �߰�
        finalDamage = (int)(finalDamage * multiplier);
        return finalDamage;
    }
    /// <summary>
    /// �߰� ������ + ������ ����
    /// </summary>
    public int GetFinalDamage(int addtionalDamage, float multiplier)
    {
        int finalDamage = 0;
        // �߰� ������
        finalDamage += addtionalDamage;
        finalDamage = GetCommonDamage(finalDamage);

        // ������ ���� �߰�
        finalDamage = (int)(finalDamage * multiplier);
        return finalDamage;
    }
    /// <summary>
    /// ������ ��
    /// </summary>
    private int GetCommonDamage(int finalDamage)
    {
        // �⺻ ���� ������ 
        finalDamage += Model.AttackPower;
        // ġ��Ÿ ������
        if (Random.value < Model.CriticalChance / 100f)
            finalDamage = (int)(finalDamage * (Model.CriticalDamage / 100f));

        return finalDamage;
    }
    #endregion

    IEnumerator ControlMousePointer()
    {
        while (true)
        {
            if (Time.timeScale == 1 && IsMouseVisible == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (Time.timeScale == 0 || IsMouseVisible == true)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            yield return null;
        }
    }

    private void TriggerCantOperate()
    {
        _states[(int)CurState].TriggerCantOperate();
    }
    // �ʱ� ���� ============================================================================================================================================ //
    /// <summary>
    /// �ʱ� ����
    /// </summary>
    private void Init()
    {
        InitGetComponent();
        InitPlayerStates();

        _defaultMuzzlePointRot = MuzzletPoint.localRotation;
    }

    /// <summary>
    /// �÷��̾� ���� �迭 ����
    /// </summary>
    private void InitPlayerStates()
    {
        _states[(int)State.Idle] = new IdleState(this);                 // Idle
        _states[(int)State.Run] = new RunState(this);                   // �̵�(�޸���)
        _states[(int)State.MeleeAttack] = new MeleeAttackState(this);   // ��������
        _states[(int)State.ThrowAttack] = new ThrowState(this);         // ��ô����
        _states[(int)State.SpecialAttack] = new SpecialAttackState(this); // Ư������
        _states[(int)State.Jump] = new JumpState(this);                 // ����
        _states[(int)State.DoubleJump] = new DoubleJumpState(this);     // ��������
        _states[(int)State.JumpAttack] = new JumpAttackState(this);     // ��������
        _states[(int)State.JumpDown] = new JumpDownState(this);         // �ϰ� ���� 
        _states[(int)State.Fall] = new FallState(this);                 // �߶�
        _states[(int)State.DoubleJumpFall] = new DoubleJumpFallState(this); // �������� �߶�
        _states[(int)State.Dash] = new DashState(this);                 // �뽬
        _states[(int)State.Drain] = new DrainState(this);               // �巹��
        _states[(int)State.Hit] = new HitState(this);                   // �ǰ�
        _states[(int)State.Dead] = new DeadState(this);                 // ���
    }

    /// <summary>
    /// UI�̺�Ʈ ����
    /// </summary>
    private void InitUIEvent()
    {
        PlayerPanel panel = View.Panel;

        // ��ô������Ʈ
        Model.CurThrowCountSubject = new Subject<int>();
        Model.CurThrowCountSubject
            .DistinctUntilChanged()
            .Subscribe(x =>  View.UpdateText(panel.ObjectCount, $"{x} / {Model.MaxThrowables}") );
        View.UpdateText(panel.ObjectCount, $"{Model.CurThrowables} / {Model.MaxThrowables}");

        // ü��
        Model.CurHpSubject
            .DistinctUntilChanged()
            .Subscribe(x => panel.BarValueController(panel.HpBar, Model.CurHp, Model.MaxHp));
        panel.BarValueController(panel.HpBar, Model.CurHp, Model.MaxHp);

        // ���׹̳�
        Model.CurStaminaSubject
            .DistinctUntilChanged()
            .Subscribe(x => panel.BarValueController(panel.StaminaBar, Model.CurStamina, Model.MaxStamina));
        panel.BarValueController(panel.StaminaBar, Model.CurStamina, Model.MaxStamina);

        // Ư���ڿ�
        Model.CurManaSubject
            .DistinctUntilChanged()
            .Subscribe(x => panel.MpBar.value = x);
        panel.MpBar.value = Model.CurHp;

        // Ư������ ����
        Model.SpecialChargeGageSubject
            .DistinctUntilChanged()
            .Subscribe(x => panel.ChargingMpBar.value = x);
        panel.ChargingMpBar.value = Model.SpecialChargeGage;

        // ���׹̳� ����
        Model.MaxStaminaCharge = 1;
        Model.CurStaminaChargeSubject
            .DistinctUntilChanged()
            .Subscribe(x => panel.BarValueController(panel.ChanrgeStaminaBar, Model.CurStaminaCharge, Model.MaxStaminaCharge));
        panel.BarValueController(panel.ChanrgeStaminaBar, Model.CurStaminaCharge, Model.MaxStaminaCharge);
    }

    /// <summary>
    /// �ʱ� ��������Ʈ ����
    /// </summary>
    private void InitGetComponent()
    {
        Model = GetComponent<PlayerModel>();
        View = GetComponent<PlayerView>();
        Rb = GetComponent<Rigidbody>();
        Battle = GetComponent<BattleSystem>();
    }
    private void InitAdditionnal()
    {
        Model.AdditionalEffects.Clear();
        List<AdditionalEffect> tempList = new List<AdditionalEffect>();

        ProcessInitAddtional(tempList, Model.PlayerAdditionals);
        ProcessInitAddtional(tempList, Model.ThrowAdditionals);
        ProcessInitAddtional(tempList, Model.HitAdditionals);
    }
    private void ProcessInitAddtional<T>(List<AdditionalEffect> tempList, List<T> additionals) where T : AdditionalEffect
    {
        foreach (AdditionalEffect additional in additionals)
        {
            tempList.Add(additional);
        }
        additionals.Clear();
        foreach (AdditionalEffect additional in tempList)
        {
            AddAdditional(additional);
        }
        tempList.Clear();
    }

    private void StartRoutine()
    {
        StartCoroutine(RecoveryStamina());
    }


    #region �ִϸ��̼� �ݹ�

    public void OnTrigger()
    {
        TriggerPlayerAdditional();
        _states[(int)CurState].OnTrigger();
    }
    public void EndAnimation()
    {
        _states[(int)CurState].EndAnimation();
    }
    public void OnCombo()
    {
        _states[(int)CurState].OnCombo();
    }
    public void EndCombo()
    {
        _states[(int)CurState].EndCombo();
    }
    #endregion
}
