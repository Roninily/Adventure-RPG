using UnityEngine;
using System.Collections;

public class TestRunner : MonoBehaviour
{
    [Header("测试设置")]
    public bool runTestsOnStart = true;
    public DebugHelper debugHelper;

    void Start()
    {
        if (runTestsOnStart)
        {
            StartCoroutine(RunAllTests());
        }
    }

    IEnumerator RunAllTests()
    {
        Debug.Log("=== 开始运行测试 ===");

        // 等待一帧确保所有对象都已初始化
        yield return null;

        // 测试1: 检查场景对象
        yield return StartCoroutine(TestSceneObjects());

        // 测试2: 检查脚本组件
        yield return StartCoroutine(TestScriptComponents());

        // 测试3: 检查UI引用
        yield return StartCoroutine(TestUIReferences());

        // 测试4: 测试玩家移动
        yield return StartCoroutine(TestPlayerMovement());

        // 测试5: 测试对话系统
        yield return StartCoroutine(TestDialogueSystem());

        Debug.Log("=== 所有测试完成 ===");
    }

    IEnumerator TestSceneObjects()
    {
        Debug.Log("测试1: 检查场景对象");

        // 检查玩家
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Debug.Log("✅ 玩家对象存在: " + player.name);
        }
        else
        {
            Debug.LogError("❌ 未找到玩家对象！");
        }

        // 检查敌人
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log($"✅ 找到 {enemies.Length} 个敌人对象");

        // 检查对话系统
        DialogueSystem dialogueSystem = FindObjectOfType<DialogueSystem>();
        if (dialogueSystem != null)
        {
            Debug.Log("✅ 对话系统对象存在: " + dialogueSystem.gameObject.name);
        }
        else
        {
            Debug.LogError("❌ 未找到对话系统对象！");
        }

        yield return null;
    }

    IEnumerator TestScriptComponents()
    {
        Debug.Log("测试2: 检查脚本组件");

        // 测试玩家脚本
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                Debug.Log("✅ 玩家移动脚本存在");
            }

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("✅ 玩家生命值脚本存在");
            }
        }

        // 测试敌人脚本
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Enemy_Movement enemyMovement = enemy.GetComponent<Enemy_Movement>();
            if (enemyMovement != null)
            {
                Debug.Log($"✅ 敌人 {enemy.name} 移动脚本存在");
                if (enemyMovement.player == null)
                {
                    Debug.LogWarning($"⚠️ 敌人 {enemy.name} 的玩家引用为空 - 需要在Inspector中赋值");
                }
            }
        }

        yield return null;
    }

    IEnumerator TestUIReferences()
    {
        Debug.Log("测试3: 检查UI引用");

        DialogueSystem dialogueSystem = FindObjectOfType<DialogueSystem>();
        if (dialogueSystem != null)
        {
            if (dialogueSystem.dialoguePanel == null)
            {
                Debug.LogError("❌ 对话面板引用为空！");
            }
            else
            {
                Debug.Log("✅ 对话面板引用正常");
            }

            if (dialogueSystem.contentText == null)
            {
                Debug.LogError("❌ 对话文本组件引用为空！");
            }
            else
            {
                Debug.Log("✅ 对话文本组件引用正常");
            }

            if (dialogueSystem.nextBtn == null)
            {
                Debug.LogWarning("⚠️ 下一步按钮引用为空");
            }
            else
            {
                Debug.Log("✅ 下一步按钮引用正常");
            }
        }

        yield return null;
    }

    IEnumerator TestPlayerMovement()
    {
        Debug.Log("测试4: 测试玩家移动");

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                // 检查必要的组件
                if (playerMovement.rb == null)
                {
                    Debug.LogError("❌ 玩家Rigidbody2D组件未赋值！");
                }
                else
                {
                    Debug.Log("✅ 玩家Rigidbody2D组件正常");
                }

                if (playerMovement.anim == null)
                {
                    Debug.LogError("❌ 玩家Animator组件未赋值！");
                }
                else
                {
                    Debug.Log("✅ 玩家Animator组件正常");
                }
            }
        }

        yield return null;
    }

    IEnumerator TestDialogueSystem()
    {
        Debug.Log("测试5: 测试对话系统");

        DialogueSystem dialogueSystem = FindObjectOfType<DialogueSystem>();
        if (dialogueSystem != null)
        {
            // 检查对话内容
            if (dialogueSystem.dialogueList == null || dialogueSystem.dialogueList.Count == 0)
            {
                Debug.LogWarning("⚠️ 对话内容列表为空 - 需要在Inspector中添加对话内容");
            }
            else
            {
                Debug.Log($"✅ 对话内容列表包含 {dialogueSystem.dialogueList.Count} 条对话");
            }

            // 测试打开对话
            dialogueSystem.OpenDialogue();
            Debug.Log("✅ 对话系统测试完成");
        }

        yield return null;
    }

    // 公共测试方法
    public void RunQuickTest()
    {
        StartCoroutine(QuickTest());
    }

    IEnumerator QuickTest()
    {
        Debug.Log("=== 快速测试 ===");

        // 检查玩家
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Debug.Log("✅ 玩家存在");
        }
        else
        {
            Debug.LogError("❌ 玩家不存在！");
        }

        // 检查敌人
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log($"敌人数量: {enemies.Length}");

        // 检查对话系统
        DialogueSystem dialogueSystem = FindObjectOfType<DialogueSystem>();
        if (dialogueSystem != null)
        {
            Debug.Log("✅ 对话系统存在");
        }
        else
        {
            Debug.LogError("❌ 对话系统不存在！");
        }

        yield return null;
    }
}