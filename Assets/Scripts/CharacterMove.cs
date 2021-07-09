using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private LayerMask platform;
    [SerializeField] private LayerMask water;
    [SerializeField] private GameObject water_obj;
    private Water_control water_control;
    private bool water_mode = true;
    private Rigidbody2D rigid;
    private Animator anim;
    private CircleCollider2D cirCollider;
    private bool IsAlive = true;
    private int prevStatus = -1;
    private int timer = 5;
    private float timer_1s = 1f;
    private bool isFacingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        IsAlive = true;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cirCollider = GetComponent<CircleCollider2D>();
        water_control = water_obj.GetComponent<Water_control>();
    }

    private int Status() {
        RaycastHit2D[] hit = new RaycastHit2D[1];
        cirCollider.Cast(cirCollider.offset + Vector2.down, hit, .1f);
        if (!IsAlive) return 10;
        if (hit[0].collider != null) {
            switch (hit[0].collider.gameObject.layer) {
                case 3: return 0; // is grounded
                case 4: {
                        Vector2 startpoint = new Vector2(cirCollider.bounds.center.x, cirCollider.bounds.center.y);
                        hit[0] = Physics2D.Raycast(startpoint, Vector2.up, .1f, water);
                        if (hit[0].collider != null) return 2; // is underwater
                    }
                    return 1; // is on water surface
                default: return 3; // is in mid-air
            };
        } else return 3; // is in mid-air
    }

    private bool hitTheWall(int dir) {
        Vector2 direction;
        if (dir == 0) direction = Vector2.left;
        else direction = Vector2.right;

        RaycastHit2D[] hit = new RaycastHit2D[1];
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = platform;
        int num_hit = cirCollider.Cast(direction, filter, hit, .01f);
        if (hit[0].collider != null) return true;
        else return false;
    } 

    // Update is called once per frame
    private void Update()
    {
        int curStatus = Status();
        Debug.Log("Status: " + curStatus);
        //if (!IsAlive) anim.Play("Mario_Dead");
        switch (curStatus) {
            case 0: anim.Play("Idle"); break;
            case 1: 
                anim.Play("Swimming");
                if (!water_mode) { water_control.incDensity(); water_mode = true; }
                break;
            case 2: anim.Play("Idle"); break;
            default: anim.Play("Idle"); break;
        }

        if (curStatus == 2) {
            if (curStatus == prevStatus) {
                if (timer_1s < 0) {
                    Timer.Create(timer);
                    timer_1s = 1f;
                    timer -= 1;
                    if (timer < 0) StartCoroutine(Die());
                }
                else timer_1s -= Time.deltaTime;
            }
        } else timer = 5;
        
        if (curStatus <= 2 && Input.GetKeyDown(KeyCode.Space)) {
            float jumpSpeed = 10f;
            rigid.velocity = Vector2.up * jumpSpeed;
            
        }
        if (curStatus <= 3) movement(curStatus);
        if (prevStatus != curStatus) prevStatus = curStatus;
        //if (IsAlive) attack();
    }

    private void movement(int stat) {
        float moveSpeed = 10f;
        if (stat == 2) {
            Vector2 move = new Vector2();
            if (water_mode) {water_control.decDensity(); water_mode = false;}
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
            if (Input.GetKey(KeyCode.LeftArrow) && !hitTheWall(0)) {
                rigid.velocity = new Vector2(-moveSpeed, rigid.velocity.y);
                if (isFacingRight) Flip();
            } else if (Input.GetKey(KeyCode.RightArrow) && !hitTheWall(1)) {
                rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
                if (!isFacingRight) Flip();
            } else if (Input.GetKey(KeyCode.DownArrow) && (stat == 1)) {
                water_control.decDensity(); water_mode = false;
            } else rigid.velocity = new Vector2(0, rigid.velocity.y);
        }
    }

    private void OnCollisionEnter2D (Collision2D col) {
        if (col.gameObject.tag.Equals("Water")) {
            Debug.Log("Enter water");
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
        cirCollider.enabled = !cirCollider.enabled;
        rigid.velocity = new Vector2(0, 20f);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("SampleScene");
    }

    void Flip() {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void FixedUpdate() {
        //rigid.velocity = new Vector2(curMoveSpeed, curJumpSpeed);
    }
}
