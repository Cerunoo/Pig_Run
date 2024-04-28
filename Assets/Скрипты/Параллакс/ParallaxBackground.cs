using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxBackground : MonoBehaviour
{
    public ParallaxCamera parallaxCamera;
    List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    void Start()
    {
        if (parallaxCamera == null)
        {
            parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();
        }
        if (parallaxCamera != null)
        {
            parallaxCamera.onCameraTranslate += Move;
        }
        SetLayers();
    }

    void SetLayers()
    {
        parallaxLayers.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();
            if (layer != null) { parallaxLayers.Add(layer); }
            FindChildParallaxLayers(transform.GetChild(i));
        }
    }

    void FindChildParallaxLayers(Transform parent)
    {
        foreach (Transform child in parent)
        {
            ParallaxLayer layer = child.GetComponent<ParallaxLayer>();
            if (layer != null) { parallaxLayers.Add(layer); }
            else { FindChildParallaxLayers(child); }
        }
    }

    public void AddParallaxLayer(ParallaxLayer layer)
    {
        parallaxLayers.Add(layer);
    }

    public void OffAnim()
    {
        StartCoroutine(WaitFix());
        IEnumerator WaitFix()
        {
            yield return new WaitForSeconds(1.5f);
            gameObject.GetComponent<Animator>().enabled = false;
        }
    }

    void Move(float delta)
    {
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.Move(delta);
        }
    }
}