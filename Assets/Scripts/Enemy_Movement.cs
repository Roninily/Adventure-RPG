using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    [Header("�ƶ�����")]
    public float speed = 4f; // �����ƶ��ٶ�
    private Rigidbody2D rb;
    public Transform player;

    [Header("׷��Χ")]
    public float chaseRange = 3f; // �������İ뾶�����㳡�����Բ��Collider��С����һ��
    private bool isPlayerInRange = false; // ����Ƿ���׷��Χ��

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // ȷ�����˲��ᱻ����Ӱ��
    }

    // ���������룺��ҽ��뷶Χ����ʼ׷��
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ֻ�Դ�Player��ǩ��������Ч
        {
            isPlayerInRange = true;
        }
    }

    // ������ͣ��������ڷ�Χ�ڣ�����׷��״̬
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    // �������˳�������뿪��Χ��ֹͣ׷��
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            rb.velocity = Vector2.zero; // ֱ�Ӱ��ٶ����㣬��ֹ����ȥ����׷
        }
    }

    // �����ƶ��߼�����FixedUpdate���֤�ȶ�
    void FixedUpdate()
    {
        // ��Ҳ��ڷ�Χ�ڣ�ֱ�Ӳ�ִ���ƶ��߼�
        if (!isPlayerInRange || player == null)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // ����ڷ�Χ�ڣ����㷽���ƶ�
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;

        // ��ѡ���õ���������ң����ҷ�ת��
        if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
