using System.Collections;
using UnityEngine;
using Zenject;

/// <summary>
/// Cube collider.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class CubeCollider : MonoBehaviour
{
    /// <summary>
    /// The sparks pool.
    /// </summary>
    [Inject]
    private SparksPool _sparksPool;

    /// <summary>
    /// When we hit something
    /// </summary>
    /// <param name="collision">Collision.</param>
	private void OnCollisionEnter(Collision collision)
	{
        // If it is a wall and it has not yet been hit before
        var hitWall = collision.transform.GetComponent<WallCollider>();
        if (null != hitWall && !hitWall.Hit)
        {
            // Set the wall as hit!
            hitWall.SetHit();

			// Play VFX on center of collision!
			if (null != _sparksPool)
			{
                var center = GetCenterOfContactPoints(collision.contacts);
                var sparks = _sparksPool.Spawn(center, Quaternion.LookRotation(-Physics.gravity));
                StartCoroutine(DespawnWhenDone(sparks));
			}
        }
	}

    /// <summary>
    /// Gets the center of an array of contact points.
    /// </summary>
    /// <returns>The center of contact points.</returns>
    /// <param name="points">Points.</param>
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

    /// <summary>
    /// Despawns the sparks when they are done playing
    /// </summary>
    /// <returns>The when done.</returns>
    /// <param name="system">System.</param>
	private IEnumerator DespawnWhenDone(ParticleSystem system)
	{
		yield return new WaitForSeconds(system.main.startLifetime.constantMax +
                                        system.main.duration);
		_sparksPool.Despawn(system);
	}
}
