using System;
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

        private void Awake()
        {
            _playerStatusData = PlayerStatusData.Instance;
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