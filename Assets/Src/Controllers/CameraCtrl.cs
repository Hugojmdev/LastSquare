using System;
using UnityEngine;

public class CameraCtrl : MonoBehaviour {
    
    [SerializeField]
    private Transform target; // Reference to the player's transform

    [SerializeField]
    private Vector3 offset;   // Offset from the target position

    [SerializeField]
    private float smoothSpeed = 0.125f; // Speed of camera movement

    [SerializeField]
    private float topGap = 3f;

    [SerializeField]
    private float botGap = 2f;

    [SerializeField]
    private Transform topLimit;

    [SerializeField]
    private Transform botLimit;

    private Vector3 previousPosition;

    private void Start() {
        //Set initial positions for camera following
        transform.position = target.position + offset;
        previousPosition = target.position;
    }

    // Update is called once per frame
    void Update(){
        FollowTarget();
    }

    //Follow the target
    private void FollowTarget() {
        if (target != null) {
            // Calculate the desired position for the camera
            Vector3 targetPosition = target.position;
            targetPosition.x += offset.x;
            targetPosition.y = GetTargetY();
            targetPosition.z = offset.z;

            // Smoothly interpolate between the current position and the desired position
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
        }
    }

    //Gets y position depending on the camera limits
    private float GetTargetY(){
        float y = previousPosition.y + offset.y;

        //Evaluate the distance between target in top limit
        int topDistance = Math.Abs((int)topLimit.position.y - (int)target.position.y);
        //Evaluate the distance between target in bot limit
        int botDistance = Math.Abs((int)botLimit.position.y - (int)target.position.y);

        //Updates the y position if there is a top or bot gap
        if((topDistance <= topGap ) || (botDistance <= botGap)){
            y = target.position.y + offset.y;
            previousPosition = target.position; 
        }
        return y;
    }
}
