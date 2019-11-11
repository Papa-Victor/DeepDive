using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;


public class NotificationManager : SingletonBehavior<NotificationManager>
{
    [SerializeField]
    private float notificationTime = 1.0f;

    [SerializeField]
    private float notificationSpeed = 2.0f;

    [SerializeField]
    private float notificationOffset = 15.0f;

    private bool notificationSent;

    private Queue<string> notificationQueue;

    protected override void Awake()
    {
          base.Awake();
          notificationQueue = new Queue<string>();      
    }

    void FixedUpdate()
    {
        if (!NotificationQueueEmpty() && !notificationSent)
        {
            notificationSent = true;
            string message = notificationQueue.Dequeue();
            Debug.Log("Sending notification : " + message);
            sendNotificationMessage(message);
        }
    }

    public void QueueNotificationMessage(string message)
    {
        notificationQueue.Enqueue(message);
    }

    private bool NotificationQueueEmpty()
    {
        return notificationQueue.Count == 0;
    }

    private void sendNotificationMessage(string message)
    {
        RectTransform rt = Instance.GetComponent<RectTransform>();
        TextMeshProUGUI text = Instance.GetComponentInChildren<TextMeshProUGUI>();
        text.text = message;
        Sequence sq = DOTween.Sequence();
        sq.Append(rt.DOMoveX(rt.position.x - rt.rect.width - notificationOffset, notificationSpeed).SetEase(Ease.InSine));
        sq.AppendInterval(notificationTime);
        sq.Append(rt.DOMoveX(rt.position.x, notificationSpeed).SetEase(Ease.OutSine));
        sq.onComplete += () => 
        {
            notificationSent = false;
        };
        sq.Play();
    }
}
