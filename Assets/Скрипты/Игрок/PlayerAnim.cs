using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animator anim;
    PlayerMovement movement;

    void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (movement.checkGround.isGround)
        {
            if (Input.GetKey(KeyCode.W) && movement.isWork)
            {
                anim.SetTrigger("takeOff");
            }
            anim.SetBool("jump", false);
        }
        else
        {
            anim.SetBool("jump", true);
        }

        if (movement.horizontal == 0)
        {
            anim.SetBool("run", false);
        }
        else
        {
            anim.SetBool("run", true);
        }
    }
}
