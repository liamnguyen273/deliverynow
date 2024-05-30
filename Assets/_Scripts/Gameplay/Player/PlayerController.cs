using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace DeliveryNow.Gameplay
{
    /// <summary>
    /// Handle all Player controller
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] SplineAnimate splineAnimate;
        [SerializeField] Transform body;

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
            isRightLane = true;
            isOccupied = false;

            body.transform.position = new Vector3(BODY_OFFSET, 0, 0);
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

        public void ResetSpeed()
        {
            tweenerSpeed.Kill();
            UpdatePathSpeed(SPEED_DEFAULT);
        }

        public void StopMoving()
        {
            tweenerSpeed.Kill();
            tweenerSpeed = DOVirtual.Float(SPEED_DEFAULT, 0.001f, BRAKE_TIME, (float value) =>
            {
                UpdatePathSpeed(value);
            });
        }

        public void StartMoving()
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
