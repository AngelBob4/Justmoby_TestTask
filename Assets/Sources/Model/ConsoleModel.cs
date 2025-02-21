using Lean.Localization;
using UnityEngine.UI;

public class ConsoleModel
{
    private Text _consoleTextBox;

    public void Init(Text consoleTextBox)
    {
        _consoleTextBox = consoleTextBox;
    }

    public void WriteToConsole(TypeOfText typeOfText)
    {
        LeanTranslation translation = LeanLocalization.CurrentTranslations[typeOfText.ToString()];
        _consoleTextBox.text = translation.Data.ToString();
    }
}

public enum TypeOfText
{
    CubeInstallation,
    CubeThrowing,
    CubeDisappearing,
    HeightLimit,
}