using UnityEngine;
using Zenject;

/// <summary>
/// Pool for spark effect
/// </summary>
public class SparksPool : MonoMemoryPool<Vector3, Quaternion, ParticleSystem>
{
    /// <summary>
    /// Reinitialize the sparks
    /// </summary>
    /// <returns>The reinitialize.</returns>
    /// <param name="pos">Position.</param>
    /// <param name="rot">Rotation.</param>
    /// <param name="system">Particle system.</param>
    protected override void Reinitialize(Vector3 pos, Quaternion rot, ParticleSystem system)
    {
        base.Reinitialize(pos, rot, system);

        // Set the correct position, rotation
        system.transform.position = pos;
        system.transform.rotation = rot;

        // Replay the particle effect
        system.Clear(true);
        system.Play(true);
    }
}
