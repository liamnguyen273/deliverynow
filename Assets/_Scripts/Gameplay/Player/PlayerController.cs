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
        [SerializeField] PlayerHitbox playerHitbox;
        public Transform body;

        public static Action<PlayerController> onPlayerDataLoaded;
        public static Action<float> onProgressUpdate;

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
            playerHitbox.ResetState();
            body.transform.localRotation = Quaternion.Euler(Vector3.zero);

            isRightLane = true;
            isOccupied = false;

            splineAnimate.Restart(false);
            UpdatePathSpeed(0.001f);
            splineAnimate.Pause();
            body.transform.localPosition = new Vector3(BODY_OFFSET, 0, 0);

            splineAnimate.Updated += CheckFinishLineReached;
            PlayerHitbox.onCarHit += EndControl;

            onProgressUpdate?.Invoke(0f);
            onPlayerDataLoaded?.Invoke(this);
        }

        public void ResetStateToContinue()
        {
            playerHitbox.ResetState();
            body.transform.localRotation = Quaternion.Euler(Vector3.zero);

            isRightLane = true;
            isOccupied = false;

            body.transform.localPosition = new Vector3(BODY_OFFSET, 0, 0);

            splineAnimate.Updated += CheckFinishLineReached;
            PlayerHitbox.onCarHit += EndControl;

            splineAnimate.NormalizedTime = Mathf.Clamp(splineAnimate.NormalizedTime - 0.05f, 0, 1);

            onProgressUpdate?.Invoke(splineAnimate.NormalizedTime);
            onPlayerDataLoaded?.Invoke(this);
        }

        public void EndControl()
        {
            splineAnimate.Updated -= CheckFinishLineReached;
            PlayerHitbox.onCarHit -= EndControl;
            DOTween.Kill(gameObject);
            stateMachine.SetState(new PlayerIdle(this));
        }

        private void OnDisable()
        {
            StopCar();
            EndControl();
        }

        private void CheckFinishLineReached(Vector3 vector, Quaternion quaternion)
        {
            onProgressUpdate?.Invoke(splineAnimate.NormalizedTime);

            if (splineAnimate.NormalizedTime == 1f)
            {
                CompleteLevel();
            }
        }

        void CompleteLevel()
        {
            //Debug.Log("Level Complete");
            EndControl();
            StopCar();
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
                .JoinCallback(() => { isOccupied = false; })
                .SetTarget(gameObject);


            isRightLane = !isRightLane;
        }

        #region Car Engine
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
            }).SetTarget(gameObject);
        }

        public void StartEngine()
        {
            tweenerSpeed.Kill();
            tweenerSpeed = DOVirtual.Float(0.001f, SPEED_DEFAULT, BRAKE_TIME, (float value) =>
            {
                UpdatePathSpeed(value);
            }).SetTarget(gameObject);
        }

        private void UpdatePathSpeed(float newSpeed)
        {
            float prevProgress = splineAnimate.NormalizedTime;
            splineAnimate.MaxSpeed = newSpeed;
            splineAnimate.NormalizedTime = prevProgress;
        }
        #endregion
    }
}
