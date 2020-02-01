using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider))]
public sealed class Slingshot : MonoBehaviour 
{
    [Min(0f)] public float maxStretching = .5f;
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

    private void OnDrawGizmosSelected()
    {
        if (rubberBands)
        {
            Vector3 origin = rubberBands.LauchPosition;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(origin, origin - transform.forward * maxStretching);
        }
    }

    public void DragAmmo(Vector3 dragPos)
    {
        if (!CanDrag()) return;

        dragPos = ClampMaxArea(dragPos);

        rubberBands?.Dragging(dragPos);
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

    private Vector3 ClampMaxArea(Vector3 position)
    {
        Vector3 direction = (position - rubberBands.LauchPosition);
        float distance = direction.magnitude;

        if(distance > maxStretching)
        {
            position = rubberBands.LauchPosition + direction.normalized * maxStretching;
        }

        return position;
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
