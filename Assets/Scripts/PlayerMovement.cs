using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("移动设置")]
    public float speed = 5f;
    public Rigidbody2D rb;
    public Animator anim;

    [Header("交互设置")]
    public GameObject interactTip;
    public DialogueSystem dialogueSystem; // 直接引用

    private bool canInteract = false;
    private Vector2 movement;

    void Update()
    {
        // 优化1：获取输入的代码放在 Update 中，防止按键响应漏帧
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (anim != null)
        {
            anim.SetFloat("horizontal", Mathf.Abs(movement.x));
            anim.SetFloat("vertical", Mathf.Abs(movement.y));
        }

        // 优化2：检测E键
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("?? 玩家按下了 E 键"); // 可以在 Console 里看到这句话

            if (canInteract)
            {
                Debug.Log("? 在交互范围内，准备调用对话系统");

                if (dialogueSystem != null)
                {
                    // 优化3：如果面板没打开，才去打开它（防止狂按E键导致对话一直重置）
                    if (!dialogueSystem.dialoguePanel.activeSelf)
                    {
                        dialogueSystem.OpenDialogue();

                        // 对话时可以把头顶的提示先隐藏掉，看起来更清爽
                        if (interactTip != null) interactTip.SetActive(false);
                    }
                }
                else
                {
                    Debug.LogError("? 错误：Player 脚本上的 DialogueSystem 变量为空，请拖拽赋值！");
                }
            }
        }
    }

    void FixedUpdate()
    {
        // 物理移动保持在 FixedUpdate 中
        rb.velocity = movement.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dialogue"))
        {
            canInteract = true;
            if (interactTip != null) interactTip.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Dialogue"))
        {
            canInteract = false;
            if (interactTip != null) interactTip.SetActive(false);
        }
    }
}