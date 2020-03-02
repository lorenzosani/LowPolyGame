using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateSea : MonoBehaviour
{
    private bool direction;
    private float top = -1.05F;
    private float bottom = -1.15F;
    private Vector3 goalPosition;
    private Vector3 velocity = Vector3.zero;
    public float speed = 1.0F;
    
    void Start() {
        goalPosition.z = transform.position.z;
        goalPosition.x = transform.position.x;
        direction = true;
    }

    void LateUpdate() {
        if(transform.position.y > -1.06 | transform.position.y < -1.14){
            direction = !direction;
        }
        if (direction) { goalPosition.y = top; }
        else { goalPosition.y = bottom; }
        transform.position = Vector3.SmoothDamp(transform.position, goalPosition, ref velocity, speed);
    }
}
