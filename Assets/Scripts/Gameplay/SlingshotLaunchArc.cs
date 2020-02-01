using UnityEngine;


[DisallowMultipleComponent]
[RequireComponent(typeof(LineRenderer))]
public sealed class SlingshotLaunchArc : MonoBehaviour 
{
    [Min(0)] public int resolution = 10;
    [SerializeField] private LineRenderer line;

    private Vector3[] positions;

    private void Reset()
    {
        line = GetComponent<LineRenderer>();
    }

    private void Awake () 
	{
        positions = new Vector3[resolution];
    }

    public void Draw(Vector3 dragPos, Vector3 dragDirection, float force)
    {
        float gravity = Mathf.Abs(Physics.gravity.y);
        float angle = Vector3.SignedAngle(dragDirection, Vector3.right, Vector3.back) * Mathf.Deg2Rad;

        positions = new Vector3[resolution];

        for (int i = 0; i < positions.Length; i++)
        {
            float t = (float) i / positions.Length;
            Vector3 offset = new Vector3(
                force * t * Mathf.Cos(angle),
                force * t * Mathf.Sin(angle) - 0.5F * gravity * t * t,
                0f);

            positions[i] = dragPos + offset;
        }

        line.positionCount = positions.Length;
        line.SetPositions(positions);
    }
}