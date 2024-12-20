using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Test_PlayerController : MonoBehaviour
{
    [SerializeField] float speed;

    Vector3 m_Movement;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal"); // ���� �Է�
        float z = Input.GetAxis("Vertical"); // ���� �Է�

        m_Movement = new Vector3(x, 0, z).normalized; //����ȭ

        transform.position += m_Movement * speed * Time.deltaTime;
    }
}
