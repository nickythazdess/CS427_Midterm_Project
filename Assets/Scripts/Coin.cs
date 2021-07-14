using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{   
    [SerializeField] private AudioSource eat; 
    void OnTriggerEnter2D (Collider2D hit) {
        if (hit.tag.Equals("Player")) {
            CoinCount.coinAmount += 1;
            eat.Play();
            Destroy(gameObject);
        }
    }
}
