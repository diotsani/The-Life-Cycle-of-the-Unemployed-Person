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
        public bool isPauseGame;

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
                playerStatusData.isPlayGame = false;
                if (playerStatusData.isNewDay)
                {
                    OnChangeDay?.Invoke();
                    playerStatusData.ResetAction();
                    playerStatusData.AppliedJob();
                    _dayManager.ChangeDay(0.5f);
                }
            }
        }
        public void CheckEndGame() // after apply job, in change day
        {
            if (playerStatusData.isApplyJob)
            {
                Debug.Log("Check End Game After Apply Job");
                CheckProbability(playerStatusData.skill,playerStatusData.maxSkill);
            }
        }
        private void EndGameCondition()
        {
            if (playerStatusData.stress >= 100)
            {
                OnGameOver(Constants.EndGame.LoseDescriptionStress);
            }
            else if (playerStatusData.health <= 0)
            {
                OnGameOver(Constants.EndGame.LoseDescriptionHealth);
            }
            else if(playerStatusData.isApplied && playerStatusData.skill >= 100)
            {
                OnVictory();
            }
            if(!playerStatusData.isMaxDay)return;
            if (playerStatusData.skill <= 100 && playerStatusData.isApplied)
            {
                ProbabilityVictory(playerStatusData.skill,playerStatusData.maxSkill);
                return;
            }
            OnGameOver(Constants.EndGame.LoseDescriptionSkill);
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
                OnGameOver(Constants.EndGame.LoseDescriptionSkill);
            }
        }
        private void CheckProbability(int skillChance, int maxSkill)
        {
            int randomChance = Random.Range(0, maxSkill+1);
            Debug.Log(randomChance);
            if (randomChance <= skillChance)
            {
                OnVictory();
            }
            else
            {
                Debug.Log("You are not lucky");
                playerStatusData.isApplyJob = false;
            }
        }

        private void OnGameOver(string message)
        {
            OnEndGame?.Invoke();
            OnShowEndGame?.Invoke(Constants.EndGame.LoseTitle, message);
            isEndGame = true;

        }

        private void OnVictory()
        {
            OnEndGame?.Invoke();

            if (playerStatusData.skill <= 30)
            {
                OnShowEndGame?.Invoke(Constants.EndGame.WinTitle, Constants.EndGame.WinDescription30);
            }
            else if (playerStatusData.skill <= 60 && playerStatusData.skill > 30)
            {
                OnShowEndGame?.Invoke(Constants.EndGame.WinTitle, Constants.EndGame.WinDescription60);
            }
            else if (playerStatusData.skill <= 90 && playerStatusData.skill > 60)
            {
                OnShowEndGame?.Invoke(Constants.EndGame.WinTitle, Constants.EndGame.WinDescription90);
            }
            else if (playerStatusData.skill > 90)
            {
                OnShowEndGame?.Invoke(Constants.EndGame.WinTitle, Constants.EndGame.WinDescription90);
            }
            //OnShowEndGame?.Invoke(Constants.EndGame.WinTitle, Constants.EndGame.WinDescription);
            isEndGame = true;
        }
    }
}