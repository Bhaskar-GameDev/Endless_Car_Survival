using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyreRotation : MonoBehaviour
{
    public float rotationSpeed = 5f;
    void Update()
    {
        transform.Rotate(rotationSpeed,0f,0f);
    }
}
