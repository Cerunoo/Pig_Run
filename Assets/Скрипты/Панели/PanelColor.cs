using UnityEngine;
using UnityEngine.UI;

public class PanelColor : MonoBehaviour
{
    public Panel panel;
    public Color panelColor;

    void OnEnable()
    {
        panel.gameObject.GetComponent<Image>().color = panelColor;
        panel.headerFill.color = panelColor;
    }
}
