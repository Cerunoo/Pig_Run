using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncLoading : MonoBehaviour
{
    public Image progressSlider;
    public Text progressText;

    public GameObject buttonStartScene;

    [RuntimeInitializeOnLoadMethod]
    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Screen.fullScreen = true;
            StartCoroutine(LoadAsync(1));
        }
    }

    AsyncOperation operation;
    public IEnumerator LoadAsync(int index, bool waitStart = false)
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Button[] allButtons = FindObjectsOfType<Button>(true);
            for (int i = 0; i < allButtons.Length; i++)
            {
                allButtons[i].interactable = false;
            }
            gameObject.GetComponent<Animator>().SetBool("show", true);
        }

        yield return new WaitForSeconds(1);
        operation = SceneManager.LoadSceneAsync(index);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            progressSlider.fillAmount = Mathf.Clamp01(operation.progress / 0.9f);
            progressText.text = (progressSlider.fillAmount * 100).ToString("F0") + "%";
            yield return null;
            if (progressSlider.fillAmount == 1)
            {
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    buttonStartScene.SetActive(true);
                }
                else if (!waitStart)
                {
                    operation.allowSceneActivation = true;
                }
            }
        }
    }

    public void StartScene()
    {
        operation.allowSceneActivation = true;
    }
}
