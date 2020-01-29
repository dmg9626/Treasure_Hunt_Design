using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : Singleton<PlayerController>
{
    /// <summary>
    /// Actor currently controlled by the player
    /// </summary>
    [SerializeField]
    private Actor player;

    private void Awake()
    {
        // Make sure we have an actor (default to oliver if not specified)
        if(player == null) {
            Debug.LogError(name + " | Missing reference to Player actor");
        }
    }

    private void Update()
    {
        // Update camera perspective
        PerspectiveController.Instance.UpdatePerspective(player);

        // Update camera to follow player actor
        CameraController.Instance.CameraUpdate(player);
    }

    /// <summary>
    /// Returns reference to currently-controlled Actor (the player)
    /// </summary>
    public Actor GetPlayer()
    {
        return player;
    }
}