using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public ThrowObjectData Data;
    public bool CanAttack;
    [SerializeField] public List<ThrowAdditional> ThrowAdditionals = new List<ThrowAdditional>();

    // ������Ʈ ��ü ������
    public int ObjectDamage;
    // �÷��̾��� �߰� ������
    [HideInInspector]public int PlayerDamage;
    public int Damage => ObjectDamage + PlayerDamage;
    // ������ ���
    [HideInInspector] public float DamageMultyPlier;
    [Space(10)]
    // ���� ����(���߽�)
    public float Radius;
    // �˹�Ÿ�
    public float KnockBackDistance;
    // ���׹̳� ȸ����
    public float SpecialRecovery;

    public List<GameObject> IgnoreTargets = new List<GameObject>();
    protected Collider[] _overlapCollider = new Collider[20];
    protected PlayerController _player;

    [HideInInspector] public Rigidbody Rb;
    protected Collider _collider;

    protected void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _player = GameObject.FindGameObjectWithTag(Tag.Player).GetComponent<PlayerController>();
        gameObject.layer = Layer.ThrowObject;

        CanAttack = true;
    }

    private void Start()
    {
        EnterThrowAdditional();
    }
    private void OnEnable()
    {
        _collider.isTrigger = true;
    }
    private void OnDisable()
    {
        ClearThrowAddtional();
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == Layer.Player)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.AddThrowObject(this);
            DestroyObject();
        }

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger == true)
            return;

        string tag = other.gameObject.tag;
        int layer = other.gameObject.layer;
        if (layer == Layer.Monster)
        {
            // �����ؾ��ϴ� Ÿ�ٴ���� �������� ����
            if (IgnoreTargets.Contains(other.gameObject) == true) 
                return;

            TriggerThrowAddtional();
            HitTarget();
            _player.ThrowObjectResultCallback(true);
        } 
        else if (tag != Tag.Player )
        {
            CanAttack = false;
            _collider.isTrigger = false;
            _player.ThrowObjectResultCallback(false);
        }
    }

    private void Update()
    {
        UpdateThrowAdditional();

        if(CanAttack == false)
        {
            if(_player.Model.CurThrowables >= _player.Model.MaxThrowables)
            {
                gameObject.layer = Layer.CantPickTrash;
            }
            else
            {
                gameObject.layer = Layer.CanPickTrash;
            }
        }
    }
    private void FixedUpdate()
    {
        FixedUpdateThrowAdditional();
    }
    #region Init
    public void Init(PlayerController player, List<ThrowAdditional> throwAdditionals)
    {
        _player = player;
        Radius = player.Model.BoomRadius;
        // ���߽� ȸ�� ������
        SpecialRecovery = player.Model.RegainMana[player.Model.ChargeStep] ;
        SpecialRecovery += SpecialRecovery * player.Model.RegainAdditiveMana / 100; 

        AddThrowAdditional(throwAdditionals, player);
    }
    public void Init(PlayerController player, int addionalDamage,List<ThrowAdditional> throwAdditionals)
    {
        _player = player;
        PlayerDamage = addionalDamage;
        Radius = player.Model.BoomRadius;
        // ���߽� ȸ�� ������
        SpecialRecovery = player.Model.RegainMana[player.Model.ChargeStep];
        SpecialRecovery += SpecialRecovery * player.Model.RegainAdditiveMana / 100;

        AddThrowAdditional(throwAdditionals, player);
    }
    #endregion
    public void Shoot(float throwPower)
    {
        Rb.AddForce(transform.forward * throwPower, ForceMode.Impulse);
    }
    /// <summary>
    /// ���� �߰� ȿ�� �ߵ�
    /// </summary>
    public void EnterThrowAdditional()
    {
        foreach (ThrowAdditional throwAdditional in ThrowAdditionals)
        {
            throwAdditional.Enter();
        }
    }
    public void ExitThrowAdditional()
    {
        foreach (ThrowAdditional throwAdditional in ThrowAdditionals)
        {
            throwAdditional.Exit();
        }
    }

    public void UpdateThrowAdditional()
    {
        if (CanAttack == false)
            return;

        foreach (ThrowAdditional throwAdditional in ThrowAdditionals)
        {
            throwAdditional.Update();
        }
    }

    public void FixedUpdateThrowAdditional()
    {
        if (CanAttack == false)
            return;

        foreach (ThrowAdditional throwAdditional in ThrowAdditionals)
        {
            throwAdditional.FixedUpdate();
        }
    }
    public void TriggerThrowAddtional()
    {
        if (CanAttack == false)
            return;
     
        foreach (ThrowAdditional throwAdditional in ThrowAdditionals)
        {
            throwAdditional.Trigger();
        }
    }
    /// <summary>
    /// Ÿ�� ����
    /// </summary>
    protected void HitTarget()
    {
        if (CanAttack == false)
            return;

        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, Radius, _player.OverLapColliders, 1 << Layer.Monster);

        for (int i = 0; i < hitCount; i++)
        {
            int finalDamage = _player.GetFinalDamage(Damage, DamageMultyPlier, out bool isCritical);
            // ����� �ֱ�
            int hitDamage = _player.Battle.TargetAttackWithDebuff(_player.OverLapColliders[i], finalDamage, true, isCritical);

            if (KnockBackDistance > 0)
                _player.DoKnockBack(_player.OverLapColliders[i].transform, transform.forward, KnockBackDistance);
        }

        // �÷��̾� Ư������ �ڿ� ȹ��
        _player.Model.CurMana += SpecialRecovery;
        DestroyObject();
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }


    protected void AddThrowAdditional(List<ThrowAdditional> throwAdditionals, PlayerController player)
    {
        foreach (ThrowAdditional throwAdditional in throwAdditionals)
        {
            int index = ThrowAdditionals.FindIndex(origin => origin.Origin.Equals(throwAdditional.Origin));
            if (index >= ThrowAdditionals.Count)
                return;

            if (index == -1)
            {
                ThrowAdditional instance = Instantiate(throwAdditional);
                instance.Origin = throwAdditional.Origin;
                instance.Init(player, throwAdditional, this);
                ThrowAdditionals.Add(instance);
            }
        }
    }
    private void RemoveThrowAddtional(ThrowAdditional throwAdditional)
    {
        throwAdditional.Exit();
        ThrowAdditionals.Remove(throwAdditional);
        Destroy(throwAdditional);
    }
    private void ClearThrowAddtional()
    {
        for(int i = ThrowAdditionals.Count -1 ; i >= 0; i--)
        {
            RemoveThrowAddtional(ThrowAdditionals[i]);
        }
    }

    protected void DestroyObject()
    {
        Destroy(gameObject);
    }
}

[System.Serializable]
public class ThrowObjectData
{
    public int ID;
    public string Name;
}
