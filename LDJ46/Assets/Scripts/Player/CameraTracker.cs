using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
/// <summary>
/// Script to be located on the camera GameObject
/// </summary>
public class CameraTracker : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float cameraSpeed;

    private void FixedUpdate()
    {
        //Direction where the camera ahs to move to
        Vector2 cameraMoveDir = (target.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance > 0)
        {
            Vector3 newCameraPos = transform.position + (Vector3)cameraMoveDir * (distance * cameraSpeed * Time.deltaTime);
            float distanceAfterMoving = Vector3.Distance(newCameraPos, target.transform.position);
            //Save check if game has low framerate
            if (distanceAfterMoving > distance)
            {
                newCameraPos = target.transform.position;
            }
            //Always z = -1 in order to have in frustum the scene elements
            transform.position = new Vector3(newCameraPos.x,newCameraPos.y,-1f);
        }
    }
}
