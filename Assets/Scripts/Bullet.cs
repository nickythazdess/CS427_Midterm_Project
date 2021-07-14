using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField] private AudioSource bullet_hit;
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
        bullet_hit.Play();
        Enemy enemy = hit.GetComponent<Enemy>();
        if (enemy != null) {
            enemy.Die();
        }
        if (!hit.tag.Equals("Water")) {
            Destroy(gameObject);
        }
    }
}
