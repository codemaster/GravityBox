using UnityEngine;

/// <summary>
/// Handles gryoscopic movements and rotates the camera and gravity to match
/// </summary>
public class GyroController : MonoBehaviour
{
	/// <summary>
	/// Checks if the gyroscope is even supported
	/// </summary>
	public static bool Supported
	{
		get
		{
			return SystemInfo.supportsGyroscope;
		}
	}

	/// <summary>
	/// At what speed does the gyroscope affect gravity & camera rotation
	/// </summary>
	public float GyroAffectSpeed;

	/// <summary>
	/// If we are allowing keyboard control of the gravity manipulation
	/// </summary>
	private bool _allowKeyboardControl;

	/// <summary>
	/// What gravity we are trying to lerp to
	/// </summary>
	private Vector3 _targetGravity;

	/// <summary>
	/// What camera rotation we are trying to lerp to
	/// </summary>
	private Quaternion _targetCameraRotation;

	/// <summary>
	/// How much a single turn will rotate everything in euclidian degrees
	/// </summary>
	private const float QuarterTurn = 90f;

	/// <summary>
	/// When this object is created
	/// </summary>
	private void Start()
	{
		// Setup initial targets
		_targetGravity = Physics.gravity;
		_targetCameraRotation = Camera.main.transform.rotation;

		if (Supported)
		{
			// Enable the gyroscope
			Input.gyro.enabled = true;
		}
		else
		{
			Debug.LogWarning("Gyroscope not supported.", this);
			if (Application.isEditor)
			{
				Debug.Log("Editor testing. Allowing keyboard control.", this);
				_allowKeyboardControl = true;
			}
		}
	}

	/// <summary>
	/// Handle each frame
	/// </summary>
	private void Update()
	{
		// Normal gravity setting
		if (!_allowKeyboardControl)
		{
			var gravityAngleDiff = Vector3.Angle(Physics.gravity, Input.gyro.gravity);
			_targetCameraRotation = Camera.main.transform.rotation *
				Quaternion.Euler(gravityAngleDiff * Vector3.forward);
			_targetGravity = Input.gyro.gravity;
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
			Time.fixedDeltaTime * GyroAffectSpeed);
	}

	/// <summary>
	/// Handle each post-frame
	/// </summary>
	private void LateUpdate()
	{
		Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation,
			_targetCameraRotation, Time.deltaTime * GyroAffectSpeed);
	}

	/// <summary>
	/// Handle changing values in the inspector
	/// </summary>
	private void OnValidate()
	{
		GyroAffectSpeed = Mathf.Max(GyroAffectSpeed, 1f);
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
