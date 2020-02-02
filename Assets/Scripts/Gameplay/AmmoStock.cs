using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;


[DisallowMultipleComponent]
public sealed class AmmoStock : MonoBehaviour
{
    private Stack<Ammo> stack;

    private void Awake()
    {
        stack = new Stack<Ammo>();
        var ammoList = GetComponentsInChildren<Ammo>();
        LoadAmmo();
    }


    private void LoadAmmo()
    {
        foreach (Ammo ammo in GetComponentsInChildren<Ammo>())
        {
            stack.Push(ammo);
        }
    }

    public Ammo NextAmmo()
    {
        return HasAmmo() ? stack.Pop() : null;
    }

    public bool HasAmmo()
    {
        if (stack == null) return false;
        return stack.Count > 0;
    }
}