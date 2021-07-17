using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1 : MonoBehaviour
{
    [SerializeField] public Transform target;
    private Vector2 Cave1_bottom_left = new Vector2(-1.74f, -13.63f);
    private Vector2 Cave1_top_right = new Vector2(16.81f, -0.29f);
    private Vector2 Other_bottom_left = new Vector2(39.16f, -31.69f);
    private Vector2 Other_top_right = new Vector2(156.7f, 40.5f);

    void Update() {
        Vector2 limit_bot_left = Cave1_bottom_left;
        Vector2 limit_top_right = Cave1_top_right;

        if (target.position.x > 28f) {
            limit_bot_left = Other_bottom_left;
            limit_top_right = Other_top_right;
        }

        transform.position = new Vector3(
            Mathf.Clamp(target.position.x, limit_bot_left.x, limit_top_right.x),
            Mathf.Clamp(target.position.y, limit_bot_left.y, limit_top_right.y),
            transform.position.z);
    }
}