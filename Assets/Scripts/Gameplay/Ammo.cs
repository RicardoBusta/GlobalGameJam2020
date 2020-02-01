using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public sealed class Ammo : MonoBehaviour 
{
    [SerializeField]
    private Rigidbody rigidBody;

    private void Reset()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    public void Throw(Vector3 direction, float force)
    {
        rigidBody.isKinematic = false;
        rigidBody.AddForce(direction * force, ForceMode.Impulse);
    }

    public void Dragging(Vector3 position, Vector3 direction)
    {
        rigidBody.isKinematic = true;
        rigidBody.position = position;
        rigidBody.rotation = Quaternion.LookRotation(direction);
    }
}