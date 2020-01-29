using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class Map : MonoBehaviour
{
    /// <summary>
    /// Pivot point used for rotation (along X-axis) when raising/lowering map
    /// </summary>
    [SerializeField] private Transform pivot;

    /// <summary>
    /// Map rotation when pulled up
    /// </summary>
    [SerializeField] private float mapUpPosition;

    /// <summary>
    /// Map rotation when put away
    /// </summary>
    [SerializeField] private float mapDownPosition;

    /// <summary>
    /// Duration of map animation
    /// </summary>
    [SerializeField] private float transitionDuration;

    public enum State { HIDDEN, SHOWN }

    /// <summary>
    /// Tells whether map is currently shown or hidden
    /// </summary>
    private State state = State.SHOWN;

    // Start is called before the first frame update
    void Start()
    {
        if (!pivot)
            Debug.LogError(name + " | missing pivot point!");
    }

    public void SetMap(State newState)
    {
        // Return if already moving map, or map is already in specified state
        if (bringMapCoroutine != null)
            return;
        else if (state.Equals(newState))
        {
            return;
            Debug.Log("Already in requested state " + newState);
        }

        // Show/hide map
        if (newState.Equals(State.SHOWN))
            bringMapCoroutine = StartCoroutine(RotateMap(mapUpPosition));
        else
            bringMapCoroutine = StartCoroutine(RotateMap(mapDownPosition));
        
        // Update state
        state = newState;
    }


    /// <summary>
    /// Instance of BringMap
    /// </summary>
    private Coroutine bringMapCoroutine;

    /// <summary>
    /// Rotate map pivot towards given rotation
    /// </summary>
    /// <param name="targetRotation">Rotation to pivot map towards</param>
    /// <returns></returns>
    private IEnumerator RotateMap(float targetRotation)
    {
        // Save starting rotation
        float startRotation = pivot.localEulerAngles.x;

        // Rotate with hermite curve
        float t = 0; while(t < 1)
        {
            float newRotation = Mathfx.Hermite(startRotation, targetRotation, t);
            pivot.eulerAngles = new Vector3(newRotation, pivot.eulerAngles.y, pivot.eulerAngles.z);

            t += Time.deltaTime / transitionDuration;
            yield return null;
        }
        // Make sure we get there
        pivot.eulerAngles = new Vector3(targetRotation, pivot.eulerAngles.y, pivot.eulerAngles.z);

        // Set coroutine to null so it can be called again
        bringMapCoroutine = null;
    }
}
