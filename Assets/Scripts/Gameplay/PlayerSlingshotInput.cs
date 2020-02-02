using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Slingshot))]
public sealed class PlayerSlingshotInput : MonoBehaviour 
{
    public Slingshot slingshot;

    private Camera levelCamera;
    
    public bool enabled;

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
        if (!enabled) return;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(levelCamera.transform.position.z);
        Vector3 mouseWorldPos = levelCamera.ScreenToWorldPoint(mousePos);

        slingshot.DragAmmo(mouseWorldPos);
    }

    private void OnMouseUp()
    {
        if (!enabled) return;
        slingshot.ReleaseAmmo();
    }
}