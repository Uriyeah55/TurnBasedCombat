using UnityEngine;

public class CameraTransitionManager : MonoBehaviour
{
    public Camera[] cameras;   // Array of cameras
    public float transitionSpeed = 2.0f;  // Speed of transition
    private bool isTransitioning = false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float transitionProgress = 0f;

    void Update()
    {
        if (isTransitioning)
        {
            transitionProgress += Time.deltaTime * transitionSpeed;

            // Interpolate position and rotation
            transform.position = Vector3.Lerp(initialPosition, targetPosition, transitionProgress);
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, transitionProgress);

            // Stop transitioning once we reach the target
            if (transitionProgress >= 1f)
            {
                isTransitioning = false;
            }
        }
    }

    public void StartCameraTransition(int fromIndex, int toIndex)
    {
        // Ensure indices are valid
        if (fromIndex < 0 || fromIndex >= cameras.Length || toIndex < 0 || toIndex >= cameras.Length)
        {
            Debug.LogError("Invalid camera indices.");
            return;
        }

        // Set the initial and target positions/rotations based on the camera array
        initialPosition = cameras[fromIndex].transform.position;
        initialRotation = cameras[fromIndex].transform.rotation;
        targetPosition = cameras[toIndex].transform.position;
        targetRotation = cameras[toIndex].transform.rotation;

        // Start transitioning
        transitionProgress = 0f;
        isTransitioning = true;
    }
}
