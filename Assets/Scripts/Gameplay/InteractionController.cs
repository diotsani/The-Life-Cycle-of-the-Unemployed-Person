using System.Collections.Generic;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class InteractionController : MonoBehaviour
    {
        [SerializeField] private List<BaseInteraction> _objects;

        private void OnEnable()
        {
            Decision.OnClickDecision += SetAllObjects;
            GameplayFlow.OnChangeDay += ChangeDay;
            GameplayFlow.OnEndGame += OnGameOver;
        }

        private void OnDisable()
        {
            Decision.OnClickDecision -= SetAllObjects;
            GameplayFlow.OnChangeDay -= ChangeDay;
            GameplayFlow.OnEndGame -= OnGameOver;
        }

        private void SetAllObjects()
        {
            foreach (var obj in _objects)
            {
                obj.isClicked = true;
            }
        }

        private void ChangeDay()
        {
            foreach (var obj in _objects)
            {
                obj.AddDurability();
            }
        }
        public void ResetAllDurability()
        {
            foreach (var obj in _objects)
            {
                obj.ResetDurability();
            }
        }
        private void OnGameOver()
        {
            foreach (var obj in _objects)
            {
                obj.DeactivateDecision();
            }
        }
    }
}