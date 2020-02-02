using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider))]
public sealed class Slingshot : MonoBehaviour 
{
    [Min(0f)] public float launchMultiplier = 4f;

    public bool enabled;

    [SerializeField] private SlingshotLaunchArc launcher;
    [SerializeField] private SlingshotRubberBands rubberBands;

    private AmmoStock stock;
    private Ammo currentMunnition;
    private GameplayUI ui;

    private void Reset()
    {
        launcher = GetComponentInChildren<SlingshotLaunchArc>();
        rubberBands = GetComponentInChildren<SlingshotRubberBands>();
    }

    private void Awake()
    {
        ui = FindObjectOfType<GameplayUI>();
        stock = FindObjectOfType<AmmoStock>();
        GetNextAmmo();
    }

    public void DragAmmo(Vector3 dragPos)
    {
        if (!CanDrag()) return;

        dragPos = rubberBands.Dragging(dragPos);

        float launchForce = GetFireForce();
        launcher.Draw(dragPos, rubberBands.LaunchDirection, launchForce);
        currentMunnition?.Dragging(dragPos, rubberBands.LaunchDirection);
        ui?.SetStretching(rubberBands.Stretching);
    }

    public void ReleaseAmmo()
    {
        float launchForce = GetFireForce();
        FireCurrentAmmo(launchForce);
        GetNextAmmo();
    }

    public void FireCurrentAmmo(float force)
    {
        currentMunnition?.Throw(rubberBands.LaunchDirection, force);
    }

    private float GetFireForce()
    {
        return rubberBands.Stretching * launchMultiplier;
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