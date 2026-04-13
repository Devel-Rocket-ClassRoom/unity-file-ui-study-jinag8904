using System;
using TMPro;
using UnityEngine;

public class LocalizationText: MonoBehaviour
{
    public event Action OnLanguageChanged;
    public Languages language;

    private void OnEnable()
    {
        ChangeLanguage();
    }

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
        OnLanguageChanged?.Invoke();
    }
}