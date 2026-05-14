using UnityEngine;
using System.Collections.Generic;

public class SceneSetup : MonoBehaviour
{
    [Header("场景设置")]
    public bool autoSetupOnStart = true;
    
    [Header("对象引用")]
    public GameObject player;
    public GameObject[] enemies;
    public DialogueSystem dialogueSystem;
    
    [Header("标签设置")]
    public string playerTag = "Player";
    public string enemyTag = "Enemy";
    public string dialogueTag = "Dialogue";

    void Start()
    {
        if (autoSetupOnStart)
        {
            SetupScene();
        }
    }

    public void SetupScene()
    {
        Debug.Log("=== 开始场景设置 ===");

        // 1. 设置玩家标签
        SetupPlayer();

        // 2. 设置敌人标签
        SetupEnemies();

        // 3. 设置对话系统
        SetupDialogueSystem();

        // 4. 检查组件引用
        CheckComponentReferences();

        Debug.Log("=== 场景设置完成 ===");
    }

    void SetupPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (player != null)
        {
            player.tag = playerTag;
            Debug.Log($"✅ 玩家标签设置为: {playerTag}");

            // 确保玩家有必要的组件
            EnsurePlayerComponents();
        }
        else
        {
            Debug.LogWarning("⚠️ 未找到玩家对象");
        }
    }

    void SetupEnemies()
    {
        if (enemies == null || enemies.Length == 0)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }

        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.tag = enemyTag;
                Debug.Log($"✅ 敌人 {enemy.name} 标签设置为: {enemyTag}");

                // 确保敌人有必要的组件
                EnsureEnemyComponents(enemy);
            }
        }
    }

    void SetupDialogueSystem()
    {
        if (dialogueSystem == null)
        {
            dialogueSystem = FindObjectOfType<DialogueSystem>();
        }

        if (dialogueSystem != null)
        {
            dialogueSystem.gameObject.tag = dialogueTag;
            Debug.Log($"✅ 对话系统标签设置为: {dialogueTag}");
        }
        else
        {
            Debug.LogWarning("⚠️ 未找到对话系统对象");
        }
    }

    void EnsurePlayerComponents()
    {
        if (player != null)
        {
            // 检查Rigidbody2D
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = player.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0;
                Debug.Log("✅ 为玩家添加Rigidbody2D组件");
            }

            // 检查Collider2D
            Collider2D collider = player.GetComponent<Collider2D>();
            if (collider == null)
            {
                BoxCollider2D boxCollider = player.AddComponent<BoxCollider2D>();
                boxCollider.isTrigger = false;
                Debug.Log("✅ 为玩家添加BoxCollider2D组件");
            }

            // 检查Animator
            Animator animator = player.GetComponent<Animator>();
            if (animator == null)
            {
                animator = player.AddComponent<Animator>();
                Debug.Log("✅ 为玩家添加Animator组件");
            }

            // 检查PlayerMovement
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement == null)
            {
                playerMovement = player.AddComponent<PlayerMovement>();
                Debug.Log("✅ 为玩家添加PlayerMovement组件");
            }

            // 检查PlayerHealth
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth == null)
            {
                playerHealth = player.AddComponent<PlayerHealth>();
                playerHealth.currentHealth = 3;
                playerHealth.maxHealth = 3;
                Debug.Log("✅ 为玩家添加PlayerHealth组件");
            }
        }
    }

    void EnsureEnemyComponents(GameObject enemy)
    {
        if (enemy != null)
        {
            // 检查Rigidbody2D
            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = enemy.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0;
                Debug.Log($"✅ 为敌人 {enemy.name} 添加Rigidbody2D组件");
            }

            // 检查Collider2D
            Collider2D collider = enemy.GetComponent<Collider2D>();
            if (collider == null)
            {
                CircleCollider2D circleCollider = enemy.AddComponent<CircleCollider2D>();
                circleCollider.isTrigger = true;
                Debug.Log($"✅ 为敌人 {enemy.name} 添加CircleCollider2D组件");
            }

            // 检查Enemy_Movement
            Enemy_Movement enemyMovement = enemy.GetComponent<Enemy_Movement>();
            if (enemyMovement == null)
            {
                enemyMovement = enemy.AddComponent<Enemy_Movement>();
                Debug.Log($"✅ 为敌人 {enemy.name} 添加Enemy_Movement组件");
            }

            // 检查Enemy_Combat
            Enemy_Combat enemyCombat = enemy.GetComponent<Enemy_Combat>();
            if (enemyCombat == null)
            {
                enemyCombat = enemy.AddComponent<Enemy_Combat>();
                Debug.Log($"✅ 为敌人 {enemy.name} 添加Enemy_Combat组件");
            }
        }
    }

    void CheckComponentReferences()
    {
        Debug.Log("=== 检查组件引用 ===");

        // 检查玩家脚本引用
        if (player != null)
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                if (playerMovement.rb == null)
                {
                    Debug.LogWarning("⚠️ 玩家移动脚本中的Rigidbody2D引用为空 - 请在Inspector中手动赋值");
                }

                if (playerMovement.anim == null)
                {
                    Debug.LogWarning("⚠️ 玩家移动脚本中的Animator引用为空 - 请在Inspector中手动赋值");
                }
            }
        }

        // 检查敌人脚本引用
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                Enemy_Movement enemyMovement = enemy.GetComponent<Enemy_Movement>();
                if (enemyMovement != null)
                {
                    if (enemyMovement.player == null)
                    {
                        Debug.LogWarning($"⚠️ 敌人 {enemy.name} 的玩家引用为空 - 请在Inspector中手动赋值");
                    }
                }
            }
        }

        // 检查对话系统引用
        if (dialogueSystem != null)
        {
            if (dialogueSystem.dialoguePanel == null)
            {
                Debug.LogWarning("⚠️ 对话系统中的对话面板引用为空 - 请在Inspector中手动赋值");
            }

            if (dialogueSystem.contentText == null)
            {
                Debug.LogWarning("⚠️ 对话系统中的文本组件引用为空 - 请在Inspector中手动赋值");
            }

            if (dialogueSystem.nextBtn == null)
            {
                Debug.LogWarning("⚠️ 对话系统中的下一步按钮引用为空 - 请在Inspector中手动赋值");
            }
        }
    }

    // 公共方法供其他脚本调用
    public void ForceSetupScene()
    {
        SetupScene();
    }

    public void LogSceneStatus()
    {
        Debug.Log("=== 场景状态 ===");
        Debug.Log($"玩家对象: {(player != null ? "存在" : "不存在")}");
        Debug.Log($"敌人数量: {enemies?.Length ?? 0}");
        Debug.Log($"对话系统: {(dialogueSystem != null ? "存在" : "不存在")}");
    }
}