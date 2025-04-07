using System;
using UnityEngine;

namespace TimeSwitching
{
    public class TimeAffectedObjectController : MonoBehaviour
    {
        [SerializeField] private TimeAffectedObjectsSO objectData;

        private Rigidbody2D objectRB;
        private SpriteRenderer objectSprite;
        private Collider2D objectCollider;

        private void Awake()
        {
            objectRB = GetComponent<Rigidbody2D>();
            objectSprite = GetComponent<SpriteRenderer>();
            objectCollider = GetComponent<Collider2D>();
        }

        public void SetPastProperties()
        {
            objectRB.bodyType = objectData.isMovableInPast ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
            objectSprite.sprite = objectData.pastSprite;
            Updatecollider();
        }

        public void SetPresentProperties()
        {
  
            objectRB.bodyType = objectData.isMovableInPresent ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
            objectSprite.sprite = objectData.presentSprite;
            Updatecollider();
        }

        private void Updatecollider()
        {

            if (objectSprite == null || objectCollider == null)
            {
                Debug.Log("null");
                return;

            }

            if (objectCollider is BoxCollider2D boxCollider)
            {
                boxCollider.size = objectSprite.sprite.bounds.size;
                boxCollider.offset = objectSprite.sprite.bounds.center;
            }
        }


    }
}

