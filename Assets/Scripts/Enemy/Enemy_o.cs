using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_o : Enemy
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
        anim.Play("Normal_o");
    }

    // Update is called once per frame
    void Update()
    {
        bloomTimer -= Time.deltaTime;
        Debug.Log(bloomTimer);
        if (bloomTimer < 0) {
            cirCollider.radius = 5.5f;
            blooming -= Time.deltaTime;
            anim.Play("Bloom_o");
            if (blooming < 0) {
                bloomTimer = 3f;
                blooming = 0.5f;
                anim.Play("Normal_o");
                cirCollider.radius = 3.0f;
            }
        }
    }
}
