using System;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    public event EventHandler<ColliderTriggeredEventArgs> OnColliderTriggered;

    void OnTriggerStay2D(Collider2D collider)
    {
        OnColliderTriggered?.Invoke(this, new ColliderTriggeredEventArgs(collider));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        OnColliderTriggered?.Invoke(this, new ColliderTriggeredEventArgs(collider));
    }
}
