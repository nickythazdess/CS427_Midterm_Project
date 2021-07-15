using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_gr : MonoBehaviour
{
    private float bloomTimer;
    private float blooming;
    private Animator anim;
    private CircleCollider2D cirCollider;
    [SerializeField] private AudioSource die;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cirCollider = GetComponent<CircleCollider2D>();
        bloomTimer = 3f;
        blooming = 0.5f;
        anim.Play("Normal_gr");
    }



    // Update is called once per frame
    void Update()
    {
        bloomTimer -= Time.deltaTime;
        Debug.Log(bloomTimer);
        if (bloomTimer < 0) {
            cirCollider.radius = 5.5f;
            blooming -= Time.deltaTime;
            anim.Play("Bloom_gr");
            if (blooming < 0) {
                bloomTimer = 3f;
                blooming = 0.5f;
                anim.Play("Normal_gr");
                cirCollider.radius = 3.0f;
            }
        }
    }

    public void Die() {
        die.Play();
        Destroy(gameObject);
    }
}
