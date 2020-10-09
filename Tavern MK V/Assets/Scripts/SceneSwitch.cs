using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public sealed class SceneSwitch : Singleton<SceneSwitch>
{
    #region Properties

    public int CurrentScene { get; private set; }
    public SliderUI LoadSlider { get; private set; }

    #endregion


    #region Methods

    public void LoadMainMenu()
    {
        LoadScene(0);
    }

    public void LoadNextScene()
    {
        if (CurrentScene == SceneManager.sceneCount)
        {
            LoadMainMenu();
        }
        else
        {
            LoadScene(CurrentScene + 1);
        }
    }

    public void Restart()
    {
        LoadScene(CurrentScene);
    }

    public void LoadScene(int sceneIndex)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);
        StartCoroutine(LoadSceneAsyncC(async));
    }

    private IEnumerator LoadSceneAsyncC(AsyncOperation async)
    {
        EnableLoadSlider();
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            SetLoadValue(async.progress + 0.1f);
            float progress = async.progress * 100f;
            if (async.progress < 0.9f && Mathf.RoundToInt(progress) != 100)
            {
                async.allowSceneActivation = false;
            }
            else
            {
                if (async.allowSceneActivation) yield return null;
                async.allowSceneActivation = true;
                DisableLoadSlider();
            }
            yield return null;
        }
    }

    public void Pause(bool pause)
    {
        Time.timeScale = pause ? 0f : 1f;
    }

    public void SetLoadSlider(SliderUI slider)
    {
        LoadSlider = slider;
        LoadSlider.gameObject.SetActive(false);
    }

    private void EnableLoadSlider()
    {
        if (LoadSlider) return;
        LoadSlider.gameObject.SetActive(true);
        SetLoadValue(0f);
    }

    private void DisableLoadSlider()
    {
        if (!LoadSlider) return;
        LoadSlider.gameObject.SetActive(false);
    }

    private void SetLoadValue(float value)
    {
        if (LoadSlider == null) return;
        LoadSlider.GetControl.value = value;
    }

    public void SetCurrentScene()
    {
        CurrentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    #endregion
}
