using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TrailRenderer))]
public sealed class Ammo : MonoBehaviour 
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private TrailRenderer trail;

    private void Reset()
    {
        trail = GetComponent<TrailRenderer>();
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    private void Awake()
    {
        DisablePhysics();
    }

    public void Throw(Vector3 direction, float force)
    {
        EnablePhysics();
        rigidBody.velocity = direction * force;
    }

    public void Dragging(Vector3 position, Vector3 direction)
    {
        DisablePhysics();
        rigidBody.position = position;
        rigidBody.rotation = Quaternion.LookRotation(direction);
    }

    public void EnablePhysics()
    {
        trail.emitting = true;
        rigidBody.isKinematic = false;
    }

    public void DisablePhysics()
    {
        trail.Clear();
        trail.emitting = false;
        rigidBody.isKinematic = true;
    }
}