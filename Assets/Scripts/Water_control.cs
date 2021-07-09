using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_control : MonoBehaviour
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
        
    }

    public void incDensity() { eff.density = 5; }

    public void decDensity() { eff.density = 0; }
}
