using System.Xml;
using UnityEngine;


public class LanguageController : Singleton<LanguageController>
{
    #region Properties

    public string LanguageCode { get; private set; }

    #endregion


    #region PrivateFields

    private XmlDocument _root;

    #endregion


    #region Events

    public delegate void LanguageChanged();
    public event LanguageChanged languageChanged;

    #endregion


    #region UnityMethods

    private void Awake()
    {
        Init();
    }

    #endregion


    #region Methods

    public void Init(string languageCode = "", string file = "Language")
    {
        _root = new XmlDocument();

        if (!languageCode.Equals("")) LanguageCode = languageCode;
        else if (string.IsNullOrWhiteSpace(LanguageCode))
        {
            LanguageCode = SetLanguage();
        }
        

        var config = LoadResource(file);

        if (!config) return;

        _root.LoadXml(config.text);
    }

    public string Text(string level, string id)
    {
        if (_root == null) return "[not init]";
        string path = $"Settings/group[@id='{level}']/string[@id='{id}']/text";
        XmlNode value = _root.SelectSingleNode(path);
        if (value == null) return "[not found]";
        return value.InnerText;
    }

    public void SwitchLanguage()
    {
        switch (LanguageCode)
        {
            case "RU":
                Init("EN");
                break;
            case "EN":
                Init("DE");
                break;
            case "DE":
                Init("RU");
                break;
            default:
                Init("EN");
                break;
        }
        languageChanged?.Invoke();
    }

    private string LocalizeResourceName(string resourceName)
    {
        return $"{resourceName}{LanguageCode}";
    }

    private TextAsset LoadResource(string resourceName)
    {
        return Resources.Load(LocalizeResourceName(resourceName),
            typeof(TextAsset)) as TextAsset;
    }

    private string SetLanguage()
    {
        return Application.systemLanguage == SystemLanguage.Russian ? "RU" : "EN";
    }

    #endregion
}
