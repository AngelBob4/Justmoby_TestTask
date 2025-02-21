using Reflex.Attributes;
using UnityEngine;
using Cubes.Model;
using UnityEngine.UI;

namespace Cubes.View
{
    public class ConsoleBootstrap : MonoBehaviour
    {
        [SerializeField] private Text _consoleTextBox;

        [Inject]
        private void Inject(ConsoleModel consoleModel)
        {
            consoleModel.Init(_consoleTextBox);
        }
    }
}