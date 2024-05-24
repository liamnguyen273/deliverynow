using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Owlet.UI.Popups
{
    [RequireComponent(typeof(Popup))]
    public class PopupHandler : MonoBehaviour
    {
        protected Popup popup;

        protected virtual void OnEnable()
        {
            if (popup == null)
                popup = GetComponent<Popup>();
            popup.onOpened += EnableAnimation;
        }

        protected virtual void OnDisable()
        {
            popup.onOpened -= EnableAnimation;
        }


        void EnableAnimation()
        {
            StartCoroutine(EnableAnim());
        }

        protected virtual IEnumerator EnableAnim()
        {
            yield break;
        }
    }
}
