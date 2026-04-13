using TMPro;
using UnityEngine;

public class LocalizationText: MonoBehaviour
{
    public StringTableText text1;
    public StringTableText text2;
    public Languages language;

    private void OnValidate()
    {
        ChangeLanguage();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))   // 한국어
        {
            language = Languages.Korean;
            ChangeLanguage();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))   // 영어
        {
            language = Languages.English;
            ChangeLanguage();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))   // 일본어
        {
            language = Languages.Japanese;
            ChangeLanguage();
        }
    }

    private void ChangeLanguage()
    {
        Variables.Language = language;
        UpdateText();
    }

    private void UpdateText()
    {
        text1.OnChangedId();
        text2.OnChangedId();
    }
}