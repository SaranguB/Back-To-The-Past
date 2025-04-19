using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using Trap;
using UnityEngine;

namespace Trap
{
    public class TrapView : MonoBehaviour
    {
        private TrapController trapController;
        public TrapSO trapData;
        public Collider2D trapCollider;
        public Rigidbody2D trapRigidBody;
        public Animator trapAnimator;


        public void SetController(TrapController trapController)
        {
            this.trapController = trapController;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<PlayerView>() != null)
            {
                if (trapController != null)
                    trapController.ActivateTrap();
            }
        }

        public void ActivateFallingPlatform()
        {
            StartCoroutine(disableFallingPlatform());
        }

        private IEnumerator disableFallingPlatform()
        {
            yield return new WaitForSeconds(trapData.trapDelay);

            if (trapAnimator != null)
                trapAnimator.enabled = false;

            if (trapRigidBody != null)
                trapRigidBody.bodyType = RigidbodyType2D.Dynamic;

        }
    }
}