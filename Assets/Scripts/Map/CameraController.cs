using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    private Vector3 offset = new Vector3(0, 0, -10);
    public Camera mainCamera;
    public void ResetCameraPosition()
    {
        Vector3 targetPosition = player.position + offset;
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        transform.position = targetPosition;
    }
    // Start is called before the first frame update
    
    public void SetCameraBounds(float newMinX, float newMinY, float newMaxX, float newMaxY)
    {
        minX = newMinX;
        minY = newMinY;
        maxX = newMaxX;
        maxY = newMaxY;
    }
    public void SetCameraSize(float newSize)
    {
        mainCamera.orthographicSize = newSize;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + offset;
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
            transform.position = targetPosition;
        }
    }
}
