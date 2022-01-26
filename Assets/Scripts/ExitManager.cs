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

    private bool isFading = false;

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
            if (isFading)
            {
                return;
            }

            if (exitCanvas.alpha == 0)
            {
                isFading = true;
                exitCanvas.DOFade(1, 0.5f).From(0).onComplete += () =>
                {
                    isFading = false;
                };
                exitCanvas.blocksRaycasts = true;
                exitCanvas.interactable = true;
            }
            else
            {
                isFading = true;
                exitCanvas.DOFade(0, 0.5f).From(1).onComplete += () =>
                {
                    isFading = false;
                };
                exitCanvas.blocksRaycasts = false;
                exitCanvas.interactable = false;
            }
        }
    }

    public void Yes()
    {
        CharacterStat.SaveData();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void No()
    {
        isFading = true;
        exitCanvas.DOFade(0, 0.5f).From(1).onComplete += () =>
        {
            isFading = false;
        };
        exitCanvas.blocksRaycasts = false;
        exitCanvas.interactable = false;
    }
}