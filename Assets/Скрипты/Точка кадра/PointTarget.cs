using UnityEngine;

public class PointTarget : MonoBehaviour
{
    public Transform target;
    public bool followX = true;
    public bool followY = true;

    Vector2 startPos;

    void Start ()
    {
        startPos = transform.position;
    }

    void Update ()
    {
        float posX = followX ? target.position.x + startPos.x : startPos.x;
        float posY = followY ? target.position.y + startPos.y : startPos.y;
        transform.position = new Vector2(posX, posY);
    }
}
