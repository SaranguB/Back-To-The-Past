using System;
using UnityEngine;

namespace Level
{
    public class LevelView : MonoBehaviour
    {
        private LevelController levelController;

        public GameObject key;
        public Animator finalDoorAnimation;

        public void SetController(LevelController levelController)
        {
            this.levelController = levelController;
        }


        public void DestroyKey()
        {
            Destroy(key.gameObject);
        }

        public void OpenFinalDoor()
        {
            finalDoorAnimation.enabled = true;
        }

        public void LevelFInished()
        {
            OpenFinalDoor();
        }

        public void PlayerGotKey()
        {
            DestroyKey();
        }
    }
}
