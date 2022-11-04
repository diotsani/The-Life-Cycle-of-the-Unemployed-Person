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
        [SerializeField] private float _scrollSpeed;
        [SerializeField] private float _dayCycleTime;
        [SerializeField] private GameObject _clockwise;
        [SerializeField] private GameObject[] _dayCycle;
        private float _clockMorning = 210f;
        private float _clockNoon = 360f;
        private float _clockAfternoon = 120f;
        private float _clockNight = 210f;
        
        
        [Header("Color")]
        [SerializeField] private Color _morningColor; 
        [SerializeField] private Color _noonColor;
        [SerializeField] private Color _afternoonColor;
        [SerializeField] private Color _nightColor;
        
        [Header("Light")]
        [SerializeField] private Light _noonLight_1;
        [SerializeField] private Light _noonLight_2;

        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private GameObject _pSystemSpotlight;

        [Header("Spotlight")]
        [SerializeField] private GameObject[] _spotlight;

        private void Awake()
        {
            _playerStatusData = PlayerStatusData.Instance;
        }

        private void Start()
        { 
            var pSystem = Instantiate(_particleSystem, transform);
            pSystem.gameObject.SetActive(true);
            pSystem.Play();

            var pSpotlight = Instantiate(_pSystemSpotlight, transform);
            pSpotlight.gameObject.SetActive(true);
        }

        private void Update()
        {
            CameraZoom();
            float delay = Mathf.PingPong(Time.time, _dayCycleTime) / _dayCycleTime;
            if (_playerStatusData.action == 3)
            {
                _camera.DOColor(_morningColor, _dayCycleTime);
                _noonLight_1.DOIntensity(1.4f, _dayCycleTime).From(0);
                _noonLight_2.DOIntensity(7.83f, _dayCycleTime).From(0);

                DayCycle("Morning");
                Spotlight("MorningSpotlight");

                _clockwise.transform.DORotate(new Vector3(0, 0, _clockMorning),4f);
            }
            else if (_playerStatusData.action == 2)
            {
                _camera.DOColor(_noonColor, _dayCycleTime);

                DayCycle("Noon");
                
                _clockwise.transform.DORotate(new Vector3(0, 0, _clockNoon),4f);
            }
            else if (_playerStatusData.action == 1)
            {
                _camera.DOColor(_afternoonColor, _dayCycleTime);

                DayCycle("Afternoon");
                Spotlight("AfternoonSpotlight");
                
                _clockwise.transform.DORotate(new Vector3(0, 0, _clockAfternoon),4f);
            }
            else if (_playerStatusData.action == 0)
            {
                _camera.DOColor(_nightColor, _dayCycleTime);

                DayCycle("Night");
                Spotlight("NightSpotlight");
                
                _clockwise.transform.DORotate(new Vector3(0, 0, _clockNight),4f);
            }
        }

        private void CameraZoom()
        {
            float maxZoom = 1.14f;
            float minZoom = 0.65f;
            _camera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * _scrollSpeed;

            if (_camera.orthographicSize > maxZoom)
            {
                _camera.orthographicSize = maxZoom;
            }
            else if(_camera.orthographicSize < minZoom)
            {
                _camera.orthographicSize = minZoom;
            }
            
        }
        private void DayCycle(string name)
        {
            foreach (GameObject day in _dayCycle)
            {
                day.SetActive(false);
                if (day.name == name)
                {
                    day.SetActive(true);
                }
            }
        }
        private void Spotlight(string name)
        {
            foreach (GameObject spot in _spotlight)
            {
                spot.SetActive(false);
                if (spot.name == name)
                {
                    spot.SetActive(true);
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