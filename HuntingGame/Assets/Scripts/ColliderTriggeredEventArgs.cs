using UnityEngine;

public class ColliderTriggeredEventArgs
{
    public Collider2D Collider { get; private set; }

    public ColliderTriggeredEventArgs(Collider2D collider)
    {
        Collider = collider;
    }
}
