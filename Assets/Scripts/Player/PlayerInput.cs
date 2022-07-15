using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private AnimationCurve MoveCurve;
    [SerializeField] private float SpeedMultiplier = 5f;
    [SerializeField] private float DashSpeedMultiplier = 7f;
    [SerializeField] private float DashDuration = 0.25f;
    [SerializeField] private float DashCooldownDuration = 1f;
    public bool Active { get { return active; } set { active = value; } }
    private bool active;
    private PlayerMovement playerMovement;
    private FirePod frontalFirepod;
    private float horizontalInput;
    private float verticalInput;
    private float dashDurationTimer = 0f;
    private float dashCooldownDurationTimer = 0f;
    private Vector2 direction;
    private Vector2 dashDirection;
    private bool dashActive = false;

    private void Start()
    {
        active = true;
        playerMovement = GetComponent<PlayerMovement>();
        frontalFirepod = GetComponentInChildren<FirePod>();
    }

    private void Update()
    {
        ReadMovementControls();
        ReadFireControls();
    }

    private void FixedUpdate()
    {
        TranslateMovementControls();
    }

    private void ReadFireControls()
    {
        if(active)
        {
            if(Input.GetButton(Constants.FIRE_BUTTON))
            {
                if(frontalFirepod != null)
                {
                    frontalFirepod.Fire();
                }
            }
        }
    }

    private void ReadMovementControls()
    {
        if(dashActive)
        {
            dashDurationTimer -= Time.deltaTime;
            if(dashDurationTimer < 0f)
            {
                dashActive = false;
                dashDurationTimer = 0f;
            }    
        }
        if(dashCooldownDurationTimer > 0f)
        {
            dashCooldownDurationTimer -= Time.deltaTime;
            if(dashCooldownDurationTimer < 0f)
            {
                dashCooldownDurationTimer = 0f;
            }
        }
        if(active)
        {
            horizontalInput = Input.GetAxis(Constants.HORIZONTAL_AXIS);
            verticalInput = Input.GetAxis(Constants.VERTICAL_AXIS);
            if(Input.GetKeyDown(Constants.CONTROLS_DASH) && !dashActive && dashCooldownDurationTimer <= 0f)
            {
                dashActive = true;
                dashDurationTimer = DashDuration;
                dashCooldownDurationTimer = DashCooldownDuration;
                dashDirection.x = horizontalInput;
                dashDirection.y = verticalInput;
            }
        }
        else
        {
            horizontalInput = 0;
            verticalInput = 0;
        }
    }

    private void TranslateMovementControls()
    {
        if(dashActive)
        {
            playerMovement.Move(dashDirection.normalized * DashSpeedMultiplier * SpeedMultiplier);
        }
        else
        {
            direction.x = horizontalInput;
            direction.y = verticalInput;
            playerMovement.Move(direction * MoveCurve.Evaluate(direction.magnitude) * SpeedMultiplier);
        }
    }
}
