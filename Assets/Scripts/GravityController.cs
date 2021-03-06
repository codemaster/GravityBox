﻿using UnityEngine;

/// <summary>
/// Handles rotating the camera and gravity to match
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class GravityController : MonoBehaviour
{
	/// <summary>
	/// At what speed do movements affect gravity & camera rotation
	/// </summary>
	public float AffectSpeed;

    /// <summary>
    /// If the gravity controller is enabled and allowed to change gravity!
    /// </summary>
    public bool Enabled
    {
        get
        {
            return _enabled;
        }
        set
        {
            if (null != _rigidBodyComponent)
            {
                _rigidBodyComponent.useGravity = value;
                _rigidBodyComponent.isKinematic = !value;
            }

            _enabled = value;
        }
    }

	/// <summary>
	/// What gravity we are trying to lerp to
	/// </summary>
	private Vector3 _targetGravity;

	/// <summary>
	/// What camera rotation we are trying to lerp to
	/// </summary>
	private Quaternion _targetCameraRotation;

    /// <summary>
    /// The rigidbody component
    /// </summary>
    private Rigidbody _rigidBodyComponent;

    /// <summary>
    /// If the gravity controller is enabled or not
    /// </summary>
    private bool _enabled;

    /// <summary>
    /// Helper to save the default gravity
    /// </summary>
    private static Vector3? DefaultGravity;

	/// <summary>
	/// How much a single turn will rotate everything in euclidian degrees
	/// </summary>
	private const float QuarterTurn = 90f;

	/// <summary>
	/// When this object is created
	/// </summary>
	private void Start()
	{
        // If we don't have a default gravity, let's get one setup now
        if (!DefaultGravity.HasValue)
        {
            DefaultGravity = Physics.gravity;
        }
        else
        {
            // Set gravity to the default
            Physics.gravity = DefaultGravity.Value;
        }
        
        // Turn off gravity initially
        _rigidBodyComponent = GetComponent<Rigidbody>();
        Enabled = false;

		// Setup initial targets
		_targetGravity = Physics.gravity;
		_targetCameraRotation = Camera.main.transform.rotation;
	}

	/// <summary>
	/// Handles a button press that rotates gravity clockwise
	/// </summary>
	public void RotateGravity()
	{
		RotateGravity(true);
	}

	/// <summary>
	/// Handle each frame
	/// </summary>
	private void Update()
	{
        // Check if this controller is enabled
        if (!Enabled)
        {
            return;
        }
        
		// Keyboard - rotate gravity
		if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
		{
			RotateGravity(true);
		}
		else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
		{
			RotateGravity(false);
		}
	}

	/// <summary>
	/// Handle each fixed update tick
	/// </summary>
	private void FixedUpdate()
	{
		Physics.gravity = Vector3.Slerp(Physics.gravity, _targetGravity,
			Time.fixedDeltaTime * AffectSpeed);
	}

	/// <summary>
	/// Handle each post-frame
	/// </summary>
	private void LateUpdate()
	{
		Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation,
			_targetCameraRotation, Time.deltaTime * AffectSpeed);
	}

	/// <summary>
	/// Handle changing values in the inspector
	/// </summary>
	private void OnValidate()
	{
		AffectSpeed = Mathf.Max(AffectSpeed, 1f);
	}

	/// <summary>
	/// Rotates gravity
	/// </summary>
	/// <param name="right">Are we rotating to the right or the left?</param>
	private void RotateGravity(bool right)
	{
		var amount = right ? QuarterTurn : -QuarterTurn;
		_targetCameraRotation = _targetCameraRotation *
			Quaternion.Euler(amount * Vector3.forward);
		_targetGravity = Quaternion.Euler(0f, 0f, amount) * _targetGravity;
	}
}
