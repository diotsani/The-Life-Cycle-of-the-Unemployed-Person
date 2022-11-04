using UnityEngine;

namespace Team8.Unemployment.Global
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Database/PlayerData")]
    public class PlayerScriptable : ScriptableObject
    {
        public PlayerData playerData;
    }
}