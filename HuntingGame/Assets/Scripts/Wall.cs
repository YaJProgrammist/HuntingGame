using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private GameObject field;
    public Vector2 PushBackVector { get; private set; }

    void Start()
    {
        PushBackVector = (field.transform.position - this.transform.position).normalized;
    }
}
