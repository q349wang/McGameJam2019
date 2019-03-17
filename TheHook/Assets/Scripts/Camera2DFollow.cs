using System;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    // Use this for initialization
    private void Start()
    {

    }


    // Update is called once per frame
    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
