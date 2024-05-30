using Owlet.StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeliveryNow.Gameplay
{
    public class BaseStatePlayerController : State
    {
        protected PlayerController playerController;

        protected float touchStartTime;
        protected bool isTouching;
        protected bool isHolding;
        protected Vector2 startTouchPosition;

        const float HELD_TIME = 0.5f;

        public BaseStatePlayerController(PlayerController playerController) : base()
        {
            this.playerController = playerController;
        }

        protected virtual void OnTap()
        {
            //Debug.Log("Tapped");
        }

        protected virtual void OnHeld()
        {
            //Debug.Log("Held");
        }

        protected virtual void OnHeldStart()
        {
            //Debug.Log("Held Start");
        }

        protected virtual void OnRelease()
        {
            //Debug.Log("Released");
        }

        protected virtual void OnTouch()
        {
            //Debug.Log("Touched Start");
        }

        public override void Update()
        {
            HandleTouchAndMouseInput();
        }

        private void HandleTouchAndMouseInput()
        {
            // Handle touch input
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        OnTouchStart(touch.position);
                        break;
                    case TouchPhase.Stationary:
                    case TouchPhase.Moved:
                        if (isTouching && !isHolding && Time.time - touchStartTime > HELD_TIME) // Considered as held if touching for more than 0.5 seconds
                        {
                            OnHeldStart();
                            isHolding = true;
                        }
                        if (isTouching && isHolding)
                        {
                            OnHeld();
                        }
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        OnTouchEnd();
                        break;
                }
            }

            // Handle mouse input
            if (Input.GetMouseButtonDown(0))
            {
                OnTouchStart(Input.mousePosition);
            }
            if (Input.GetMouseButton(0))
            {
                if (isTouching && !isHolding && Time.time - touchStartTime > HELD_TIME) // Considered as held if touching for more than 0.5 seconds
                {
                    OnHeldStart();
                    isHolding = true;
                }
                if (isTouching && isHolding)
                {
                    OnHeld();
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                OnTouchEnd();
            }
        }

        private void OnTouchStart(Vector2 position)
        {
            touchStartTime = Time.time;
            isTouching = true;
            isHolding = false;
            startTouchPosition = position;
            OnTouch();
        }

        private void OnTouchEnd()
        {
            if (isTouching)
            {
                OnRelease();
                if (!isHolding)
                {
                    OnTap();
                }
            }
            isTouching = false;
            isHolding = false;
        }
    }
}
