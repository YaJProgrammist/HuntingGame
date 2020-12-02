using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Animal : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    protected List<Vector2> velocities;
    protected float currentSpeed;
    public event EventHandler<AnimalRemovedEventArgs> OnAnimalRemoved;

    public void Remove()
    {
        OnAnimalRemoved?.Invoke(this, new AnimalRemovedEventArgs(this));
        Destroy(this.gameObject);
    }

    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        velocities = new List<Vector2>();
    }

    protected virtual void Update()
    {
        AdjustVelocity();
        velocities.Clear();
    }

    private void AdjustVelocity()
    {
        Vector2 averageVelocity = new Vector2(0, 0);

        if (velocities.Count > 0)
        {
            foreach (Vector2 velocity in velocities)
            {
                averageVelocity += velocity.normalized;
            }

            averageVelocity /= velocities.Count;
        }

        rigidbody.velocity = averageVelocity * currentSpeed;
        LookInDirection(averageVelocity);
    }

    private void LookInDirection(Vector2 direction)
    {
        this.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }
}
