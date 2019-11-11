using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;

public class Joystick
{
    private Vector2 axises;
    private float lastAngle;
    private float cumul;
    private int nbCircle;

    public Action OnCircleCompleted;

    public void UpdateAxises(Vector2 axises)
    {
       this.axises = axises;

        float stickAngle = axises.ToAngle();

        if (stickAngle < 0)
        {
            stickAngle += 360;
        }

        if (!float.IsNaN(stickAngle))
        {
            float deltaAngle = stickAngle - lastAngle;
            if (deltaAngle < 0)
            {
                //Clockwise rotation
                cumul += Mathf.Abs(deltaAngle);
            }

            if (cumul > 360)
            {
                cumul = 0;
                ++nbCircle;
                if (OnCircleCompleted != null) OnCircleCompleted();
                
            }

            lastAngle = stickAngle;
        }
    }

    public Vector2 GetAxises()
    {
        return axises;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("(");
        sb.Append(axises.x);
        sb.Append(", ");
        sb.Append(axises.y);
        sb.Append(")");
        return sb.ToString();
    }

    public float GetAngle()
    {
        return axises.ToAngle();
    }

    public int GetNbCircle()
    {
        return nbCircle;
    }
}
