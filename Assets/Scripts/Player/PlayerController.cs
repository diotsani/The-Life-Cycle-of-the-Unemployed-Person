using System;
using System.Collections;
using System.Collections.Generic;
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

        public PlayerStatus playerStatus;
        public NavMeshAgent agent;
        public Animator animator;
        public Tooltip tooltip;
        public float stopDistance;
        
        private void FixedUpdate()
        {
            switch (playerStatus)
            {
                case PlayerStatus.walk:
                    Walk();
                    break;
                case PlayerStatus.interact:
                    Interact();
                    break;
                default:
                    playerStatus = PlayerStatus.idle;
                    break;
            }
        }

        public void Walk()
        {
            if (Vector3.Distance(transform.position, tooltip.target) <= stopDistance)
            {
                agent.ResetPath();
                animator.SetBool("isWalk", false);

                playerStatus = PlayerStatus.interact;
                
                LookAtTarget(tooltip.target);
            }
            else
            {
                agent.SetDestination(tooltip.target);
                animator.SetBool("isWalk", true);
            }
        }

        public void Interact()
        {
            Debug.Log("Interaction");
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