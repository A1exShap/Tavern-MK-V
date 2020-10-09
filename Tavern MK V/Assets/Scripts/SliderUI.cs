using UnityEngine;
using UnityEngine.UI;


public sealed class SliderUI : MonoBehaviour, IText, IControl
{
    #region Private Fields

    private Text _text;
    private Slider _control;

    #endregion


    #region UnityMethods

    private void Awake()
    {
        _control = transform.GetComponentInChildren<Slider>();
        _text = transform.GetComponentInChildren<Text>();
    }

    #endregion


    #region Methods

    public Text GetText => _text;

    public Slider GetControl => _control;

    public GameObject Instance => gameObject;

    public Selectable Control => GetControl;

    public void SetValue(float value)
    {
        _control.value = value;
    }

    public void Interractable(bool value)
    {
        GetControl.interactable = value;
    }

    #endregion
}
