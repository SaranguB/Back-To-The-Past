using System;
using UnityEngine;

namespace Wepons.Bomb
{
    public class BombView : MonoBehaviour
    {
        private BombController bombController;

        public Animator bombAnimator;
        public BombDamageArea damageArea;
        public CapsuleCollider2D damageAreaCollider;

        private void Start()
        {
            bombAnimator = GetComponent<Animator>();
        }

        public void SetController(BombController bombController)
        {
            this.bombController = bombController;
        }


        public void ConfigurePosition(Transform bombPosition)
        {
            this.transform.position = bombPosition.position;
        }

        public void OnExplosionEnd()
        {

            bombController.SetBombState(BombState.Exploded);
        }
    }
}
