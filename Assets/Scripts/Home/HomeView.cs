using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Team8.Unemployment.Home
{
    public class HomeView : MonoBehaviour
    {
        [SerializeField] private Button _newGameButton;

        private void Start()
        {
            _newGameButton.onClick.AddListener(OnNewGame);
        }
        private void OnNewGame()
        {
            SceneManager.LoadScene("TestGameplay");
        }
    }
}