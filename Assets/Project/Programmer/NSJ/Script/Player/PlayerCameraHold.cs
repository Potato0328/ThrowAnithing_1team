using BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject.SpaceFighter;

public class PlayerCameraHold : MonoBehaviour
{
    [SerializeField] GameObject _targetEffect;
    [Range(0,360)][SerializeField] float _angle;
    [SerializeField] float _detectRange;
    private PlayerController _player;

    private int _targetIndex;
    private bool _isStart;
    Coroutine _checkDistanceToTarget;
    Coroutine _changeTargetRoutine;
    [System.Serializable]
    struct TargetInfo
    {
        public Transform Target;
        public float Distance;
    }

    [SerializeField] private List<TargetInfo> _targetList = new List<TargetInfo>();
    [SerializeField]private Transform _target;
    private void Awake()
    {
        _player = GetComponentInParent<PlayerController>();
        _targetEffect.transform.SetParent(null);

        Camera.main.GetOrAddComponent<CinemachineBrain>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
        _isStart = true;
    }
    private void OnEnable()
    {
        if (_isStart == false)
            return;
        _targetEffect.SetActive(true);
        SetTargetList();

        if(_changeTargetRoutine == null)
        {
            _changeTargetRoutine = StartCoroutine(ChangeTargetRoutine());
        }
    }

    private void OnDisable()
    {
        if(_checkDistanceToTarget != null)
        {
            StopCoroutine(_checkDistanceToTarget);
            _checkDistanceToTarget = null;
        }
        if(_changeTargetRoutine != null)
        {
            StopCoroutine(_changeTargetRoutine);
            _changeTargetRoutine = null;
        }

        _targetEffect.SetActive(false);
        _targetList.Clear();
        _target = null;
        _player.IsTargetHolding = false;
        _player.IsTargetToggle = false;
    }

    private void Update()
    {
        // Ÿ���� ã�� ��������
        if (_targetList.Count <= 0)
            return;

        // Ÿ���� �׾����� (Destroy�ǰų� Disable������)
        if (_target == null || _target.gameObject.activeSelf == false)
        {
            // ����Ʈ�� ��� ��, �ٽ� ������ ���� ��Ž���Ѵ�
            _targetList.Clear();
            SetTargetList();
        }
        else
        {
            // �÷��̾��� Ÿ�� ������ ������ Ÿ����ġ�� ����
            _player.TargetPos = _target.transform.position;
            // Ÿ�� ����Ʈȿ�� Ÿ����ġ
            _targetEffect.transform.position = new(_target.position.x, _target.position.y + 0.3f, _target.position.z);
        }

    }

    /// <summary>
    /// �� Ÿ����
    /// </summary>
    private void SetTargetList()
    {
        // �÷��̾ ���� �ٶ�
        //_player.LookAtCameraFoward();
        
        // �ֺ��� ���Ͱ� �ִ��� ��ĵ
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _detectRange, _player.OverLapColliders, 1 << Layer.Monster);
        for (int i = 0; i < hitCount; i++)
        {
            Transform targetTransform = _player.OverLapColliders[i].transform;
            // 2. ���� ���� �ִ��� Ȯ��
            Vector3 source = transform.position;
            source.y = 0;
            Vector3 destination = targetTransform.position;
            destination.y = 0;

            Vector3 targetDir = (destination - source).normalized;

            float targetAngle = Vector3.Angle(_player.CamareArm.forward, targetDir); // ��ũ�ڻ��� �ʿ� (������)
            if (targetAngle > _angle * 0.5f)
                continue;

            // ���ǿ� �����ϸ� �ش� Ÿ���� �Ÿ��� �Բ� ����
            TargetInfo targetInfo = SetTargetInfo(targetTransform, Vector3.Distance(transform.position, targetTransform.position));
            _targetList.Add(targetInfo);
        }
        // ��ĵ�� �����ߴٸ� �ٷ� ����
        if (_targetList.Count <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
        // Ÿ���� �Ÿ������� ����
        _targetList.Sort((a, b) => a.Distance.CompareTo(b.Distance));
        // ���� ����� ���� Ÿ������ ����
        _target = _targetList[0].Target;
        _targetIndex = 0;

        if (_checkDistanceToTarget == null)
        {
            _checkDistanceToTarget = StartCoroutine(CheckDistanceToTarget());
        }
    }

    //TargetInfo ����
    private TargetInfo SetTargetInfo(Transform target, float distance)
    {
        TargetInfo info = new TargetInfo();
        info.Target = target;
        info.Distance = distance;
        return info;
    }

   /// <summary>
   /// �÷��̾�� Ÿ���� �־������� Ÿ���� �ڵ� ����
   /// </summary>
   /// <returns></returns>
    IEnumerator CheckDistanceToTarget()
    {
        while (true)
        {
            if (_target != null)
            {
                if (Vector3.Distance(_player.transform.position, _target.position) > _detectRange)
                {
                    gameObject.SetActive(false);
                }
            }
            yield return 0.5f.GetDelay();
        }
    }

    IEnumerator ChangeTargetRoutine()
    {
        while (true)
        {
            float mouseScroll = Input.GetAxisRaw(InputKey.Mouse_ScrollWheel);
            if (_player.IsTargetToggle == true && mouseScroll > 0)
            {
                // Ÿ�� �ε��� �ø�
                _targetIndex++;
                // Ÿ�� �ε����� ����Ʈ ī��Ʈ���� �Ѿ��ٸ� �ٽ� ó������
                if (_targetIndex >= _targetList.Count)
                {
                    _targetIndex = 0;
                }
                // �ش� Ÿ������ ����
                _target = _targetList[_targetIndex].Target;
                yield return 0.2f.GetDelay();
            }
            else if (_player.IsTargetToggle == true && mouseScroll < 0)
            {
                // Ÿ�� �ε��� ����
                _targetIndex--;
                // Ÿ�� �ε����� 0 ���϶�� �� ������
                if (_targetIndex < 0)
                {
                    _targetIndex = _targetList.Count - 1;
                }
                // �ش� Ÿ������ ����
                _target = _targetList[_targetIndex].Target;
                yield return 0.2f.GetDelay();
            }
            yield return null;
        }
    }
}
