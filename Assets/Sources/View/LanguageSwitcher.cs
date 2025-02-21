using UnityEngine;
using Lean.Localization;

public class LanguageSwitcher : MonoBehaviour
{
    private const string EnglishCode = "English";
    private const string RussianCode = "Russian";

    public void Start()
    {
        LeanLocalization.SetCurrentLanguageAll(RussianCode);
    }

    private void OnGUI()
    {
        float buttonWidth = 200f;
        float buttonHeight = 50f;

        if (GUI.Button(new Rect(10, 10, buttonWidth, buttonHeight), "Switch to Russian"))
        {
            LeanLocalization.SetCurrentLanguageAll(RussianCode);
        }

        if (GUI.Button(new Rect(10, 70, buttonWidth, buttonHeight), "Switch to English"))
        {
            LeanLocalization.SetCurrentLanguageAll(EnglishCode);
        }
    }
}