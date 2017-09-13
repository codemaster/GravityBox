using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeCollider : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
        //Debug.Log($"Collision with {collision.gameObject.name} had {collision.contacts.Length} contact points", this);

        var center = GetCenterOfContactPoints(collision.contacts);

        // TODO: Play VFX on center of collision!
	}

    private static Vector3 GetCenterOfContactPoints(ContactPoint[] points)
    {
		Vector3 center = Vector3.zero;
		foreach (var contactPoint in points)
		{
            center += contactPoint.point;
		}
		center /= points.Length;
        return center;
    }
}
