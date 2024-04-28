using UnityEngine;

public class PlayerCheckGround : MonoBehaviour
{
    public bool isGround;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            isGround = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            isGround = false;
        }
    }
}
