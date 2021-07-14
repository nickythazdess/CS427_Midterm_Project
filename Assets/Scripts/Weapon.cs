using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] public GameObject bulletpf;
    [SerializeField] public AudioSource attack;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
            attack.Play();
        }
    }

    void Shoot() {
        Instantiate(bulletpf, firePoint.position, firePoint.rotation);
    }
}
