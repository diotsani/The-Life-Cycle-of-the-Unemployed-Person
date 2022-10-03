using System;
using Team8.Unemployment.Global;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class GameplayFlow : MonoBehaviour
    {
        PlayerStatusData playerStatusData;

        private void Start()
        {
            playerStatusData = PlayerStatusData.instance;
        }

        private void ActionOver()
        {
            if (playerStatusData.action == 0)
            {
                //change day
                playerStatusData.ResetAction();
            }
        }
    }
}