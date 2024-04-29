using UnityEngine;

public class DialogueAnim : MonoBehaviour
{
    public Animator startAnim;
    public DialogueManager dm;

    void OnTriggerEnter2D(Collider2D other)
    {
        startAnim.SetBool("startOpen", true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        startAnim.SetBool("startOpen", false);
        dm.EndDialogue();
    }
}