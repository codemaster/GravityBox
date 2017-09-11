using UnityEngine;

/// <summary>
/// Handles rotating the camera and gravity to match
/// </summary>
public class GravityController : MonoBehaviour
{
	/// <summary>
	/// At what speed do movements affect gravity & camera rotation
	/// </summary>
	public float AffectSpeed;

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
