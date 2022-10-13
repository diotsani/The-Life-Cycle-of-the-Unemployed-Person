using System;
using Team8.Unemployment.Global;
using TMPro;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class DebugView : MonoBehaviour
    {
        PlayerStatusData _playerStatusData;
        [Header("Player Stats Display")]
        [SerializeField] private TMP_Text _skillText;
        [SerializeField] private TMP_Text _stressText;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private TMP_Text _bookText;
        [SerializeField] private TMP_Text _foodText;
        [SerializeField] private TMP_Text _actionText;
        [SerializeField] private TMP_Text _daysText;
        
        [Header("Decision History Display")]
        [SerializeField] private GameObject _content;
        [SerializeField] private TMP_Text _historyText;

        private void OnEnable()
        {
            BaseInteraction.OnShowHistory += ShowHistory;
        }

        private void OnDisable()
        {
            BaseInteraction.OnShowHistory -= ShowHistory;
        }

        private void Start()
        {
            _playerStatusData = PlayerStatusData.Instance;
        }

        private void Update()
        {
            _skillText.text = Constants.Status.Skill + _playerStatusData.skill;
            _stressText.text = Constants.Status.Stress + _playerStatusData.stress;
            _healthText.text = Constants.Status.Health + _playerStatusData.health;
            _moneyText.text = Constants.Status.Money + _playerStatusData.money;
            _bookText.text = Constants.Status.Book + _playerStatusData.book;
            _foodText.text = Constants.Status.Food + _playerStatusData.food;
            _actionText.text = Constants.Status.Action + _playerStatusData.action;
            _daysText.text = Constants.Status.Day + _playerStatusData.day;
        }
        void ShowHistory(string getHistory)
        {
            var text = Instantiate(_historyText,_content.transform);
            text.text = getHistory;
            text.gameObject.SetActive(true);
        }
    }
}