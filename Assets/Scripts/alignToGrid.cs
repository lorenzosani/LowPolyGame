using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class alignToGrid : MonoBehaviour
{
    private static Vector3 lastPos;
    private static Vector3 truePos;

    void LateUpdate()
    {
        if (this.transform.position != lastPos)
        {
            truePos.x = (float)Math.Round(this.transform.position.x);
            truePos.y = this.transform.position.y;
            truePos.z = (float)Math.Round(this.transform.position.z);

            this.transform.position = truePos;
            lastPos = truePos;
        }
    }
}