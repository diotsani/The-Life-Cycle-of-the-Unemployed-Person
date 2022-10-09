using System;
using Team8.Unemployment.Global;
using UnityEngine;
using UnityEngine.Rendering;

namespace Team8.Unemployment.Gameplay
{
    public class GameplayFlow : MonoBehaviour
    {
        public delegate void EventName();
        public static event EventName OnEndGame;
        public static event EventName OnChangeDay;
        
        [Header("Dependencies")] private PlayerStatusData playerStatusData;
        [SerializeField] private DayManager _dayManager;

        private void Awake()
        {
            playerStatusData = PlayerStatusData.Instance;
        }

        private void Start()
        {
            playerStatusData = PlayerStatusData.Instance;

            _dayManager.ChangeDay();
        }

        private void Update()
        {
            ActionOver();
            EndGameCondition();
        }

        private void ActionOver()
        {
            if (playerStatusData.action == 0)
            {
                OnChangeDay?.Invoke();
                
                playerStatusData.ResetAction();
                playerStatusData.AppliedJob();
                playerStatusData.ChangeStatus();

                _dayManager.ChangeDay();
            }
        }

        private void EndGameCondition()
        {
            if (playerStatusData.food <= 0)
            {
                OnGameOver();
            }
            if(playerStatusData.isApplyJob && playerStatusData.skill >= 100)
            {
                OnVictory();
            }
            if(!playerStatusData.isMaxDay)return;
            if (playerStatusData.skill <= 100 && !playerStatusData.isApplyJob)
            {
                OnGameOver();
                return;
            }
            OnVictory();
        }

        private void OnGameOver()
        {
            Debug.Log("GameOver");
        }

        private void OnVictory()
        {
            Debug.Log("Victory");
        }
    }
}