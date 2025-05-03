using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;  
    public Vector3 offset = new Vector3(0f, 5f, -10f); 
    public float smoothSpeed = 5f;  
    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;

        
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

      
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}