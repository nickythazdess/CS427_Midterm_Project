using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rigid;
    private float disappearTimer;
    // Start is called before the first frame update
    void Start()
    {
        float speed = 15f;
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = transform.right * speed;
        disappearTimer = 1f;
    }

    void Update() {
        disappearTimer -= Time.deltaTime;

        if (disappearTimer < 0) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D (Collider2D hit) {
        Enemy enemy = hit.GetComponent<Enemy>();
        if (enemy != null) {
            enemy.Die();
        }
        Destroy(gameObject);
    }
}
