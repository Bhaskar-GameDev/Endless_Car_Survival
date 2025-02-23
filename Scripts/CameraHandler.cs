using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public GameObject[] cameras;
    private int i = 0;

    void Start()
    {
        cameras[i].SetActive(true);

        for(int j = 1; j < cameras.Length; j++)
        {
            cameras[j].SetActive(false);
        }
    }

    public void NextCamera()
    {
        
        cameras[i].SetActive(false);

        i++;

        if (i >= cameras.Length)
        {
            i = 0;
        }

        cameras[i].SetActive(true);
    }
}
