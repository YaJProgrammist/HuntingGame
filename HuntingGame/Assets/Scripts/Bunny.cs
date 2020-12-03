using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bunny : Animal
{
    const float NORMAL_SPEED = 3;
    const float HIGH_SPEED = 10;
    [SerializeField] private ColliderTrigger flairArea;
    [SerializeField] private ColliderTrigger selfCollider;

    protected override void Start()
    {
        base.Start();
        currentSpeed = NORMAL_SPEED;
        flairArea.OnColliderTriggered += (s, ea) => OnFlairTriggered(ea.Collider);
        selfCollider.OnColliderTriggered += (s, ea) => OnSelfTriggered(ea.Collider);
    }

    void OnSelfTriggered(Collider2D collider)
    {
        if (collider.tag == "wolf" || collider.tag == "bullet")
        {
            this.Remove();
        }
    }

    void OnFlairTriggered(Collider2D collider)
    {
        if (collider.tag == "bunny" || collider.tag == "doe" || collider.tag == "wolf" || collider.tag == "hunter")
        {
            currentSpeed = HIGH_SPEED;
            velocities.Add(this.transform.position - collider.transform.position);
        }
    }
}
