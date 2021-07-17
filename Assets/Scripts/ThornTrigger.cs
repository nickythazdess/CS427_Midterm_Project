using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask player;
    Rigidbody2D rigid;

    void Start() {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 100f, player);
        if (hit.collider != null) {
            rigid.gravityScale = 3f;
            enabled = false;
        }
    }
}
