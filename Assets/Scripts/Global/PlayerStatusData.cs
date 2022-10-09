using System;
using Team8.Unemployment.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Team8.Unemployment.Global
{
    public class PlayerStatusData : SingletonMonoBehaviour<PlayerStatusData>
    {
        public delegate void EventName();
        public static event EventName OnResetAction;
        
        public delegate void EventParameter(string name, int value);
        public static event EventParameter OnStatusChange;
        
        [Header("Config")]
        public int skill = 20;
        public int stress = 10;
        public int health = 100;
        public int money = 50;
        public int book = 5;
        public int food = 30;
        public int action = 3;
        private int _resetAction = 3;
        private int _totalAction;
        
        [Header("Change Config")]
        private int _addStress = 10;
        private int _reduceHealth = 12;

        public bool isFresh = true;
        public bool isMaxDay;
        public bool isApplyJob;
        public bool isApplied;
        private void Start()
        {
            
        }

        private void Update()
        {
            
        }
        public void ChangeStatus()
        {
            // need delay
            
            health -= _reduceHealth;
            stress += _addStress;
            
            OnStatusChange?.Invoke("Health", _reduceHealth*-1);
            OnStatusChange?.Invoke("Stress", _addStress);
            
            if (health < 0)
            {
                health = 0;
            }
            else if (health > 100)
            {
                health = 100;
            }
        }
        public void SkillCost(int value)
        {
            skill += value;
            
            var positiveValue = Mathf.Abs(value);
            if(positiveValue > 1)
            {
                OnStatusChange?.Invoke("Skill", value);
            }
            
            if(skill < 0)
            {
                skill = 0;
            }
            else if(skill > 100)
            {
                skill = 100;
            }
        }
        public void StressCost(int value)
        {
            stress += value;
            
            var positiveValue = Mathf.Abs(value);
            if(positiveValue > 1)
            {
                OnStatusChange?.Invoke("Stress", value);
            }
            
            if (stress < 0)
            {
                stress = 0;
            }
            else if (stress > 100)
            {
                stress = 100;
            }
        }
        public void HealthCost(int value)
        {
            health += value;
            
            var positiveValue = Mathf.Abs(value);
            if(positiveValue > 1)
            {
                OnStatusChange?.Invoke("Health", value);
            }
            
            if (health < 0)
            {
                health = 0;
            }
            else if (health > 100)
            {
                health = 100;
            }
        }
        public void MoneyCost(int value)
        {
            money += value;
        
            var positiveValue = Mathf.Abs(value);
            if(positiveValue > 1)
            {
                OnStatusChange?.Invoke("Money", value);
            }

            if (money < 0)
            {
                money = 0;
            }
        }
        public void BookCost(int value)
        {
            book += value;
            
            var positiveValue = Mathf.Abs(value);
            if(positiveValue > 0)
            {
                OnStatusChange?.Invoke("Book", value);
            }
            
            if (book < 0)
            {
                book = 0;
            }
        }
        public void FoodCost(int value)
        {
            if (value >= 20) //Buy Food
            {
                food += value;
            }
            if(isFresh && value >= 10) // Eat Food
            {
                int randomFood = Random.Range(10, 16);
                food -= randomFood;
                HealthCost(24);
            }
            else if(!isFresh) // Eat Food not fresh
            {
                int randomFood = Random.Range(10, 16);
                food -= randomFood;
                HealthCost(-14);
            }
        
            if (food < 0)
            {
                food = 0;
            }
        }
        public void ResetFood()
        {
            food = 0;
        }
        public void ActionCost(int value)
        {
            action -= value;
            _totalAction += value;
        }

        public void ResetAction()
        {
            action = _resetAction;
        }
        public void AppliedJob()
        {
            if(isApplyJob) isApplied = true;
        }
    }
}