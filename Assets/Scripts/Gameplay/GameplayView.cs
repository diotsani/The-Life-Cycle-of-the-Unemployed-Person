﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Team8.Unemployment.Global;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Team8.Unemployment.Gameplay
{
    public class GameplayView : MonoBehaviour
    {
        [Header("Dependencies")]
        PlayerStatusData _playerStatusData;
        [SerializeField] DayManager _dayManager;
        
        [Header("Begin Display")]
        [SerializeField] GameObject _beginPanel;
        [SerializeField] TMP_Text _beginText;
        
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
        [Header("End Game Display")]
        [SerializeField] private GameObject _endGamePanel;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _descriptionText;
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
            DayManager.OnShowDay += ShowDay;
            BaseInteraction.OnShowMonologue += ShowMonolog;
            BaseInteraction.OnShowHistory += ShowHistory;
            PlayerStatusData.OnStatusChange += ShowStatus;
            GameplayFlow.OnShowEndGame += ShowEndPanel;
            GameplayFlow.OnBeginGame += ShowBegin;
        }

        private void OnDisable()
        {
            DayManager.OnShowDay -= ShowDay;
            BaseInteraction.OnShowMonologue -= ShowMonolog;
            BaseInteraction.OnShowHistory -= ShowHistory;
            PlayerStatusData.OnStatusChange -= ShowStatus;
            GameplayFlow.OnShowEndGame -= ShowEndPanel;
            GameplayFlow.OnBeginGame -= ShowBegin;
        }

        private void Start()
        {
            _playerStatusData = PlayerStatusData.Instance;
            _beginPanel.GetComponent<Button>().onClick.AddListener(ClickBegin);
            _endGamePanel.GetComponent<Button>().onClick.AddListener(ResetGameplay);
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

        void ResetGameplay()
        {
            _playerStatusData.ResetStatus();
            SceneManager.LoadScene("TestGameplay");
        }

        private void ShowBegin()
        {
            _beginPanel.SetActive(true);
        }
        private void ClickBegin()
        {
            _beginPanel.SetActive(false);
            _dayManager.ChangeDay(0);
        }

        private void ShowDay(int value, int delay)
        {
            // _dayText.text = Constants.Status.Day + value.ToString();
            // _dayPanel.SetActive(true);
            StartCoroutine(ShowDayDelay(value,delay));
        }

        private void ShowMonolog(string monolog)
        {
            _monologText.text = monolog;
            _monologPanel.SetActive(true);
            StartCoroutine(AfterActive(_monologPanel,3f));
        }
        private void ShowEndPanel(string title, string description)
        {
            _titleText.text = title;
            _descriptionText.text = description;
            _endGamePanel.SetActive(true);
        }

        void ShowHistory(string getHistory)
        {
            var text = Instantiate(_historyText,_content.transform);
            text.text = getHistory;
            text.gameObject.SetActive(true);
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
                text.DOFade(1,1.5f).OnComplete(() => text.DOFade(0,3f).OnComplete(() => text.gameObject.SetActive(false)));
                //StartCoroutine(AfterActive(text.gameObject, 3f));
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
            obj.SetActive(false);
        }
        private IEnumerator ShowDayDelay(int value, int delay)
        {
            yield return new WaitForSeconds(delay);
            _dayText.text = Constants.Status.Day + value.ToString();
            _dayPanel.SetActive(true);
            yield return new WaitForSeconds(3);
            _playerStatusData.ChangeStatus();
            _dayPanel.SetActive(false);
        }
    }
}