using System;
using Team8.Unemployment.Global;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

namespace Team8.Unemployment.Gameplay
{
    public class GameplayFlow : MonoBehaviour
    {
        public delegate void EventName();
        public static event EventName OnEndGame;
        public static event EventName OnBeginGame;
        public static event EventName OnEndDay;
        public static event EventName OnChangeDay;
        
        public delegate void EventEndGame(string title, string description);
        public static event EventEndGame OnShowEndGame;
        
        [Header("Dependencies")] private PlayerStatusData playerStatusData;
        [SerializeField] private DayManager _dayManager;
        
        [SerializeField] private bool isEndGame;

        private void Awake()
        {
            playerStatusData = PlayerStatusData.Instance;
        }

        private void Start()
        {
            playerStatusData = PlayerStatusData.Instance;
            
            OnBeginGame?.Invoke();
        }

        private void Update()
        {
            if (!isEndGame)
            {
                EndGameCondition();
                ActionOver();
            }
        }

        private void ActionOver()
        {
            if (playerStatusData.action == 0)
            {
                OnEndDay?.Invoke();
                if (playerStatusData.isNewDay)
                {
                    OnChangeDay?.Invoke();
                    playerStatusData.ResetAction();
                    playerStatusData.AppliedJob();
                    _dayManager.ChangeDay(0.5f);
                }
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
                ProbabilityVictory(playerStatusData.skill,playerStatusData.maxSkill);
                return;
            }
            OnVictory();
        }
        private void ProbabilityVictory(int skillChance, int maxSkill)
        {
            int randomChance = Random.Range(0, maxSkill+1);
            Debug.Log(randomChance);
            if (randomChance <= skillChance)
            {
                OnVictory();
            }
            else
            {
                OnGameOver(" Skill Not Enough");
            }
        }

        private void OnGameOver(string add)
        {
            OnEndGame?.Invoke();
            OnShowEndGame?.Invoke(Constants.EndGame.LoseTitle, Constants.EndGame.LoseDescription + add);
            isEndGame = true;
        }

        private void OnVictory()
        {
            OnEndGame?.Invoke();
            OnShowEndGame?.Invoke(Constants.EndGame.WinTitle, Constants.EndGame.WinDescription);
            isEndGame = true;
        }
    }
}