using System;
using Team8.Unemployment.Global;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Team8.Unemployment.Home
{
    public class HomeView : MonoBehaviour
    {
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _creditButton;
        [SerializeField] private Button _quitButton;
        
        [Header("Credit Display")]
        [SerializeField] private CanvasGroup _creditPanel;

        private void Start()
        {
            _newGameButton.onClick.AddListener(OnNewGame);
            _creditButton.onClick.AddListener(OnCredit);
            _quitButton.onClick.AddListener(OnQuit);
        }
        private void OnNewGame()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            //SceneManager.LoadScene("TestGameplay");
        }
        private void OnCredit()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
        }
        private void OnQuit()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            Application.Quit();
        }
    }
}