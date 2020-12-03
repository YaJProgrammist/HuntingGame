using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Animal : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private bool isWondering;
    protected bool isAccelerated;
    private System.Random rand;
    protected List<Vector2> velocities;
    protected float currentSpeed;
    public event EventHandler<AnimalRemovedEventArgs> OnAnimalRemoved;

    void Awake()
    {
        rand = new System.Random();
        velocities = new List<Vector2>();
        isAccelerated = false;
    }

    public void Remove()
    {
        OnAnimalRemoved?.Invoke(this, new AnimalRemovedEventArgs(this));
        Destroy(this.gameObject);
    }

    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        StartWondering();
    }

    protected virtual void Update()
    {
        if (isAccelerated == true)
        {
            isWondering = false;
            AdjustVelocity();
            velocities.Clear();
            isAccelerated = false;
            return;
        }

        if (isWondering)
        {
            ContinueWondering();
            velocities.Clear();
            return;
        }

        StartWondering();
    }

    private void AdjustVelocity()
    {
        Vector2 averageVelocity = GetAverageVelocity();

        rigidbody.velocity = (rigidbody.velocity * 0.2f + averageVelocity * 0.8f).normalized * currentSpeed;
        LookInDirection(rigidbody.velocity);
    }

    private Vector2 GetAverageVelocity()
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

        return averageVelocity;
    }

    private void LookInDirection(Vector2 direction)
    {
        this.transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }

    protected virtual void StartWondering()
    {
        isWondering = true;
        ContinueWondering();
        velocities.Clear();
    }

    protected virtual void ContinueWondering()
    {
        Vector2 newVelocityPart;

        if (velocities.Count > 0)
        {
            newVelocityPart = GetAverageVelocity();
        }
        else
        {
            newVelocityPart = new Vector2(rand.Next(20) - 10, rand.Next(20) - 10).normalized;
        }

        rigidbody.velocity = (rigidbody.velocity * 0.8f + newVelocityPart * 0.2f).normalized * currentSpeed;
        LookInDirection(rigidbody.velocity);
    }

    protected void TryAvoidWall(Collider2D collider)
    {
        if (collider.TryGetComponent<Wall>(out Wall wall))
        {
            velocities.Add(wall.PushBackVector);
        }
    }
}
