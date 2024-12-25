using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class PlayerCameraHold : MonoBehaviour
{
    [SerializeField] GameObject _targetEffect;
    [Range(0,360)][SerializeField] float _angle;
    [SerializeField] float _detectRange;
    private PlayerController _player;

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
        //transform.SetParent(null);
    }

    private void OnEnable()
    {
        SetTargetList();
    }

    private void OnDisable()
    {
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
            _targetEffect.transform.position = _target.position;
        }
    }

    /// <summary>
    /// �� Ÿ����
    /// </summary>
    private void SetTargetList()
    {
        // �÷��̾ ���� �ٶ�
        _player.LookAtCameraFoward();
        
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
            float targetAngle = Vector3.Angle(transform.forward, targetDir); // ��ũ�ڻ��� �ʿ� (������)
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
    }

    //TargetInfo ����
    private TargetInfo SetTargetInfo(Transform target, float distance)
    {
        TargetInfo info = new TargetInfo();
        info.Target = target;
        info.Distance = distance;
        return info;
    }
}
