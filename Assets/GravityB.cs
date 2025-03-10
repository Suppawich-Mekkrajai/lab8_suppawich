using System;
using System.Collections.Generic;
using UnityEngine;

public class GravityB : MonoBehaviour
{
    
    
    private Rigidbody rb;
    private const float G = 0.006674f;
    public static List<GravityB> PlanetList;
    [SerializeField] private bool planet = false;
    [SerializeField] private int orbitSpeed = 1000;

    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (PlanetList == null)
        {
            PlanetList = new List<GravityB>();
        }

        PlanetList.Add(this);
        
        if (!planet)
        {
            rb.AddForce(Vector3.left * orbitSpeed);
        }
        
    }
    
    private void FixedUpdate()
    {
        foreach (var planet in PlanetList)
        {
            if(planet != this)
                Attract(planet); 
        }
    }

    void Attract(GravityB other)
    {
       Rigidbody otherRb = other.rb;

       Vector3 direction = rb.position - otherRb.position;
       float distance = direction.magnitude;

       float forceMagnitude = G * ((rb.mass * otherRb.mass) / Mathf.Pow(distance, 2));
       Vector3 finalForce = forceMagnitude * direction.normalized;
       
       otherRb.AddForce(finalForce);
    }
}
