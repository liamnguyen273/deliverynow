using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace DeliveryNow.Gameplay
{
    /// <summary>
    /// Handle all Player controller
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public PlayerStateMachine stateMachine;
        [SerializeField] SplineAnimate splineAnimate;
        Rigidbody rb;
        public Transform body;

        public static Action<PlayerController> onPlayerDataLoaded;

        const float SPEED_DEFAULT = 15f;
        const float BODY_OFFSET = 2.1f;
        const float ROTATE_ANGLE = 10f;

        const float ROTATE_TIME = 0.3f;
        const float MOVE_TIME = 0.3f;

        const float BRAKE_TIME = 0.3f;

        bool isRightLane = true;
        bool isOccupied = false;

        Tweener tweenerSpeed;

        public void Initialize()
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            body.transform.localRotation = Quaternion.Euler(Vector3.zero);

            isRightLane = true;
            isOccupied = false;

            splineAnimate.Restart(false);
            UpdatePathSpeed(0.001f);
            splineAnimate.Pause();
            body.transform.localPosition = new Vector3(BODY_OFFSET, 0, 0);

            splineAnimate.Updated += CheckFinishLineReached;

            onPlayerDataLoaded?.Invoke(this);
        }

        private void OnDisable()
        {
            splineAnimate.Updated -= CheckFinishLineReached;
        }

        private void CheckFinishLineReached(Vector3 vector, Quaternion quaternion)
        {
            float3 lastKnot = splineAnimate.Container.Splines[0].Knots.Last().Position;
            Vector3 destination = new Vector3(lastKnot.x, lastKnot.y, lastKnot.z) + splineAnimate.Container.transform.position;
            if (Vector3.Distance(vector, destination) < 0.01f)
            {
                CompleteLevel();
            }
        }

        void CompleteLevel()
        {
            Debug.Log("Level Complete");
            splineAnimate.Updated -= CheckFinishLineReached;
            stateMachine.SetState(new PlayerIdle(this));
            GameManager.instance.CompleteLevel();
        }

        public void ChangeLane()
        {
            if(isOccupied) return;
            Vector3 newPosition = isRightLane ? new Vector3(-BODY_OFFSET, 0, 0) : new Vector3(BODY_OFFSET, 0, 0);
            Vector3 rotateAngle = isRightLane ? new Vector3(0, -ROTATE_ANGLE, 0) : new Vector3(0, ROTATE_ANGLE, 0);

            Sequence chaneLaneSequence = DOTween.Sequence()
                .AppendCallback(() => { isOccupied = true; })
                .Append(body.DOLocalRotate(rotateAngle, ROTATE_TIME))
                .Join(body.DOLocalMove(newPosition, MOVE_TIME))
                .Append(body.DOLocalRotate(Vector3.zero, ROTATE_TIME))
                .JoinCallback(() => { isOccupied = false; });


            isRightLane = !isRightLane;
        }

        [Button]
        public void StartCar()
        {
            UpdatePathSpeed(SPEED_DEFAULT);
            splineAnimate.Play();
        }

        public void StopCar()
        {
            UpdatePathSpeed(0.001f);
            splineAnimate.Pause();
        }

        public void ResetSpeed()
        {
            tweenerSpeed.Kill();
            UpdatePathSpeed(SPEED_DEFAULT);
        }

        public void Brake()
        {
            tweenerSpeed.Kill();
            tweenerSpeed = DOVirtual.Float(SPEED_DEFAULT, 0.001f, BRAKE_TIME, (float value) =>
            {
                UpdatePathSpeed(value);
            });
        }

        public void StartEngine()
        {
            tweenerSpeed.Kill();
            tweenerSpeed = DOVirtual.Float(0.001f, SPEED_DEFAULT, BRAKE_TIME, (float value) =>
            {
                UpdatePathSpeed(value);
            });
        }

        private void UpdatePathSpeed(float newSpeed)
        {
            float prevProgress = splineAnimate.NormalizedTime;
            splineAnimate.MaxSpeed = newSpeed;
            splineAnimate.NormalizedTime = prevProgress;
        }
    }
}
