using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform TargetToLookAt;

    void Update()
    {
        if (!TargetToLookAt) TargetToLookAt = Camera.main.transform;
        transform.LookAt(TargetToLookAt);
        transform.rotation *= Quaternion.Euler(0, 180f, 0);
    }
}
