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
    
        public int skill = 20;
        public int stress = 10;
        public int health = 100;
        public int money = 50;
        public int book = 5;
        public int food = 30;
        public int action = 3;
        private int _resetAction = 3;
        private int _totalAction;
        private void Start()
        {
            
        }

        private void Update()
        {
            
        }
        public void SkillCost(int value)
        {
            skill += value;
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
            if (health < 0)
            {
                health = 0;
            }
            else if (health > 100)
            {
                health = 100;
            }
        }
        public void ReduceHealth()
        {
            int randomHealth = Random.Range(30, 40);
            health -= randomHealth;
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
            int PositiveValue = Mathf.Abs(value);
            if (money < PositiveValue)
            {
                return;
            }
            money += value;
            if (money < 0)
            {
                money = 0;
            }
        }
        public void BookCost(int value)
        {
            book += value;
            if (book < 0)
            {
                book = 0;
            }
        }
        public void FoodCost(int value)
        {
            if (value >= 20)
            {
                food += Random.Range(10, value);
            }
            else
            {
                food += value;
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
    }
}