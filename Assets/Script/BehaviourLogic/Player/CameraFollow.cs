using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(-54.2f, 69f, -49.6f); // Adjust the offset for desired position
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate desired position based on target's position and the offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly transition to the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Adjust camera's rotation to fit your view angle (change the angles to match your setup)
        transform.rotation = Quaternion.Euler(35f, 45f, 0f);
    }
}
