using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ScenesLoader : MonoBehaviour
{
    [SerializeField] private typeScript _typeScript;
    [SerializeField] private bool _unloadThisScene;

    [SerializeField] public string[] scenesToLoad;

    [SerializeField] private bool _loadingBar;
    /// <summary>
    /// UI-экран загрузки
    /// </summary>
    public GameObject loadingScreen;
    public Image progressBar;
    public TextMeshPro progressText;

    [SerializeField] private bool _loadingSprite;
    [SerializeField] private Image LoadingImageObject;
    [SerializeField] private Sprite[] LoadingSprites;

    private float targetProgress = 0f;

    private void Awake()
    {
        _loadingSprite = SettingsGame.LoadingSceneWithImage;

        if (_loadingSprite)
        {
            LoadingImageObject.sprite = LoadingSprites[Random.Range(0, LoadingSprites.Length)];
            LoadingImageObject.color = Color.white;
        }
        else
        {
            LoadingImageObject.sprite = null;
            LoadingImageObject.color = Color.black;
        }
    }

    private enum typeScript
    {
        isButton,
        isStart
    }

    private void Start()
    {
        if (_typeScript == typeScript.isStart)
        {
            StartCoroutine(LoadScenes());
        }
    }

    /// <summary>
    /// Метод для кнопки, загружающий сцены в массиве
    /// </summary>
    public void LoadScene()
    {
        StartCoroutine(LoadScenes());
    }

    /// <summary>
    /// Асинхронная загрузка сцен с отображением прогресса
    /// </summary>
    IEnumerator LoadScenes()
    {
        if (_loadingBar)
            loadingScreen.SetActive(true);

        float totalProgress = 0f;
        int sceneCount = scenesToLoad.Length;

        foreach (string sceneName in scenesToLoad)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            asyncLoad.allowSceneActivation = false;

            if (_loadingBar)
            {
                while (!asyncLoad.isDone)
                {
                    float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
                    targetProgress = totalProgress + (progress / sceneCount);

                    totalProgress = Mathf.Lerp(totalProgress, targetProgress, Time.deltaTime * 2f);

                    if(totalProgress > 1f)
                    {
                        totalProgress = 1f;
                    }

                    progressBar.fillAmount = totalProgress;
                    progressText.text = (totalProgress * 100f).ToString("F0") + "%";

                    yield return null;

                    if (totalProgress >= 1f)
                    {
                        asyncLoad.allowSceneActivation = true;
                    }
                }
            }
        }

        if (_unloadThisScene)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

        if (_loadingBar)
            loadingScreen.SetActive(false);

        Debug.Log("Все сцены загружены!");
    }
}
