using DeliveryNow.Gameplay;
using Owlet.UI;
using Owlet.UI.Popups;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow
{
    public class PlayerHitbox : MonoBehaviour
    {
        [SerializeField] Rigidbody rb;
        [SerializeField] PlayerController playerController;

        const float EXPLOSION_FORCE = 3; 
        const float EXPLOSION_RADIUS = 1;

        public static Action onCarHit { get; set; }

        public void ResetState()
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (playerController.stateMachine.state is PlayerMainHandler)
            {
                if (collision.gameObject.CompareTag(Keys.Tags.Obstacle))
                {
                    CollideWithObstacle(collision);
                }
            }
        }


        async void CollideWithObstacle(Collision collision)
        {
            onCarHit?.Invoke();
            BaseObstacle obstacle = collision.gameObject.GetComponent<BaseObstacle>();
            playerController.StopCar();
            //rb.AddExplosionForce(EXPLOSION_FORCE, obstacle.transform.position, EXPLOSION_RADIUS);
            rb.isKinematic = false;
            rb.AddTorque(Vector3.forward * -1 * 5f,ForceMode.VelocityChange);
            rb.AddForce((playerController.transform.position - obstacle.transform.position).normalized * 10f, ForceMode.VelocityChange);
            rb.AddForce(Vector3.up * 10f, ForceMode.VelocityChange);
            rb.AddForce(Vector3.right * Mathf.Sign(transform.position.x) * 10f, ForceMode.VelocityChange);
            await PopupManager.instance.OpenUI<Popup>(Keys.Popups.LevelFail, 2);
        }

    }
}
