using UnityEngine;

[DisallowMultipleComponent]
public sealed class SlingshotRubberBands : MonoBehaviour 
{
    [Min(0f)] public float maxStretching = 2f;

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

    private const int LINE_POS_ORIGIN_INDEX = 0;
    private const int LINE_POS_DRAGGING_INDEX = 1;


    private void Reset () 
	{
        leftLine = transform.Find("Left")?.GetComponent<LineRenderer>();
        rightLine = transform.Find("Right")?.GetComponent<LineRenderer>();

        leftLine.positionCount = 2;
        rightLine.positionCount = 2;
    }

    private void Awake()
    {
        PlaceRubberBands();
    }

    private void OnValidate()
    {
        if(leftLine && rightLine)
        {
            Vector3 maxStretchingPos = LauchPosition - transform.forward * maxStretching;
            UpdateLinePosition(maxStretchingPos);
        }
    }

    public Vector3 Dragging(Vector3 dragPos) 
	{
        Vector3 lauchPosition = LauchPosition;
        dragPos.z = lauchPosition.z;

        LaunchDirection = (lauchPosition - dragPos).normalized;
        Stretching = Vector3.Distance(lauchPosition, dragPos);

        if (Stretching > maxStretching)
        {
            Stretching = maxStretching;
            dragPos = LauchPosition - LaunchDirection * maxStretching;
        }       

        UpdateLinePosition(dragPos);

        return dragPos;
    }

    private void UpdateLinePosition(Vector3 position)
    {
        leftLine.useWorldSpace = true;
        rightLine.useWorldSpace = true;

        positions[LINE_POS_DRAGGING_INDEX] = position;

        positions[LINE_POS_ORIGIN_INDEX] = leftLine.transform.position;
        leftLine.SetPositions(positions);

        positions[LINE_POS_ORIGIN_INDEX] = rightLine.transform.position;
        rightLine.SetPositions(positions);
    }

    private void PlaceRubberBands()
    {
        Vector3 beginOffset = 1.6f * Vector3.up - transform.parent.forward * 0.5f;
        UpdateLinePosition(transform.parent.position + beginOffset);
    }
}