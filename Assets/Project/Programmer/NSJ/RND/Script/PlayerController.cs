
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerModel Model;
    [HideInInspector] public PlayerView View;
    [HideInInspector] public Rigidbody Rb;

    Collider[] colliders = new Collider[20];
    [SerializeField] public float AttackBufferTime;
    [SerializeField] private float _range;
    [SerializeField] private float _angle;

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
    [SerializeField] private CameraStruct _cameraStruct;
    public Transform CamareArm { get { return _cameraStruct.CamaraArm; } set { _cameraStruct.CamaraArm = value; } }
    private Transform _cameraPos { get { return _cameraStruct.CameraPos; } set { _cameraStruct.CameraPos = value; } }
    private float _cameraRotateSpeed { get { return _cameraStruct.CameraRotateSpeed; } set { _cameraStruct.CameraRotateSpeed = value; } }
    private bool _isVerticalCameraMove { get { return _cameraStruct.IsVerticalCameraMove;} set { _cameraStruct.IsVerticalCameraMove = value; } }
    #endregion
    public enum State { Idle, Run, MeleeAttack, Size }

    private PlayerState[] _states = new PlayerState[(int)State.Size];
    private State _curState;


    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        Camera.main.transform.SetParent(_cameraPos,true);
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

    public void ChangeState(State state)
    {
        _states[(int)_curState].Exit();
        _curState = state;
        _states[(int)_curState].Enter();
    }
    public void AttackMelee()
    {
        // ���� �տ� �ִ� ���͵��� Ȯ���ϰ� �ǰ� ����
        // 1. ���濡 �ִ� ���� Ȯ��
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
        Vector3 attackPos = playerPos;
        int hitCount = Physics.OverlapSphereNonAlloc(attackPos, _range, colliders, 1<<4);
        for (int i = 0; i < hitCount; i++) 
        {
            // 2. ���� ���� �ִ��� Ȯ��
            Vector3 source = transform.position;
            source.y = 0;
            Vector3 destination = colliders[i].transform.position;
            destination.y = 0;

            Vector3 targetDir = (destination - source).normalized;
            float targetAngle =  Vector3.Angle(transform.forward, targetDir); // ��ũ�ڻ��� �ʿ� (������)
            if (targetAngle > _angle * 0.5f)
                continue;

           Debug.Log($"{colliders[i].gameObject.name} ��������");
        }
    }

    private void OnDrawGizmos()
    {
        //�Ÿ�
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
        Vector3 attackPos = playerPos;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos, _range);

        //����
        Vector3 rightDir= Quaternion.Euler(0, _angle * 0.5f, 0) * transform.forward;
        Vector3 leftDir = Quaternion.Euler(0, _angle * -0.5f, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position,transform.position + rightDir * _range );
        Gizmos.DrawLine(transform.position, transform.position + leftDir * _range );
    }
    /// <summary>
    /// TPS ���� ī�޶� ȸ��
    /// </summary>
    private void RotateCamera()
    {
        float angleX = Input.GetAxis("Mouse X");
        float angleY = default;
        if (_isVerticalCameraMove == true)
            angleY = Input.GetAxis("Mouse Y");
        Vector2 mouseDelta = new Vector2(angleX, angleY) * _cameraRotateSpeed;
        Vector3 camAngle = CamareArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;
        x = x < 180 ? Mathf.Clamp(x, -10f, 50f) : Mathf.Clamp(x, 340f, 361f);
        CamareArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }


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
