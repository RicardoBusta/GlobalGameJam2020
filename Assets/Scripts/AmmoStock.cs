using UnityEngine;
using System.Collections.Generic;


[DisallowMultipleComponent]
public sealed class AmmoStock : MonoBehaviour 
{
    [Min(1)]
    public int inicialCapacity = 20;

    private Stack<Ammo> stack;

	private void Awake () 
	{
        LoadAmmo();
	}


    private void LoadAmmo()
    {
        stack = new Stack<Ammo>(inicialCapacity);

        foreach (Ammo ammo in GetComponentsInChildren<Ammo>())
        {
            stack.Push(ammo);
        }
    }

    public Ammo NextAmmo()
    {
        return HasAmmo() ?
            stack.Pop() : null;
    }

    public bool HasAmmo()
    {
        return stack.Count > 0;
    }
}