using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Leave empty to look at main camera")]
    private Transform target = null;

    private void Start()
    {
        if (target == null)
        {
            target = Camera.main.transform;
        }
    }

    private void Update()
    {
        transform.LookAt(transform.position + target.rotation * -Vector3.back,
            target.rotation * -Vector3.down);
    }
}
