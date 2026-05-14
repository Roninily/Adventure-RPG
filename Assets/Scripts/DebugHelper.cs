using UnityEngine;
using System.Collections.Generic;

public class DebugHelper : MonoBehaviour
{
    [Header("调试设置")]
    public bool showDebugInfo = true;
    public bool showPlayerInfo = true;
    public bool showEnemyInfo = true;
    public bool showDialogueInfo = true;

    [Header("场景对象引用")]
    public GameObject player;
    public GameObject[] enemies;
    public DialogueSystem dialogueSystem;

    void Start()
    {
        if (showDebugInfo)
        {
            Debug.Log("=== 调试助手已启动 ===");
            CheckSceneObjects();
            CheckScripts();
            CheckUIReferences();
        }
    }

    void Update()
    {
        if (showDebugInfo)
        {
            if (showPlayerInfo && player != null)
            {
                Debug.Log($"玩家位置: {player.transform.position}");
            }

            if (showEnemyInfo)
            {
                foreach (var enemy in enemies)
                {
                    if (enemy != null)
                    {
                        Debug.Log($"敌人位置: {enemy.transform.position}");
                    }
                }
            }
        }
    }

    void CheckSceneObjects()
    {
        Debug.Log("=== 场景对象检查 ===");

        // 检查玩家
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogError("❌ 未找到标签为 'Player' 的游戏对象！");
            }
            else
            {
                Debug.Log("✅ 找到玩家对象: " + player.name);
            }
        }

        // 检查敌人
        if (enemies == null || enemies.Length == 0)
        {
            GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemyObjects.Length > 0)
            {
                enemies = enemyObjects;
                Debug.Log($"✅ 找到 {enemies.Length} 个敌人对象");
            }
            else
            {
                Debug.LogWarning("⚠️ 未找到标签为 'Enemy' 的游戏对象");
            }
        }

        // 检查对话系统
        if (dialogueSystem == null)
        {
            DialogueSystem foundDialogue = FindObjectOfType<DialogueSystem>();
            if (foundDialogue != null)
            {
                dialogueSystem = foundDialogue;
                Debug.Log("✅ 找到对话系统对象: " + dialogueSystem.gameObject.name);
            }
            else
            {
                Debug.LogError("❌ 未找到 DialogueSystem 组件！");
            }
        }
    }

    void CheckScripts()
    {
        Debug.Log("=== 脚本组件检查 ===");

        // 检查玩家脚本
        if (player != null)
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                Debug.Log("✅ 玩家移动脚本已加载");
            }
            else
            {
                Debug.LogError("❌ 玩家对象缺少 PlayerMovement 脚本！");
            }

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("✅ 玩家生命值脚本已加载");
            }
            else
            {
                Debug.LogError("❌ 玩家对象缺少 PlayerHealth 脚本！");
            }
        }

        // 检查敌人脚本
        if (enemies != null)
        {
            foreach (var enemy in enemies)
            {
                if (enemy != null)
                {
                    Enemy_Movement enemyMovement = enemy.GetComponent<Enemy_Movement>();
                    if (enemyMovement != null)
                    {
                        Debug.Log($"✅ 敌人 {enemy.name} 移动脚本已加载");
                        if (enemyMovement.player == null)
                        {
                            Debug.LogWarning($"⚠️ 敌人 {enemy.name} 的玩家引用为空！");
                        }
                    }

                    Enemy_Combat enemyCombat = enemy.GetComponent<Enemy_Combat>();
                    if (enemyCombat != null)
                    {
                        Debug.Log($"✅ 敌人 {enemy.name} 战斗脚本已加载");
                    }
                }
            }
        }
    }

    void CheckUIReferences()
    {
        Debug.Log("=== UI 引用检查 ===");

        if (dialogueSystem != null)
        {
            if (dialogueSystem.dialoguePanel == null)
            {
                Debug.LogError("❌ 对话系统中的对话面板引用为空！");
            }
            else
            {
                Debug.Log("✅ 对话面板引用正常");
            }

            if (dialogueSystem.contentText == null)
            {
                Debug.LogError("❌ 对话系统中的文本组件引用为空！");
            }
            else
            {
                Debug.Log("✅ 对话文本组件引用正常");
            }

            if (dialogueSystem.nextBtn == null)
            {
                Debug.LogWarning("⚠️ 对话系统中的下一步按钮引用为空");
            }
            else
            {
                Debug.Log("✅ 下一步按钮引用正常");
            }
        }
    }

    // 公共方法供其他脚本调用
    public void LogPlayerPosition()
    {
        if (player != null)
        {
            Debug.Log($"玩家当前位置: {player.transform.position}");
        }
    }

    public void LogEnemyPositions()
    {
        if (enemies != null)
        {
            foreach (var enemy in enemies)
            {
                if (enemy != null)
                {
                    Debug.Log($"敌人 {enemy.name} 位置: {enemy.transform.position}");
                }
            }
        }
    }

    public void TestDialogueSystem()
    {
        if (dialogueSystem != null)
        {
            Debug.Log("测试对话系统...");
            dialogueSystem.OpenDialogue();
        }
        else
        {
            Debug.LogError("无法测试对话系统 - 引用为空！");
        }
    }
}