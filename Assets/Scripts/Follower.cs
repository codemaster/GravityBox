using UnityEngine;

/// <summary>
/// Class that has this object follow the position of another object with lerping and an optional offset
/// </summary>
public class Follower : MonoBehaviour
{
	/// <summary>
	/// How quickly the follower should lerp to the transform it's following
	/// </summary>
	public float FollowSpeed;

	/// <summary>
	/// Optional offset applied to the transform this object is following
	/// </summary>
	public Vector3 Offset;

	/// <summary>
	/// What this object is following
	/// </summary>
	public Transform Target;

	/// <summary>
	/// The position this object is lerping to
	/// </summary>
	private Vector3 _targetPosition;

	/// <summary>
	/// When this object is created
	/// </summary>
	private void Start()
	{
		// Setup our initial positioning
		LateUpdate();
	}

	/// <summary>
	/// Handle each frame
	/// </summary>
	private void Update()
	{
		// Update this objects position by lerping to the target position
		transform.position = Vector3.Lerp(transform.position, _targetPosition,
			Time.deltaTime * FollowSpeed);
	}

	/// <summary>
	/// Handle each post-frame
	/// </summary>
	private void LateUpdate()
	{
		// Ensure we are following a target
		if (Target == null)
		{
			return;
		}

		// Update the position we are trying to reach
		_targetPosition = Target.position + Offset;
	}

	/// <summary>
	/// Handle changing values in the inspector
	/// </summary>
	private void OnValidate()
	{
		// Ensure our follow speed is at least 1
		FollowSpeed = Mathf.Max(FollowSpeed, 1f);
	}
}
