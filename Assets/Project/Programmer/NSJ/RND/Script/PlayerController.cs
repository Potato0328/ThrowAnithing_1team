
using Assets.Project.Programmer.NSJ.RND.Script;
using UnityEngine;

[RequireComponent(typeof(PlayerModel))]
[RequireComponent(typeof(PlayerView))]
public class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerModel Model;
    [HideInInspector] public PlayerView View;
    [HideInInspector] public Rigidbody Rb;

    Collider[] colliders = new Collider[20];

    #region ���� ���� �ʵ�
    [System.Serializable]
    struct AttackStruct
    {
        public float AttackHeight;
        public float AttackBufferTime;
        public Transform MuzzlePoint;
        public ThrowObject ThrowPrefab;
    }
    [Header("���� ���� �ʵ�")]
    [SerializeField] private AttackStruct _attackStruct;
    public float AttackBufferTime { get { return _attackStruct.AttackBufferTime; } set { _attackStruct.AttackBufferTime = value; } }
    private float _attackHeight { get { return _attackStruct.AttackHeight; } set { _attackStruct.AttackHeight = value; } }
    private Transform _muzzltPoint { get { return _attackStruct.MuzzlePoint; } set { _attackStruct.MuzzlePoint = value; } }
    private ThrowObject _throwPrefab { get { return _attackStruct.ThrowPrefab; } set { _attackStruct.ThrowPrefab = value; } }
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
        [Range(0f, 5f)] public float CameraRotateSpeed;
        public bool IsVerticalCameraMove;
    }
    [Header("ī�޶� ���� �ʵ�")]
    [SerializeField] private CameraStruct _cameraStruct;
    public Transform CamareArm { get { return _cameraStruct.CamaraArm; } set { _cameraStruct.CamaraArm = value; } }
    private Transform _cameraPos { get { return _cameraStruct.CameraPos; } set { _cameraStruct.CameraPos = value; } }
    private float _cameraRotateSpeed { get { return _cameraStruct.CameraRotateSpeed; } set { _cameraStruct.CameraRotateSpeed = value; } }
    private bool _isVerticalCameraMove { get { return _cameraStruct.IsVerticalCameraMove; } set { _cameraStruct.IsVerticalCameraMove = value; } }
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
    public enum State { Idle, Run, MeleeAttack, ThrowAttack, Size }

    private PlayerState[] _states = new PlayerState[(int)State.Size];
    private State _curState;


    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        Camera.main.transform.SetParent(_cameraPos, true);
        _states[(int)_curState].Enter();
    }

    private void OnDisable()
    {
        _states[(int)_curState].Exit();
    }

    private void Update()
    {
        _states[(int)_curState].Update();

        RotateCamera();
    }

    private void FixedUpdate()
    {
        _states[(int)_curState].FixedUpdate();
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void ChangeState(State state)
    {
        _states[(int)_curState].Exit();
        _curState = state;
        _states[(int)_curState].Enter();
    }

    /// <summary>
    /// ������Ʈ ������ ����
    /// </summary>
    public void ThrowObject()
    {
        ThrowObject throwObject = Instantiate(_throwPrefab, _muzzltPoint.position, _muzzltPoint.rotation);
        throwObject.Init(Model.Throw, Model.HitAdditionals);
        throwObject.Shoot();
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void AttackMelee()
    {
        // ���� �տ� �ִ� ���͵��� Ȯ���ϰ� �ǰ� ����
        // 1. ���濡 �ִ� ���� Ȯ��
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + _attackHeight, transform.position.z);
        Vector3 attackPos = playerPos;
        int hitCount = Physics.OverlapSphereNonAlloc(attackPos, Model.Range, colliders, 1 << 4);
        for (int i = 0; i < hitCount; i++)
        {
            // 2. ���� ���� �ִ��� Ȯ��
            Vector3 source = transform.position;
            source.y = 0;
            Vector3 destination = colliders[i].transform.position;
            destination.y = 0;

            Vector3 targetDir = (destination - source).normalized;
            float targetAngle = Vector3.Angle(transform.forward, targetDir); // ��ũ�ڻ��� �ʿ� (������)
            if (targetAngle > Model.Angle * 0.5f)
                continue;

            IHit hit = colliders[i].GetComponent<IHit>();

            int attackDamage = (int)(Model.MeleeDamage * Model.DamageMultiplier);
            hit.TakeDamage(attackDamage);
        }
    }

    public void AddHitAdditional(HitAdditional hitAdditional)
    {
        Model.HitAdditionals.Add(hitAdditional);
    }

    private void OnDrawGizmos()
    {
        if (Model == null)
            return;
        //�Ÿ�
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + _attackHeight, transform.position.z);
        Vector3 attackPos = playerPos;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos, Model.Range);

        //����
        Vector3 rightDir = Quaternion.Euler(0, Model.Angle * 0.5f, 0) * transform.forward;
        Vector3 leftDir = Quaternion.Euler(0, Model.Angle * -0.5f, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + rightDir * Model.Range);
        Gizmos.DrawLine(transform.position, transform.position + leftDir * Model.Range);
    }
    /// <summary>
    /// TPS ���� ī�޶� ȸ��
    /// </summary>
    private void RotateCamera()
    {
        float angleX = Input.GetAxis("Mouse X");
        float angleY = default;
        // üũ�� ���콺 ���ϵ� ����
        if (_isVerticalCameraMove == true)
            angleY = Input.GetAxis("Mouse Y");
        Vector2 mouseDelta = new Vector2(angleX, angleY) * _cameraRotateSpeed;
        Vector3 camAngle = CamareArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;
        x = x < 180 ? Mathf.Clamp(x, -10f, 50f) : Mathf.Clamp(x, 340f, 361f);
        CamareArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    // �ʱ� ���� ============================================================================================================================================ //
    /// <summary>
    /// �ʱ� ����
    /// </summary>
    private void Init()
    {
        InitGetComponent();
        InitPlayerStates();
    }

    /// <summary>
    /// �÷��̾� ���� �迭 ����
    /// </summary>
    private void InitPlayerStates()
    {
        _states[(int)State.Idle] = new IdleState(this);                 // Idle
        _states[(int)State.Run] = new RunState(this);                   // �̵�(�޸���)
        _states[(int)State.MeleeAttack] = new MeleeAttackState(this);   // ��������
        _states[(int)State.ThrowAttack] = new ThrowState(this);
    }

    /// <summary>
    /// �ʱ� ��������Ʈ ����
    /// </summary>
    private void InitGetComponent()
    {
        Model = GetComponent<PlayerModel>();
        View = GetComponent<PlayerView>();
        Rb = GetComponent<Rigidbody>();
    }
}
