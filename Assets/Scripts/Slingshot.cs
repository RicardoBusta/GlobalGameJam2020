using UnityEngine;


[DisallowMultipleComponent]
public sealed class Slingshot : MonoBehaviour 
{
    public float height = 1f;
    public float radius = 1.8f;
    public Transform munnition;

    private Vector3 launchDirection;
    private float launchDistance;

    public Vector3 HeightPosition
    {
        get { return transform.position + Vector3.up * height; }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, HeightPosition, Color.red);
    }


    private void OnMouseDrag()
    {
        if (munnition) DragMonition();
    }

    private void OnMouseUp()
    {
        if (munnition) ReleaseMonition();
    }

    private void DragMonition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        launchDistance = Vector3.Distance(mouseWorldPos, HeightPosition);
        launchDirection = (HeightPosition - mouseWorldPos).normalized;

        Debug.DrawLine(mouseWorldPos, mouseWorldPos + launchDirection);

       
        munnition.position = mouseWorldPos;
    }

    private void ReleaseMonition()
    {
        Rigidbody rigidBody = munnition.gameObject.AddComponent<Rigidbody>();

        rigidBody.AddForce(launchDirection * launchDistance * 4, ForceMode.Impulse);
    }
}
