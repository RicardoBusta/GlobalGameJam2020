using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Slingshot))]
public sealed class PlayerInput : MonoBehaviour 
{
    public Slingshot slingshot;

    private void Reset()
    {
        slingshot = GetComponent<Slingshot>();
    }

    private void Start () 
	{
		
	}

	private void Update () 
	{
		
	}	
}