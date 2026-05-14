using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialoguePanel;
    public DialogueSystem dialogueSystem;
    public GameObject interactTip;

    private bool isPlayerInRange = false;
    private bool dialogueWasOpen = false;

    private void Start()
    {
        if (interactTip != null)
            interactTip.SetActive(false);
    }

    private void Update()
    {
        if (dialoguePanel == null) return;

        bool dialogueOpen = dialoguePanel.activeSelf;

        // 对话刚关闭 + 玩家还在范围内 → 重新显示提示
        if (dialogueWasOpen && !dialogueOpen && isPlayerInRange)
        {
            if (interactTip != null)
                interactTip.SetActive(true);
        }

        dialogueWasOpen = dialogueOpen;

        // 按 E 键交互
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInRange)
        {
            if (dialogueSystem != null && !dialoguePanel.activeSelf)
            {
                dialogueSystem.OpenDialogue();
                if (interactTip != null)
                    interactTip.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (interactTip != null)
                interactTip.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (interactTip != null)
                interactTip.SetActive(false);
        }
    }
}
