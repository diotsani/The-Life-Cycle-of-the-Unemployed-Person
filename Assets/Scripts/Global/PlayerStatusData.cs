using System;
using Team8.Unemployment.Gameplay;
using Team8.Unemployment.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Team8.Unemployment.Global
{
    public class PlayerStatusData : SingletonMonoBehaviour<PlayerStatusData>
    {
        public delegate void EventName();
        //public static event EventName OnResetAction;
        
        public delegate void EventParameter(string name, int value);
        public static event EventParameter OnStatusChange;

        private PlayerScriptable PlayerScriptable;
        
        [Header("Config")]
        public int skill;
        public int stress;
        public int health;
        public int money;
        public int book;
        public int food;
        public int action;
        private int _resetAction = 3;
        private int _totalAction;

        [Header("Change Config")] 
        public int day;
        public int addStress;
        public int reduceHealth;
        public int minStress = 60;
        public int maxSkill { get; private set; } = 100;

        public bool isPlayGame;
        public bool isEndGame;
        public bool isFresh = true;
        public bool isNewDay;
        public bool isMaxDay;
        public bool isApplyJob;
        public bool isApplied;
        private void Start()
        {
            PlayerScriptable = Resources.Load<PlayerScriptable>(Constants.Path.Player);
            ResetStatus();
        }
        public void ResetStatus()
        {
            skill = PlayerScriptable.playerData.skill;
            stress = PlayerScriptable.playerData.stress;
            health = PlayerScriptable.playerData.health;
            money = PlayerScriptable.playerData.money;
            book = PlayerScriptable.playerData.book;
            food = PlayerScriptable.playerData.food;
            action = PlayerScriptable.playerData.action;
            _resetAction = PlayerScriptable.playerData.action;
            //_totalAction = PlayerScriptable.playerData.totalAction;
            day = PlayerScriptable.playerData.day;
            addStress = PlayerScriptable.playerData.addStress;
            reduceHealth = PlayerScriptable.playerData.reduceHealth;
            
            isFresh = true;
            isMaxDay = false;
            isApplyJob = false;
            isApplied = false;
        }

        public void NewDay()
        {
            isNewDay = true;
        }
        public void ChangeStatus()
        {
            if(day <=1)return;
            health -= reduceHealth;
            stress += addStress;
            isPlayGame = true;
            // OnStatusChange?.Invoke("Health", _reduceHealth*-1);
            // OnStatusChange?.Invoke("Stress", _addStress);
            
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
            var positiveValue = Mathf.Abs(value);
            if(positiveValue >= 12)
            {
                int rndSkill = Random.Range(10, value + 1);
                skill += rndSkill;
                Debug.Log("Skill "+rndSkill);
                //OnStatusChange?.Invoke("Skill", rndSkill);
            }
            else
            {
                skill += value;
                Debug.Log("Skill " + value);
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
            var positiveValue = Mathf.Abs(value);
            if(positiveValue > 1)Debug.Log("Stress " + value);
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
            var positiveValue = Mathf.Abs(value);
            
            health += value;
            Debug.Log("Health "+value);
            
            // if(positiveValue > 1)
            // {
            //     OnStatusChange?.Invoke("Health", value);
            // }
            
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
            if (value >= 18)
            {
                int rndMoney = Random.Range(12, 18 + 1);
                money += rndMoney;
                Debug.Log("Money "+ rndMoney);
            }
            else
            {
                money += value;
                Debug.Log("Money "+ value);
            }
            // var positiveValue = Mathf.Abs(value);
            // if(positiveValue > 1)
            // {
            //     OnStatusChange?.Invoke("Money", value);
            // }

            if (money < 0)
            {
                money = 0;
            }
        }
        public void BookCost(int value)
        {
            book += value;
            
            // var positiveValue = Mathf.Abs(value);
            // if(positiveValue > 0)
            // {
            //     OnStatusChange?.Invoke("Book", value);
            // }
            
            if (book < 0)
            {
                book = 0;
            }
        }
        public void FoodCost(int value)
        {
            if (value == 8) //Buy Food
            {
                food += value;
                OnStatusChange?.Invoke("Food", value);
            }
            if(isFresh && value == 16) // Eat Food
            {
                int randomFood = Random.Range(10, value+1);
                food -= randomFood;
                OnStatusChange?.Invoke("Food", randomFood*-1);
                HealthCost(24);
                
                Debug.Log("Eat Fresh Food" + randomFood);
            }
            else if(!isFresh && value == 16) // Eat Food not fresh
            {
                int randomFood = Random.Range(10, value+1);
                food -= randomFood;
                OnStatusChange?.Invoke("Food", randomFood*-1);
                HealthCost(-14);
                
                Debug.Log("Eat Not Fresh Food" + randomFood);
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
            if(!isApplyJob) isApplied= false;
        }
    }
}