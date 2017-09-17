using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeCollider : MonoBehaviour
{
    public ParticleSystem HitVFX;
    
	private void OnCollisionEnter(Collision collision)
	{
        // We bumped into something so play a sound!
        // TODO: play bump SFX

        // If it is a wall and it has not yet been hit before
        var hitWall = collision.transform.GetComponent<WallCollider>();
        if (null != hitWall && !hitWall.Hit)
        {
            // Set the wall as hit!
            hitWall.SetHit();

			// Play VFX on center of collision!
			if (null != HitVFX)
			{
                var center = GetCenterOfContactPoints(collision.contacts);
				Instantiate(HitVFX, center, Quaternion.LookRotation(-Physics.gravity));
			}
        }
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
