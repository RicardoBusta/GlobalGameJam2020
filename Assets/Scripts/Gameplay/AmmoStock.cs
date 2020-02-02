using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;


[DisallowMultipleComponent]
public sealed class AmmoStock : MonoBehaviour
{
    public ScoreCalculator Score;

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
            ammo.StickEvent += Score.UpdateValue;
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