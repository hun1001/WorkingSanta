using System;
using Prototype;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

class ExitManager : MonoBehaviour
{
    [SerializeField] CanvasGroup exitCanvas;
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;

    private void Awake()
    {
        ButtonManager.Instance.AddHandledButton(yesButton);
        ButtonManager.Instance.AddHandledButton(noButton);
    }

    private void Start()
    {
        exitCanvas.alpha = 0;
        exitCanvas.interactable = false;
        exitCanvas.blocksRaycasts = false;

        yesButton.onClick.AddListener(() =>
        {
            Yes();
        });

        noButton.onClick.AddListener(() =>
        {
            No();
        });

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Awake();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (exitCanvas.alpha == 0)
            {
                exitCanvas.DOFade(1, 0.5f).From(0);
                exitCanvas.blocksRaycasts = true;
                exitCanvas.interactable = true;
                Time.timeScale = 0;
            }
            else
            {
                exitCanvas.DOFade(0, 0.5f).From(1);
                exitCanvas.blocksRaycasts = false;
                exitCanvas.interactable = false;
                Time.timeScale = 1;
            }
        }
    }

    public void Yes()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void No()
    {
        exitCanvas.alpha = 0;
        exitCanvas.blocksRaycasts = false;
        exitCanvas.interactable = false;
        Time.timeScale = 1;
    }
}