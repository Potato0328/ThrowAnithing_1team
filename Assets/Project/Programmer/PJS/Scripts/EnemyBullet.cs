using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public enum State { Basic, Poision }
    [SerializeField] Rigidbody rigid;
    [SerializeField] State curState;

    [HideInInspector] public BattleSystem Battle;
    public GameObject poisonPlate;  // �� ����
    public Transform target;    // �÷��̾�
    public int Atk;     // ���ݷ�

    private float speed;    // ��ô ���ǵ�

    public float Speed { set { speed = value; } }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // TODO : �÷��̾� �ٶ󺸴°� ���� ��� ���� �� ��ü
        //transform.LookAt(target.position + new Vector3(0, 1, 0));
    }

    private void FixedUpdate()
    {
        rigid.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == Tag.Player)
        {
            Battle.TargetAttack(other.transform, Atk, true);
            Destroy(gameObject);
        }

        if (curState == State.Poision)
        {
            Debug.Log("����");
            Instantiate(poisonPlate, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
