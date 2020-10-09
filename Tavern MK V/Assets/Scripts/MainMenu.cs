using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public sealed class MainMenu : BaseMenu

{
    #region PrivateFields

    [SerializeField] private GameObject _canvas;
    [SerializeField] private ButtonUI _newGame;
    [SerializeField] private ButtonUI _quit;
    [SerializeField] private ButtonUI _language;
    [SerializeField] private SliderUI _loadSlider;

    #endregion


    #region UnityMethods

    protected override void Start()
    {
        _thisMenuName = "MainMenu";
        SceneSwitch.Instance.SetCurrentScene();
        SceneSwitch.Instance.SetLoadSlider(_loadSlider);
        base.Start();
    }

    #endregion


    #region Methods

    protected override void Translation()
    {
        base.Translation();

        _language.GetText.text = LanguageController.Instance.LanguageCode;
    }

    protected override void SetButtonsEvents()
    {
        _newGame.GetControl.onClick.AddListener(LoadNextScene);
        _quit.GetControl.onClick.AddListener(SceneSwitch.Instance.GameQuit);
        _language.GetControl.onClick.AddListener(SwitchLanguage);
    }

    public override void Hide()
    {
        if (!IsShow) return;
        SetVisible(false);
    }

    public override void Show()
    {
        if (IsShow) return;
        SetVisible(true);
    }

    private void SetVisible(bool visible)
    {
        _canvas.SetActive(visible);
        IsShow = visible;
    }

    protected override void ButtonsDictionaryFilling()
    {
        _buttons.Add("NewGame", _newGame);
        _buttons.Add("Quit", _quit);
    }

    #endregion
}
