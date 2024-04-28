using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Panel : MonoBehaviour
{
    public Button[] lockedButtons;

    public Image headerFill;
    public Text headerText;

    public void OpenPanel(GameObject content)
    {
        for (int i = 0; i < lockedButtons.Length; i++)
        {
            lockedButtons[i].interactable = false;
        }
        gameObject.GetComponent<Animator>().SetBool("open", true);
        content.SetActive(true);
        headerText.text = content.name;
    }

    public void ClosePanel(GameObject content)
    {
        gameObject.GetComponent<Animator>().SetBool("open", false);
        StartCoroutine(waitEnd());

        IEnumerator waitEnd()
        {
            yield return new WaitForSeconds(gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            for (int i = 0; i < lockedButtons.Length; i++)
            {
                lockedButtons[i].interactable = true;
            }
            content.SetActive(false);
        }
    }
}