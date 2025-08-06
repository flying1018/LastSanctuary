using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class LoadingScene : MonoBehaviour
{
    public string nextSceneName;
    [SerializeField] private Image progressBar;

    private void Start()
    {
        StartCoroutine(LoadSceneAsync_Coroutine());
    }

    IEnumerator LoadSceneAsync_Coroutine()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextSceneName);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);

            if (progressBar != null)
                progressBar.fillAmount = progress;

            if (op.progress >= 0.9f)
            {
                // 추가 연출있다면 여기에 추가하면됨
                op.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}

public class SceneLoader : MonoBehaviour
{
    public static string nextSceneName;
    public static void LoadScene(string sceneName)
    {
        nextSceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
}