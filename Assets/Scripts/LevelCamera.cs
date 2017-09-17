using UnityEngine;

/// <summary>
/// The camera for a level
/// </summary>
[RequireComponent(typeof(Follower))]
public class LevelCamera : MonoBehaviour
{
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:LevelCamera"/> is enabled.
    /// </summary>
    /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
    public bool Enabled
    {
        get
        {
            return null != _followerComponent && _followerComponent.enabled;
        }

        set
        {
            if (null != _followerComponent)
            {
                _followerComponent.enabled = value;
            }
        }
    }
    
    /// <summary>
    /// The follower component
    /// </summary>
    private Follower _followerComponent;

    /// <summary>
    /// When the camera is initially created
    /// </summary>
    private void Awake()
    {
        // Turn off the follower component at first
        _followerComponent = GetComponent<Follower>();
        if (null != _followerComponent)
        {
            _followerComponent.enabled = false;
        }
    }
}
