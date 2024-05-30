using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
namespace Owlet.UI
{
    public class NotificationCanvas : Singleton<NotificationCanvas>
    {
        [SerializeField] CanvasGroup textCanvas;
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] Transform defaultPos;

        Sequence closeSequence;
        private void Start()
        {
            closeSequence = DOTween.Sequence()
                        .SetAutoKill(false)
                        .Append(textCanvas.DOFade(1, .2f))
                        .AppendInterval(1.5f)
                        .Append(textCanvas.DOFade(0, .2f))
                        .Pause();
        }

        public void Show(string str, Transform pos = null)
        {
            text.SetText(str);
            text.transform.position = pos == null ? defaultPos.position : pos.position;
            closeSequence.Restart();
        }
    }
}
