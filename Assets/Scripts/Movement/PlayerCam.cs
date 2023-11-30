using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
    public Transform camHolder;

    float xRotation;
    float yRotation;

    private void Start()
    {
        xRotation = camHolder.parent.eulerAngles.x;
        yRotation = camHolder.parent.eulerAngles.y;
    }

}