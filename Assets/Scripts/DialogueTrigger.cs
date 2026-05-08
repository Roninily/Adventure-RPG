using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialoguePanel; 

    public void TriggerDialogue()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(!dialoguePanel.activeSelf);
        }
    }
}