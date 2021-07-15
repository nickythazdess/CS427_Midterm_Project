using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private AudioSource die;
    
    public void Die() {
        die.Play();
        Destroy(gameObject);
    }
}
