using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public interface IInteractable
    {
        string GetName();
        
        void OnInteraction(bool status);

        Vector3 TargetPostision();
        float StopDistance();
    }
}