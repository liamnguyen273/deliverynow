using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

namespace Owlet.UI.Buttons
{
    public class ToggleSwitch : MonoBehaviour
    {
        [SerializeField] float off_x_value;
        [SerializeField] float on_x_value;
        [SerializeField] GameObject slider;
        [SerializeField] GameObject onSwitch;

        public Action<bool> onClicked;
        bool on;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Switch);
        }

        public void Switch()
        {
            on = !on;

            if (on)
            {
                slider.transform.DOLocalMoveX(on_x_value, 0.1f);
                onSwitch.SetActive(true);
            }
            else
            {
                slider.transform.DOLocalMoveX(off_x_value, 0.1f);
                onSwitch.SetActive(false);
            }

            onClicked.Invoke(on);
        }

        public void SetState(bool state)
        {
            on = state;
            if (on)
            {
                slider.transform.localPosition = new Vector3(on_x_value, slider.transform.localPosition.y, slider.transform.localPosition.z);
                onSwitch.SetActive(true);
            }
            else
            {
                slider.transform.localPosition = new Vector3(off_x_value, slider.transform.localPosition.y, slider.transform.localPosition.z);
                onSwitch.SetActive(false);
            }
        }
    }
}
