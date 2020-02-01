using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider))]
public sealed class Slingshot : MonoBehaviour 
{
    [Min(0f)] public float launchMultiplier = 4f;

    [SerializeField] private SlingshotRubberBands rubberBands;

    private AmmoStock stock;
    private Ammo currentMunnition;

    private void Reset()
    {
        rubberBands = GetComponentInChildren<SlingshotRubberBands>();
    }

    private void Awake()
    {
        stock = FindObjectOfType<AmmoStock>();
        GetNextAmmo();
    }

    public void DragAmmo(Vector3 dragPos)
    {
        if (!CanDrag()) return;

        dragPos = rubberBands.Dragging(dragPos);
        currentMunnition?.Dragging(dragPos);
    }

    public void ReleaseAmmo()
    {
        float launchForce = rubberBands.Stretching * launchMultiplier;
        FireCurrentAmmo(launchForce);
        GetNextAmmo();
    }

    public void FireCurrentAmmo(float force)
    {
        currentMunnition?.Throw(rubberBands.LaunchDirection, force);
    }

    private bool CanDrag()
    {
        return currentMunnition != null;
    }

    private bool InsideSphere(Vector3 center, float radius, Vector3 position)
    {
        return Vector3.Distance(position, center) < radius;
    }

    private void GetNextAmmo()
    {
        currentMunnition = stock?.NextAmmo();
    }
}
