using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [SerializeField] public Transform target;
    void Update() {
        transform.position = new Vector3(
            Mathf.Clamp(target.position.x, -100f, 100f),
            Mathf.Clamp(target.position.y, -100f, 100f),
            transform.position.z);
    }
}
