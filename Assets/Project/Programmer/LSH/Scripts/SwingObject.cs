using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingObject : MonoBehaviour
{
    [SerializeField] float swingSpeed = 2.0f;  // ������ ��鸮�� �ӵ�
    [SerializeField] float swingAngle = 30.0f; // ������ �ִ� ��鸮�� ����

    void Update()
    {
        // swing
        float angle = Mathf.Sin(Time.time * swingSpeed) * swingAngle;

        Debug.Log(angle);

        // ���� rotation�� ��ȭ
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 
                                              transform.rotation.eulerAngles.y, 
                                              angle);


    }
}
