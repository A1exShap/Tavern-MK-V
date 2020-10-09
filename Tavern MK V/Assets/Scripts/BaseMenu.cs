using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public abstract class BaseMenu : MonoBehaviour
{
    #region ProtectedFields

    protected bool IsShow { get; set; }
    protected string _thisMenuName;
    protected Dictionary<string, ButtonUI> _buttons = new Dictionary<string, ButtonUI>();
    
    #endregion


    #region UnityMethods

    protected virtual void Start()
    {
        SetButtonsEvents();
        ButtonsDictionaryFilling();
        Translation();
    }

    #endregion


    #region Methods

    public abstract void Show();

    public abstract void Hide();

    protected abstract void SetButtonsEvents();

    protected void SwitchLanguage()
    {
        LanguageController.Instance.SwitchLanguage();
        Translation();
    }

    protected virtual void Translation()
    {
        LanguageController.Instance.Init();

        foreach (var pair in _buttons)
        {
            pair.Value.GetText.text = LanguageController.Instance.Text(_thisMenuName, pair.Key);
        }
    }
    
    protected virtual void LoadScene(int sceneIndex)
    {
        ActionsBeforeSceneSwitch();
        SceneSwitch.Instance.LoadScene(sceneIndex);
    }

    protected virtual void LoadNextScene()
    {
        ActionsBeforeSceneSwitch();
        SceneSwitch.Instance.LoadNextScene();
    }

    protected void ActionsBeforeSceneSwitch()
    {
        Hide();
        SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
    }

    protected virtual void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
    }

    protected abstract void ButtonsDictionaryFilling();

    #endregion
}
