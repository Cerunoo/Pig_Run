using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    float speed;

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}