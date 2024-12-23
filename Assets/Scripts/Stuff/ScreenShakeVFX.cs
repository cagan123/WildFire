using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeVFX : MonoBehaviour
{
    private Vector3 originalPosition;
    private Coroutine shakeCoroutine;

    private void Start()
    {
        // Store the camera's original position
        originalPosition = transform.localPosition;
    }

    /// <summary>
    /// Triggers a screenshake effect.
    /// </summary>
    /// <param name="duration">The duration of the shake in seconds.</param>
    /// <param name="magnitude">The magnitude of the shake (how far the camera moves).</param>
    public void Shake(float duration, float magnitude)
    {
        // Stop any ongoing shake coroutine
        if (shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);

        // Start a new shake coroutine
        shakeCoroutine = StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Generate a random offset
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            // Apply the offset to the camera
            transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0f);

            // Increment elapsed time
            elapsed += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Reset the camera to its original position
        transform.localPosition = originalPosition;
        shakeCoroutine = null;
    }
}
