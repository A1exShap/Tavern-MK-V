using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public sealed class PauseMenu : BaseMenu
{
    #region PrivateFields

    [SerializeField] private GameObject _panel;
    [SerializeField] private ButtonUI _pause;
    [SerializeField] private ButtonUI _continue;
    [SerializeField] private ButtonUI _restart;
    [SerializeField] private ButtonUI _menu;
    [SerializeField] private ButtonUI _quit;
    [SerializeField] private ButtonUI _language;
    [SerializeField] private SliderUI _loadSlider;

    #endregion


    #region Events

    public Event LanguageChanged = new Event();

    #endregion


    #region UnityMethods

    protected override void Start()
    {
        _thisMenuName = "PauseMenu";
        IsShow = true;
        Hide();
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
        _pause.GetControl.onClick.AddListener(Pause);
        _continue.GetControl.onClick.AddListener(Pause);
        _restart.GetControl.onClick.AddListener(SceneSwitch.Instance.Restart);
        _menu.GetControl.onClick.AddListener(SceneSwitch.Instance.LoadMainMenu);
        _quit.GetControl.onClick.AddListener(SceneSwitch.Instance.GameQuit);
        _language.GetControl.onClick.AddListener(SwitchLanguage);
    }

    private void Pause()
    {
        if (IsShow) Hide();
        else Show();
    }

    public override void Hide()
    {
        if (!IsShow) return;
        SceneSwitch.Instance.Pause(false);
        SetVisible(false);
        _pause.gameObject.SetActive(true);
    }

    public override void Show()
    {
        if (IsShow) return;
        SceneSwitch.Instance.Pause(true);
        SetVisible(true);
        _pause.gameObject.SetActive(false);
    }

    private void SetVisible(bool visible)
    {
        _panel.SetActive(visible);
        IsShow = visible;
    }

    protected override void ButtonsDictionaryFilling()
    {
        _buttons.Add("Pause", _pause);
        _buttons.Add("Continue", _continue);
        _buttons.Add("Restart", _restart);
        _buttons.Add("Menu", _menu);
        _buttons.Add("Quit", _quit);
    }

    #endregion
}
