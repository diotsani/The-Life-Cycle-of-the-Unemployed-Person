using UnityEngine;

namespace Team8.Unemployment.Global
{
    [System.Serializable]
    public class PlayerData
    {
        public int skill;
        public int stress;
        public int health;
        public int money;
        public int book;
        public int food;
        public int action;
        private int _resetAction;
        private int _totalAction;
        public int day;
        public int addStress;
        public int reduceHealth;
    }
}