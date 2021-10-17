using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public static Transform point;

    private void Awake()
    {
        point = transform.GetChild(0);
    }
}
