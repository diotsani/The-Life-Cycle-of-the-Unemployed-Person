using System;
using Team8.Unemployment.Global;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class DayManager : MonoBehaviour
    {
        public delegate void EventName();
        public static event EventName OnMaxDay;
        
        public delegate void EventParameter(int value);
        public static event EventParameter OnShowDay;
        
        private PlayerStatusData _playerStatusData;
        
        [SerializeField] private int _amountDay;
        private int _maxDay = 7;

        private void Start()
        {
            _playerStatusData = PlayerStatusData.Instance;
        }
        public void ChangeDay()
        {
            CheckDay();
            if (!_playerStatusData.isMaxDay)
            {
                _amountDay++;
                OnShowDay?.Invoke(_amountDay);
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
    }
}