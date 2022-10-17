using System;
using System.Collections.Generic;
using DG.Tweening;
using Team8.Unemployment.Global;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class DayManager : MonoBehaviour
    {
        public delegate void EventName();
        public static event EventName OnMaxDay;
        
        public delegate void EventParameter(int value,float delay);
        public static event EventParameter OnShowDay;
        
        private PlayerStatusData _playerStatusData;
        
        [SerializeField] private int _amountDay;
        private int _maxDay = 7;
        
        [Header("Day Cycle")]
        [SerializeField] private Camera _camera;
        [SerializeField] private float _dayCycleTime;
        [SerializeField] private GameObject[] _dayCycle;
        [Header("Color")]
        [SerializeField] private Color _morningColor; 
        [SerializeField] private Color _noonColor;
        [SerializeField] private Color _afternoonColor;
        [SerializeField] private Color _nightColor;
        
        [Header("Light")]
        [SerializeField] private Light _noonLight_1;
        [SerializeField] private Light _noonLight_2;

        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private ParticleSystem _pSystemCurrent;

        [Header("Spotlight")]
        [SerializeField] private GameObject[] _spotlight;

        private void Awake()
        {
            _playerStatusData = PlayerStatusData.Instance;
        }

        private void Start()
        { 
            _pSystemCurrent = Instantiate(_particleSystem, transform);
            _pSystemCurrent.gameObject.SetActive(true);
            _pSystemCurrent.Play();
        }

        private void Update()
        {
            float delay = Mathf.PingPong(Time.time, _dayCycleTime) / _dayCycleTime;
            if (_playerStatusData.action == 3)
            {
                _camera.DOColor(_morningColor, _dayCycleTime);
                _noonLight_1.DOIntensity(1.4f, _dayCycleTime).From(0);
                _noonLight_2.DOIntensity(7.83f, _dayCycleTime).From(0);

                foreach (GameObject day in _dayCycle)
                {
                    day.SetActive(false);
                    if (day.name == "Morning")
                    {
                        day.SetActive(true);
                    }
                }
            }
            else if (_playerStatusData.action == 2)
            {
                _camera.DOColor(_noonColor, _dayCycleTime);
                foreach (GameObject day in _dayCycle)
                {
                    day.SetActive(false);
                    if (day.name == "Noon")
                    {
                        day.SetActive(true);
                    }
                }
            }
            else if (_playerStatusData.action == 1)
            {
                _camera.DOColor(_afternoonColor, _dayCycleTime);
                foreach (GameObject day in _dayCycle)
                {
                    day.SetActive(false);
                    if (day.name == "Afternoon")
                    {
                        day.SetActive(true);
                    }
                }
            }
            else if (_playerStatusData.action == 0)
            {
                _camera.DOColor(_nightColor, _dayCycleTime);
                foreach (GameObject day in _dayCycle)
                {
                    day.SetActive(false);
                    if (day.name == "Night")
                    {
                        day.SetActive(true);
                    }
                }
            }
        }

        public void ChangeDay(float delay)
        {
            CheckDay();
            if (_playerStatusData.isMaxDay == false)
            {
                _amountDay++;
                _playerStatusData.day++;
                OnShowDay?.Invoke(_amountDay,delay);
            }
        }
        private void CheckDay()
        {
            if (_amountDay == _maxDay)
            {
                _playerStatusData.isMaxDay = true;
                OnMaxDay?.Invoke();
            }
        }
        public int AmountDay()
        {
            return _amountDay;
        }
    }
}