using System;
using System.Collections;
using System.Collections.Generic;
using Team8.Unemployment.Gameplay;
using Team8.Unemployment.Global;
using Team8.Unemployment.Utility;
using UnityEngine;
using UnityEngine.AI;

namespace Team8.Unemployment.Player
{
    public class PlayerController : MonoBehaviour
    {
        public enum PlayerStatus
        {
            idle,
            walk,
            interact,
        }
        public InteractionController interactionController;
        public PlayerStatus playerStatus;
        public NavMeshAgent agent;
        public Animator animator;
        public float animSpeed;
        public float delayAnimSpeed;
        public TipInteraction tipInteraction;
        public float stopDistance;
        [SerializeField] private float _footstepPlayRate = 0.7f;
        private float _lastPlayTime;
        
        public IInteractable currentInteractable;
        public BaseInteraction currentInteraction;
        
        public bool isWalking;
        private void FixedUpdate()
        {
            switch (playerStatus)
            {
                case PlayerStatus.walk:
                    Walk();
                    break;
                case PlayerStatus.interact:
                    //Interact();
                    break;
                default:
                    playerStatus = PlayerStatus.idle;
                    break;
            }
        }

        public void Walk()
        {
            if (Vector3.Distance(transform.position, tipInteraction.target) <= stopDistance)
            {
                isWalking = false;
                currentInteraction.isInteracted = true;
                //interactionController.SetInteracted(true);
                Interact();
                agent.ResetPath();
                animator.SetBool("isWalk", false);
                playerStatus = PlayerStatus.interact;
                
                LookAtTarget(tipInteraction.target);
            }
            else
            {
                isWalking = true;
                FootStep();
                Interact();
                interactionController.SetOffParent(); // dont delete this line
                interactionController.SetInteracted(false); 
                agent.SetDestination(tipInteraction.target);
                animator.SetBool("isWalk", true);
                animator.speed = agent.speed + animSpeed * delayAnimSpeed * Time.deltaTime;
            }
        }
        void FootStep()
        {
            if (Time.time - _lastPlayTime > _footstepPlayRate)
            {
                _lastPlayTime = Time.time;
                    
                AudioManager.Instance.PlaySFX("Footstep 3");
            }
        }

        public void Interact()
        {
            currentInteractable.OnInteraction(!isWalking); //dont delete this line
        }
        
        public void LookAtTarget(Vector3 target)
        {
            Vector3 _lookAtTarget = target - transform.position;
            _lookAtTarget.y = 0;
            Quaternion _rotation = Quaternion.LookRotation(_lookAtTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, Time.deltaTime * 100f);
        }
    }
}