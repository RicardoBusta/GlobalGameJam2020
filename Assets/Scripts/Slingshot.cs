using UnityEngine;


[DisallowMultipleComponent]
public sealed class Slingshot : MonoBehaviour 
{
    public float height = 1f;
    //public float radius = 1.8f;
    public float addicionalLaunchForce = 4f;

    public Ammo currentMunnition;

    private Vector3 launchDirection;
    private float launchDistance;

    private void Awake()
    {
        currentMunnition = FindObjectOfType<Ammo>();
    }

    public Vector3 HeightPosition
    {
        get { return transform.position + Vector3.up * height; }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, HeightPosition, Color.red);
    }


    public void DragMonition(Vector3 input)
    {
        input.z = transform.position.z;

        if (!CanDrag(input)) return;

        launchDistance = Vector3.Distance(HeightPosition, input);
        launchDirection = (HeightPosition - input).normalized;


        Debug.DrawLine(input, input + launchDirection);


        currentMunnition.Draging(input);
    }

    public void ReleaseMonition()
    {
        float launchForce = launchDistance * addicionalLaunchForce;
        currentMunnition.Fire(launchDirection, launchForce);
    }

    private bool CanDrag(Vector3 input)
    {
        return true;
    }

    private void GetAmmo()
    {

    }
}
