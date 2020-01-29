using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    #region components

    /// <summary>
    /// Used for applying player movement
    /// </summary>
    protected Rigidbody rigidbody;

    // TODO: move to a camera-related class
    /// <summary>
    /// 3D perspective camera 
    /// </summary>
    public PerspectiveCameraControl ghostCamera { get; private set;}

    #endregion

    [Header("Movement Settings")]
    /// <summary>
    /// Base movement speed
    /// </summary>
    [SerializeField]
    protected float movementSpeed = 10f;

    /// <summary>
    /// Speed multiplier when sprinting
    /// </summary>
    [SerializeField]
    protected float sprintMultiplier = 1.5f;

    /// <summary>
    /// Used to generate Actor movement - varies depending on current camera perspective, or assigned NPC behavior
    /// </summary>
    public Movement movement;

    /// <summary>
    /// Sets actor movement control scheme
    /// </summary>
    /// <param name="movement">Movement control scheme</param>
    public void SetMovement(Movement movement)
    {
        this.movement = movement;
    }

    /// <summary>
    /// Current perspective used for this actor (remains unchanged when not player-controlled)
    /// </summary>
    /// <value></value>
    public Perspective perspective;
    
    void Awake()
    {
        // Get components in Awake() before other scripts request them in Start()
        rigidbody = GetComponent<Rigidbody>();
        ghostCamera = GetComponentInChildren<PerspectiveCameraControl>();
    }

    private void Start()
    {
        // TODO: make sure we only set initial perspective in one place
        // Default to third-person perspective for both actors
        perspective = PerspectiveController.Instance.GetPerspectiveByType(CameraController.CameraViews.THIRD_PERSON);
        movement = perspective.movement;
    }

    private void FixedUpdate()
    {
        // Move actor
        Move();
    }

    /// <summary>
    /// Applies movement, returns vector representing movement
    /// </summary>
    /// <returns>Vector of movement</returns>
    private Vector3 Move()
    {
        // Get move direction (as unit vector) from movement class
        Vector3 direction = movement.GetMovement(this);

        // Apply movement (scaled by movement speed)
        float magnitude = movementSpeed * Time.fixedDeltaTime;

        // Apply speed multiplier if sprinting
        if(Input.GetKey(KeyCode.LeftShift)) {
            magnitude *= sprintMultiplier;
        }

        // Apply movement
        Vector3 moveVector = direction * magnitude;
        rigidbody.MovePosition(transform.position + moveVector);

        // Return movement
        return moveVector;
    }
}