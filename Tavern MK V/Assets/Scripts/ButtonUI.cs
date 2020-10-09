using UnityEngine;
using UnityEngine.UI;


public class ButtonUI : MonoBehaviour, IText
{
    #region PrivateFields

    private Text _text;
    private Button _control;

    #endregion


    #region Properties

    public Text GetText
    {
        get
        {
            if (!_text)
            {
                _text = transform.GetComponentInChildren<Text>();
            }

            return _text;
        }
    }

    public Button GetControl
    {
        get
        {
            if (!_control)
            {
                _control = transform.GetComponentInChildren<Button>();
            }

            return _control;
        }
    }

    #endregion


    #region Methods

    public void SetInteractable(bool value)
    {
        GetControl.interactable = value;
    }

    public GameObject Instance => gameObject;

    public Selectable Control => GetControl;

    #endregion
}
