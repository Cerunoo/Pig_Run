using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnPrefabs;

    public Transform posX;
    public Transform minPosY;
    public Transform maxPosY;

    public float minTimeSpawn;
    public float maxTimeSpawn;

    public float minSize;
    public float maxSize;

    public float minTran;
    public float maxTran;

    void Start()
    {
        SpawnItem();
        StartCoroutine(spawnObject());
    }

    IEnumerator spawnObject()
    {
        yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
        SpawnItem();
        StartCoroutine(spawnObject());
    }

    void SpawnItem()
    {
        GameObject prefab = spawnPrefabs[Random.Range(0, spawnPrefabs.Length)];
        Vector2 pos = new Vector2(posX.position.x, Random.Range(minPosY.position.y, maxPosY.position.y));
        float size = Random.Range(minSize, maxSize) * prefab.transform.localScale.x;
        int sortOrder = 150; // *
        int flip = Random.Range(0, 2);
        float transparent = Random.Range(minTran, maxTran);

        GameObject item = Instantiate(prefab, pos, Quaternion.identity);
        item.transform.localScale = new Vector2(size, size);
        item.transform.SetParent(transform);

        SpriteRenderer sr = item.GetComponent<SpriteRenderer>();
        sr.sortingOrder = sortOrder;
        sr.flipX = (flip == 1) ? true : false;
        sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, transparent);
    }
}