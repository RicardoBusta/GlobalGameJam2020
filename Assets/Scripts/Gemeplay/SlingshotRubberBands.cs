using UnityEngine;

[DisallowMultipleComponent]
public sealed class SlingshotRubberBands : MonoBehaviour 
{
    [SerializeField] private LineRenderer leftLine;
    [SerializeField] private LineRenderer rightLine;

    private readonly Vector3[] positions = new Vector3[2];

    public float Stretching { get; private set; }
    public Vector3 LaunchDirection { get; private set; }
    public Vector3 LauchPosition
    {
        get
        {
            return Vector3.Lerp(leftLine.transform.position, rightLine.transform.position, 0.5f);
        }
    }

    private const int LINE_RENDERER_ORIGIN_INDEX = 0;
    private const int LINE_RENDERER_DRAGGING_INDEX = 1;

    private void Reset () 
	{
        leftLine = transform.Find("Left")?.GetComponent<LineRenderer>();
        rightLine = transform.Find("Right")?.GetComponent<LineRenderer>();
    }

	public void Dragging(Vector3 dragPos) 
	{
        Vector3 lauchPosition = LauchPosition;
        dragPos.z = lauchPosition.z;

        Stretching = Vector3.Distance(lauchPosition, dragPos);
        LaunchDirection = (lauchPosition - dragPos).normalized;

        leftLine.useWorldSpace = true;
        rightLine.useWorldSpace = true;

        positions[LINE_RENDERER_DRAGGING_INDEX] = dragPos;
   
        positions[LINE_RENDERER_ORIGIN_INDEX] = leftLine.transform.position;
        leftLine.SetPositions(positions);

        positions[LINE_RENDERER_ORIGIN_INDEX] = rightLine.transform.position;
        rightLine.SetPositions(positions);
    }
}