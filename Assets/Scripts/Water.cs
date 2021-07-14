using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private BuoyancyEffector2D eff;
    // Start is called before the first frame update
    void Start()
    {
        eff = GetComponent<BuoyancyEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        eff.density = Water_control.density;
    }
}
