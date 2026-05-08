using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    [Header("移动设置")]
    public float speed = 4f; // 敌人移动速度
    private Rigidbody2D rb;
    public Transform player;

    [Header("追逐范围")]
    public float chaseRange = 3f; // 触发器的半径，和你场景里的圆形Collider大小保持一致
    private bool isPlayerInRange = false; // 玩家是否在追逐范围内

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // 确保敌人不会被重力影响
    }

    // 触发器进入：玩家进入范围，开始追逐
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 只对带Player标签的物体生效
        {
            isPlayerInRange = true;
        }
    }

    // 触发器停留：玩家在范围内，保持追逐状态
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    // 触发器退出：玩家离开范围，停止追逐
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            rb.velocity = Vector2.zero; // 直接把速度清零，防止滑出去还在追
        }
    }

    // 物理移动逻辑放在FixedUpdate里，保证稳定
    void FixedUpdate()
    {
        // 玩家不在范围内，直接不执行移动逻辑
        if (!isPlayerInRange || player == null)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // 玩家在范围内，计算方向并移动
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;

        // 可选：让敌人面向玩家（左右翻转）
        if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
