﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DOVirtual.DelayedCall(0.5f, () => { SceneManager.LoadScene("Gameplay"); });
    }
}
