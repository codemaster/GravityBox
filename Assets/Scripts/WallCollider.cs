﻿using UnityEngine;
using Zenject;

[RequireComponent(typeof(Renderer))]
public class WallCollider : MonoBehaviour
{
    public bool Hit { get; private set; }
    public Material HitMaterial;
    
    [Inject]
    private CubeCollider _cubeCollider;
    [Inject]
    private ScoreTracker _scoreTracker;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ensure it's our cube collider
        // Ignore if we've been hit already
        if (other.gameObject != _cubeCollider.gameObject || Hit)
        {
            return;
        }

        // We've been hit
        Hit = true;

        // TODO: Play capture VFX

        // Increment our score
        ++_scoreTracker.Score;

        // Change our wall to be the fancy new material
        if (HitMaterial != null)
        {
            _renderer.sharedMaterial = HitMaterial;
        }
    }
}
