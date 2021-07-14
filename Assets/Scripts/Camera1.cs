using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1 : MonoBehaviour
{
    [SerializeField] public Transform target;
    private Vector2 Cave1_bottom_left = new Vector2(-1.74f, -13.63f);
    private Vector2 Cave1_top_right = new Vector2(16.81f, -0.29f);
    /*private Vector2 Water_Grass_bottom_left = new Vector2(39.25f, -14.11f);
    private Vector2 Water_Grass_top_right = new Vector2(81.76f, 0.15f);
    private Vector2 Sky_bottom_left = new Vector2(39.25f, 10.9f);
    private Vector2 Sky_top_right = new Vector2(156.68f, 41.15f);
    private Vector2 Water_bottom_left = new Vector2(39.25f, -32.2f);
    private Vector2 Water_top_right = new Vector2(156.68f, -25.9f);
    private Vector2 Cave2_bottom_left = new Vector2(104.29f, -14.08f);
    private Vector2 Cave2_top_right = new Vector2(122.71f, 0.18f);
    private Vector2 Grass_bottom_left = new Vector2(144.4f, -14.08f);
    private Vector2 Grass_top_right = new Vector2(156.69f, 0.18f);*/
    private Vector2 Other_bottom_left = new Vector2(39.16f, -31.69f);
    private Vector2 Other_top_right = new Vector2(156.7f, 40.5f);

    void Update() {
        Vector2 limit_bot_left = Cave1_bottom_left;
        Vector2 limit_top_right = Cave1_top_right;
        /*if (target.position.y <= -20f) {
            limit_bot_left = Water_bottom_left;
            limit_top_right = Water_top_right;
        } else if (target.position.y >= 6.2f) {
            limit_bot_left = Sky_bottom_left;
            limit_top_right = Sky_top_right;
        } else if (target.position.x > 28f) {
            if (target.position.x <= 93.1f) {
                limit_bot_left = Water_Grass_bottom_left;
                limit_top_right = Water_Grass_top_right;
            } else if (target.position.x <= 134f) {
                limit_bot_left = Cave2_bottom_left;
                limit_top_right = Cave2_top_right;
            } else {
                limit_bot_left = Grass_bottom_left;
                limit_top_right = Grass_top_right;
            }
        }*/

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