using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC;

public class William_TestScript : MonoBehaviour
{
    private Joystick joystick;

    [SerializeField]
    private SceneInfo sceneInfo;

    void Start()
    {
        if (sceneInfo)
        {
            Scenes.Load(sceneInfo);
        }        
        this.DelayedCall( () => {
            NotificationManager.Instance.QueueNotificationMessage("This is a notification sent by William");
            NotificationManager.Instance.QueueNotificationMessage("This is a second notification sent by William");
        }, 1);
        // joystick = new Joystick();
        // joystick.OnCircleCompleted += () => 
        // {
        //     Debug.Log(joystick.GetNbCircle() + " circle(s) completed");
        // };
    }

    void Update()
    {
        // joystick.UpdateAxises(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }

    void FixedUpdate()
    {
        
    }
}
