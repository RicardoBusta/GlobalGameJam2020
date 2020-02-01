using System;
using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TrailRenderer))]
public sealed class Ammo : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private TrailRenderer trail;

    public LayerMask SticksTo;

    public event Action StickEvent;

    private void Reset()
    {
        trail = GetComponent<TrailRenderer>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        DisablePhysics();
    }

    public void Throw(Vector3 direction, float force)
    {
        rigidBody.constraints = RigidbodyConstraints.FreezePositionZ;
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

    private void OnCollisionEnter(Collision other)
    {
        var otherLayer = other.gameObject.layer;
        Debug.Log($"other: {otherLayer} mask: {SticksTo.value}");
        if (SticksTo.value == (SticksTo.value | (1 << otherLayer)))
        {
            StickEvent?.Invoke();
            DisablePhysics();
        }
    }
}