using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;


[DisallowMultipleComponent]
public sealed class AmmoStock : MonoBehaviour
{
    [Min(1)] public int initialCapacity = 20;

    public ScoreCalculator Score;

    private Stack<Ammo> stack;

    private void Awake()
    {
        stack = new Stack<Ammo>(initialCapacity);
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
        return stack.Count > 0;
    }
}