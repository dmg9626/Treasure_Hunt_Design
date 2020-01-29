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

    /// <summary>
    /// Reference to map
    /// </summary>
    [SerializeField]
    private Map map;

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

        // Pull map up/down based on mouse scroll input
        if (Input.mouseScrollDelta.y > 0)
            map.SetMap(Map.State.SHOWN);
        else if (Input.mouseScrollDelta.y < 0)
            map.SetMap(Map.State.HIDDEN);
    }

    /// <summary>
    /// Returns reference to currently-controlled Actor (the player)
    /// </summary>
    public Actor GetPlayer()
    {
        return player;
    }
}