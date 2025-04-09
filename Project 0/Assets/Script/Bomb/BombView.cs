using System;
using UnityEngine;

namespace Wepons.Bomb
{
    public class BombView : MonoBehaviour
    {
        private BombController bombController;
        public void SetController(BombController bombController)
        {
         this.bombController = bombController;   
        }

        public void ConfigurePosition(Transform bombPosition)
        {
            this.transform.position = bombPosition.position;
        }
    }
}
