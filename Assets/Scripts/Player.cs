using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask platform;
    [SerializeField] private LayerMask water;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource loseSound;
    private bool water_mode = true;
    private Rigidbody2D rigid;
    private Animator anim;
    private CircleCollider2D cirCollider;
    private bool IsAlive = true;
    private int prevStatus = -1;
    private int timer = 5;
    private float timer_1s = 1f;
    private bool isFacingRight = true;
    private int character;
    // Start is called before the first frame update
    void Start()
    {
        IsAlive = true;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cirCollider = GetComponent<CircleCollider2D>();
        character = PlayerPrefs.GetInt("Character");
    }

    private int Status() {
        RaycastHit2D[] hit = new RaycastHit2D[1];
        cirCollider.Cast(Vector2.down, hit, .1f);
        if (!IsAlive) return 10;
        if (hit[0].collider != null) {
            switch (hit[0].collider.gameObject.layer) {
                case 3: return 0; // is grounded
                case 4: {
                        Vector2 startpoint = new Vector2(cirCollider.bounds.center.x, cirCollider.bounds.center.y);
                        hit[0] = Physics2D.Raycast(startpoint, Vector2.zero, .1f, water);
                        if (hit[0].collider != null) return 2; // is underwater
                    }
                    return 1; // is on water surface
                default: return 4;
            };
        } else return 3; // is in mid-air
    }

    // Update is called once per frame
    private void Update()
    {
        int curStatus = Status();
        Debug.Log("Status: " + curStatus);
        //if (!IsAlive) anim.Play("Dead");
        if (IsAlive) {
            PlayAnim(curStatus);

            if (character == 0) countdown_red(curStatus);
            else if (character == 1) countdown_yellow(curStatus);
            else if (character == 2) countdown_green(curStatus);
            
            if (Input.GetKeyDown(KeyCode.Space) && ((character != 1 && curStatus <= 2) 
                                                || (character == 1 && curStatus <= 3))) 
            {
                float jumpSpeed = 15f;
                rigid.velocity = Vector2.up * jumpSpeed;
                jumpSound.Play();
            }
            
            if (curStatus <= 3) movement(curStatus);
            if (prevStatus != curStatus) prevStatus = curStatus;
            if (timer < 0) StartCoroutine(Die());
        }
    }

    private void movement(int stat) {
        float moveSpeed = 10f;
        if (stat == 2) {
            Vector2 move = new Vector2();
            if (water_mode) { Water_control.decDensity(); water_mode = false;}
            if (Input.GetKey(KeyCode.LeftArrow)) {
                move = Vector2.left;
                if (isFacingRight) Flip();
            } 
            else if (Input.GetKey(KeyCode.RightArrow)) {
                move = Vector2.right;
                if (!isFacingRight) Flip();
            }
            else if (Input.GetKey(KeyCode.DownArrow)) move = Vector2.down;
            else if (Input.GetKey(KeyCode.UpArrow)) move = Vector2.up;
            else move = Vector2.zero;

            rigid.velocity = new Vector2(move.x * moveSpeed, move.y * moveSpeed);
        } else {
            if (Input.GetKey(KeyCode.LeftArrow)) {
                rigid.velocity = new Vector2(-moveSpeed, rigid.velocity.y);
                if (isFacingRight) Flip();
            } else if (Input.GetKey(KeyCode.RightArrow)) {
                rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
                if (!isFacingRight) Flip();
            } else if (Input.GetKey(KeyCode.DownArrow) && (stat == 1)) {
               Water_control.decDensity(); water_mode = false;
            } else rigid.velocity = new Vector2(0, rigid.velocity.y);
        }
    }

    private void countdown_red(int curStatus) {
        if (curStatus == 2) {
            if (curStatus == prevStatus) {
                if (timer_1s < 0) {
                    Timer.Create(timer);
                    timer_1s = 1f;
                    timer -= 1;
                }
                else timer_1s -= Time.deltaTime;
            }
        } else timer = 5;
    }

    private void countdown_yellow(int curStatus) {
        if (curStatus == 2 || curStatus == 0) {
            if (curStatus == prevStatus) {
                if (timer_1s < 0) {
                    Timer.Create(timer);
                    timer_1s = 1f;
                    timer -= 1;
                }
                else timer_1s -= Time.deltaTime;
            }
        } else timer = 2;
    }

    private void countdown_green(int curStatus) {
        if (curStatus != 1 && curStatus != 2) {
            if (curStatus == prevStatus) {
                if (timer_1s < 0) {
                    Timer.Create(timer);
                    timer_1s = 1f;
                    timer -= 1;
                }
                else timer_1s -= Time.deltaTime;
            }
        } else timer = 5;
    }

    private void PlayAnim(int curStatus) {
        switch (curStatus) {
            case 0: {
                switch (character) {
                    case 0: anim.Play("Idle"); break;
                    case 1: anim.Play("Idle_yellow"); break;
                    case 2: anim.Play("On_land_green"); break;
                }
                break;
            }
            case 1: 
                switch (character) {
                    case 0: anim.Play("Idle"); break;
                    case 1: anim.Play("Idle_yellow"); break;
                    case 2: anim.Play("Idle_green"); break;
                }
                if (!water_mode) { Water_control.incDensity(); water_mode = true; }
                break;
            case 2: {
                switch (character) {
                    case 0: anim.Play("Swimming"); break;
                    case 1: anim.Play("Swimming_yellow"); break;
                    case 2: anim.Play("Idle_green"); break;
                }
                break;
            }
            case 3: {
                switch (character) {
                    case 0: anim.Play("Idle"); break;
                    case 1: anim.Play("Idle_yellow"); break;
                    case 2: anim.Play("On_land_green"); break;
                }
                break;
            }
            default: {
                switch (character) {
                    case 0: anim.Play("Idle"); break;
                    case 1: anim.Play("Idle_yellow"); break;
                    case 2: anim.Play("Idle_green"); break;
                }
                break;
            }
        }
    }

    private void OnCollisionEnter2D (Collision2D col) {
        if (col.gameObject.tag.Equals("Water")) {

        }
        else if (col.gameObject.tag.Equals("Enemy")) {
            StartCoroutine(Die());
        }
    }

    private void OnCollisionExit2D (Collision2D col) {
        if (col.gameObject.tag.Equals("Water")) {
            Debug.Log("Get out of water");
        }
    }

    IEnumerator Die()
    {
        IsAlive = false;
        loseSound.Play();
        cirCollider.enabled = !cirCollider.enabled;
        rigid.velocity = new Vector2(0, 20f);
        yield return new WaitForSeconds(3);
        Loader.Load(Loader.Scene.Scene);
    }

    void Flip() {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void FixedUpdate() {
        //rigid.velocity = new Vector2(curMoveSpeed, curJumpSpeed);
    }
}
