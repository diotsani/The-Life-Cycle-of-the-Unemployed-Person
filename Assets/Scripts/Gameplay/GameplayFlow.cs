using System;
using Team8.Unemployment.Global;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class GameplayFlow : MonoBehaviour
    {
        [Header("Dependencies")] PlayerStatusData playerStatusData;
        [SerializeField] private DayManager _dayManager;

        private void Start()
        {
            playerStatusData = PlayerStatusData.Instance;

            _dayManager.ChangeDay();
        }

        private void Update()
        {
            EndGameCondition();
        }

        private void ActionOver()
        {
            if (playerStatusData.action == 0)
            {
                playerStatusData.ResetAction();
                _dayManager.ChangeDay();
            }
        }

        private void EndGameCondition()
        {
            if (playerStatusData.food <= 0)
            {
                OnGameOver();
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