using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Slingshot))]
public sealed class PlayerSlingshotInput : MonoBehaviour 
{
    public Slingshot slingshot;

    private Camera levelCamera;

    private void Reset()
    {
        slingshot = GetComponent<Slingshot>();
    }

    private void Awake()
    {
        levelCamera = Camera.main;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(levelCamera.transform.position.z);
        Vector3 mouseWorldPos = levelCamera.ScreenToWorldPoint(mousePos);

        slingshot.DragMonition(mouseWorldPos);
    }

    private void OnMouseUp()
    {
        slingshot.ReleaseMonition();
    }
}