using System;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class DayManager : MonoBehaviour
    {
        public delegate void EventName();
        public static event EventName OnMaxDay;
        
        public delegate void EventParameter(int value);
        public static event EventParameter OnShowDay;
        
        [SerializeField] private int _amountDay;
        private int _maxDay = 7;
        private bool isMaxDay = false;

        private void Start()
        {
            ChangeDay();
        }
        private void ChangeDay()
        {
            CheckDay();
            if (!isMaxDay)
            {
                _amountDay++;
                OnShowDay?.Invoke(_amountDay);
            }
        }
        private void CheckDay()
        {
            if (_amountDay == _maxDay)
            {
                isMaxDay = true;
                OnMaxDay?.Invoke();
            }
        }
    }
}