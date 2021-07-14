using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_enemy : MonoBehaviour
{
    private static int hit = 3;

    [SerializeField] private AudioSource die;
    // Start is called before the first frame update
    void Start()
    {
        hit = 3;
    }

    public void Die() {
        hit--;
        if (hit <= 0) {
            die.Play();
            Destroy(gameObject);
        }
    }
}
