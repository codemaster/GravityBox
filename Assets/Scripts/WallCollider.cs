using UnityEngine;
using Zenject;

/// <summary>
/// Wall collider.
/// </summary>
[RequireComponent(typeof(Renderer))]
public class WallCollider : MonoBehaviour
{
    /// <summary>
    /// If this wall has already been hit or not
    /// </summary>
    /// <value><c>true</c> if hit; otherwise, <c>false</c>.</value>
    public bool Hit { get; private set; }

    /// <summary>
    /// The hit material.
    /// </summary>
    public Material HitMaterial;

    /// <summary>
    /// The cube collider.
    /// </summary>
    [Inject]
    private CubeCollider _cubeCollider;

    /// <summary>
    /// The score tracker.
    /// </summary>
    [Inject]
    private ScoreTracker _scoreTracker;

    /// <summary>
    /// The renderer.
    /// </summary>
    private Renderer _renderer;

    /// <summary>
    /// Initialization
    /// </summary>
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    /// <summary>
    /// Sets that this wall has been hit
    /// </summary>
    public void SetHit()
    {
        // We've been hit!
        Hit = true;

		// Increment our score
		++_scoreTracker.Score;

		// Change our wall to be the fancy new material
		if (HitMaterial != null)
		{
			_renderer.sharedMaterial = HitMaterial;
		}
    }
}
