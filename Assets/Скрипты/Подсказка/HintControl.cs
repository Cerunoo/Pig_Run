using System;
using UnityEngine;
using UnityEngine.UI;

public class HintControl : MonoBehaviour
{
    public KeyCode keycodeEvent;
    public Animator anim;

    public event Action KeycodeEventPressed;
    bool isEventStarted;

    public Image blackoutPressed;
    public float fillRatePressed;
    float fillPressed;

    bool isWork = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isWork)
        {
            anim.gameObject.SetActive(true);
            anim.SetBool("show", true);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetKey(keycodeEvent) && !isEventStarted && isWork)
        {
            anim.SetBool("pressed", true);
            if (fillPressed + fillRatePressed < 100)
            {
                fillPressed += fillRatePressed;
            }
            else
            {
                fillPressed = 100;
            }
        }
        if (!Input.GetKey(keycodeEvent) && !isEventStarted && isWork)
        {
            anim.SetBool("pressed", false);
            if (fillPressed - (fillRatePressed * 1.5f) > 0)
            {
                fillPressed -= fillRatePressed * 1.5f;
            }
            else
            {
                fillPressed = 0;
            }
        }

        blackoutPressed.fillAmount = fillPressed / 100;
        float hintSize = 1 - fillPressed / 650;
        anim.gameObject.GetComponent<RectTransform>().localScale = new Vector2(hintSize, hintSize);

        if (fillPressed >= 100 && !isEventStarted && isWork)
        {
            anim.SetBool("show", false);
            anim.SetBool("pressed", false);

            isEventStarted = true;
            KeycodeEventPressed?.Invoke();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && isWork)
        {
            anim.SetBool("show", false);
            anim.SetBool("pressed", false);
        }
    }

    public void SwitchStateWork()
    {
        isWork = !isWork;

        if (isWork == false)
        {
            anim.SetBool("show", false);
            anim.SetBool("pressed", false);
        }
    }
}