using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float bloomTimer;
    private float blooming;
    private Animator anim;
    private CircleCollider2D cirCollider;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cirCollider = GetComponent<CircleCollider2D>();
        bloomTimer = 3f;
        blooming = 0.5f;
        anim.Play("Normal");
    }



    // Update is called once per frame
    void Update()
    {
        bloomTimer -= Time.deltaTime;
        Debug.Log(bloomTimer);
        if (bloomTimer < 0) {
            cirCollider.radius = 10.0f;
            blooming -= Time.deltaTime;
            anim.Play("Bloom");
            if (blooming < 0) {
                bloomTimer = 3f;
                blooming = 0.5f;
                anim.Play("Normal");
                cirCollider.radius = 3.0f;
            }
        }
    }

    public void Die() {
        Destroy(gameObject);
    }
}