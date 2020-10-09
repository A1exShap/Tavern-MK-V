using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class MessageBox : BaseMenu, IStatusMessager
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Text _text;
    [SerializeField] private ButtonUI _resrart;
    [SerializeField] private ButtonUI _menu;
    [SerializeField] private ButtonUI _next;
    [SerializeField] private CollisionObserver _collisionObserver;

    public void OnEnable()
    {
        LanguageController.Instance.languageChanged += Translation;
        _collisionObserver.onCollision += Show;
    }

    public void OnDisable()
    {
        LanguageController.Instance.languageChanged -= Translation;
        _collisionObserver.onCollision -= Show;
    }

    protected override void Start()
    {
        _thisMenuName = "StatusShowBox";
        IsShow = true;
        Hide();
        base.Start();
    }

    public void Show(StatusType statusType)
    {
        if (statusType == StatusType.None)
        {
            return;
        }

        _text.text = statusType == StatusType.Win ?
            LanguageController.Instance.Text(_thisMenuName, "Win") :
            LanguageController.Instance.Text(_thisMenuName, "Lose");

        _next.GetControl.enabled = statusType == StatusType.Win;

        Show();
    }

    public override void Show()
    {
        if (IsShow) return;
        SceneSwitch.Instance.Pause(true);
        SetVisible(true);
    }

    public override void Hide()
    {
        if (!IsShow) return;
        SceneSwitch.Instance.Pause(false);
        SetVisible(false);
    }

    private void SetVisible(bool visible)
    {
        _panel.SetActive(visible);
        IsShow = visible;
    }

    protected override void SetButtonsEvents()
    {
        _resrart.GetControl.onClick.AddListener(SceneSwitch.Instance.Restart);
        _menu.GetControl.onClick.AddListener(SceneSwitch.Instance.LoadMainMenu);
        _next.GetControl.onClick.AddListener(SceneSwitch.Instance.LoadNextScene);
    }

    protected override void ButtonsDictionaryFilling()
    {
        _buttons.Add("Restart", _resrart);
        _buttons.Add("Menu", _menu);
        _buttons.Add("Next", _next);
    }
}
