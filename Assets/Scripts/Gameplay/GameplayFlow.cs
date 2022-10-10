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
        
        public delegate void EventEndGame(string title, string description);
        public static event EventEndGame OnShowEndGame;
        
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
            EndGameCondition();
            ActionOver();
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
            if (playerStatusData.stress >= 100 || playerStatusData.health <= 0)
            {
                OnGameOver(" Stress Over / Health Under 0, You are dead");
            }
            else if(playerStatusData.isApplied && playerStatusData.skill >= 100)
            {
                OnVictory();
            }
            if(!playerStatusData.isMaxDay)return;
            if (playerStatusData.skill < 100 && playerStatusData.isApplied)
            {
                OnGameOver(" Skill Not Enough");
                return;
            }
            OnVictory();
        }

        private void OnGameOver(string add)
        {
            OnEndGame?.Invoke();
            OnShowEndGame?.Invoke(Constants.EndGame.LoseTitle, Constants.EndGame.LoseDescription + add);
        }

        private void OnVictory()
        {
            OnEndGame?.Invoke();
            OnShowEndGame?.Invoke(Constants.EndGame.WinTitle, Constants.EndGame.WinDescription);
        }
    }
}