using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float timer = 3f;

    private void Start()
    {
        Invoke("DeactivateGameObject", timer);
    }

    private void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }
}
