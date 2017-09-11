using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeCollider : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("Colliding with: " + collision.gameObject.name);
	}
}
