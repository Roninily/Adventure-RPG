using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections; // 必须引入这个命名空间才能使用协程
using System.Collections.Generic;

public class DialogueSystem : MonoBehaviour
{
    [Header("对话面板")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI contentText;
    public Button nextBtn;

    [Header("对话内容")]
    public List<string> dialogueList;

    [Header("打字机设置")]
    public float typeSpeed = 0.05f; // 每个字弹出的间隔时间

    private int currentIndex = 0;
    private bool isTyping = false; // 记录当前是否正在打字
    private Coroutine typingCoroutine; // 记录当前的打字协程

    void Awake()
    {
        if (nextBtn != null)
        {
            nextBtn.onClick.RemoveAllListeners();
            nextBtn.onClick.AddListener(NextPage);
        }
    }

    public void OpenDialogue()
    {
        if (dialogueList == null || dialogueList.Count == 0) return;

        currentIndex = 0;
        dialoguePanel.SetActive(true);
        ShowPage(currentIndex);
    }

    public void NextPage()
    {
        // 核心逻辑优化：如果正在打字，点击按钮时直接显示全句
        if (isTyping)
        {
            // 停止打字协程
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            // 直接把当前句子的完整内容显示出来
            contentText.text = dialogueList[currentIndex];
            isTyping = false;
            return; // 结束当前点击逻辑，等待玩家下一次点击才进入下一页
        }

        // 如果没有在打字（已经显示完整），则进入下一页
        currentIndex++;

        if (currentIndex >= dialogueList.Count)
        {
            dialoguePanel.SetActive(false);
            return;
        }

        ShowPage(currentIndex);
    }

    void ShowPage(int index)
    {
        // 开始新一页前，确保之前的协程被停止
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        // 开启打字机协程
        typingCoroutine = StartCoroutine(TypeText(dialogueList[index]));
    }

    // --- 打字机协程 ---
    IEnumerator TypeText(string textToType)
    {
        isTyping = true;
        contentText.text = ""; // 清空当前显示的文本

        // 将字符串拆成一个个字符，逐个显示
        foreach (char c in textToType.ToCharArray())
        {
            contentText.text += c; // 拼接上新的字
            yield return new WaitForSeconds(typeSpeed); // 等待设定的时间
        }

        isTyping = false; // 打字完成
    }
}