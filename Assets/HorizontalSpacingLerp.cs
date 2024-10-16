using UnityEngine;
using UnityEngine.UI;
using System.Collections; 
public class HorizontalSpacingLerp : MonoBehaviour
{
    public HorizontalLayoutGroup horizontalLayoutGroup;  // Reference to the Horizontal Layout Group

    // Call this method to start the Lerp
    public void StartLerpingSpacing(float startSpacing, float endSpacing, float duration)
    {
        StartCoroutine(LerpSpacing(startSpacing, endSpacing, duration));
    }

    // Coroutine to Lerp the spacing
    private IEnumerator LerpSpacing(float startSpacing, float endSpacing, float duration)
    {
        float elapsedTime = 0f;

        // Initialize the spacing to start value
        horizontalLayoutGroup.spacing = startSpacing;

        while (elapsedTime < duration)
        {
            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the new spacing value
            float newSpacing = Mathf.Lerp(startSpacing, endSpacing, elapsedTime / duration);

            // Set the new spacing
            horizontalLayoutGroup.spacing = newSpacing;

            // Wait until the next frame
            yield return null;
        }

        // Ensure the spacing is set to the exact target at the end
        horizontalLayoutGroup.spacing = endSpacing;
    }
}
