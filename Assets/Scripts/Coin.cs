using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{    
    void OnTriggerEnter2D (Collider2D hit) {
        CharacterMove character = hit.GetComponent<CharacterMove>();
        if (character != null) {
            Destroy(gameObject);
        }
    }
}
