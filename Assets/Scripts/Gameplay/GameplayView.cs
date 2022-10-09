using System;
using System.Collections;
using System.Collections.Generic;
using Team8.Unemployment.Global;
using TMPro;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class GameplayView : MonoBehaviour
    {
        [Header("Dependencies")]
        PlayerStatusData _playerStatusData;
        [SerializeField] DayManager _dayManager;
        
        [Header("Day Display")]
        [SerializeField] private GameObject _dayPanel;
        [SerializeField] private TMP_Text _dayText;
        
        [Header("Monolog Display")]
        [SerializeField] private GameObject _monologPanel;
        [SerializeField] private TMP_Text _monologText;
        
        [Header("Status Float Display")]
        [SerializeField] private GameObject _statusFloatHeader;
        [SerializeField] private TMP_Text _statusFloatText;
        private List<TMP_Text> _statusFloatTexts = new List<TMP_Text>();
        private int _amountStatus = 5;

        [Header("Player Stats Display")]
        [SerializeField] private TMP_Text _skillText;
        [SerializeField] private TMP_Text _stressText;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private TMP_Text _bookText;
        [SerializeField] private TMP_Text _foodText;
        [SerializeField] private TMP_Text _actionText;
        [SerializeField] private TMP_Text _daysText;

        private bool _isWaitDelay;

        private void OnEnable()
        {
            //DayManager.OnShowDay += ShowDay;
            BaseInteraction.OnShowMonologue += ShowMonolog;
            PlayerStatusData.OnStatusChange += ShowStatus;
        }

        private void OnDisable()
        {
            //DayManager.OnShowDay -= ShowDay;
            BaseInteraction.OnShowMonologue -= ShowMonolog;
            PlayerStatusData.OnStatusChange -= ShowStatus;
        }

        private void Start()
        {
            _playerStatusData = PlayerStatusData.Instance;
            
            InitStatusFloat();
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
            _daysText.text = Constants.Status.Day + _dayManager.AmountDay().ToString();
        }

        private void ShowDay(int value)
        {
            _dayText.text = Constants.Status.Day + value.ToString();
            _dayPanel.SetActive(true);
            StartCoroutine(AfterActive(_dayPanel,2f));
        }

        private void ShowMonolog(string monolog)
        {
            _isWaitDelay = true;
            _monologText.text = monolog;
            _monologPanel.SetActive(true);
            StartCoroutine(AfterActive(_monologPanel,3f));
        }
        private void InitStatusFloat()
        {
            for (int i = 0; i < _amountStatus; i++)
            {
                TMP_Text text = Instantiate(_statusFloatText, _statusFloatHeader.transform);
                _statusFloatTexts.Add(text);
                text.gameObject.SetActive(false);
            }
        }
        private void ShowStatus(string name, int value)
        {
            TMP_Text text = GetStatusText();
            if(text != null)
            {
                text.gameObject.SetActive(true);
                text.text = $"{name} {value.ToString("+#;-#;0")}";
                text.transform.SetAsLastSibling();
                StartCoroutine(AfterActive(text.gameObject, 3f));
            }
        }
        private TMP_Text GetStatusText()
        {
            foreach (var text in _statusFloatTexts)
            {
                if (!text.gameObject.activeInHierarchy)
                {
                    return text;
                }
            }
            return null;
        }
        private IEnumerator AfterActive(GameObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);
            _isWaitDelay = false;
            obj.SetActive(false);
        }
    }
}