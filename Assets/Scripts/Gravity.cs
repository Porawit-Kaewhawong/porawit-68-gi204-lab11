using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Gravity : MonoBehaviour
{
    public static List<Gravity> otherObjectList;

    private Rigidbody rb;
    private const float G = 0.006674f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (otherObjectList == null)
        {
            otherObjectList = new List<Gravity>();
        }

        otherObjectList.Add(this);
    }

    void FixedUpdate()
    {
        foreach (Gravity obj in otherObjectList)
        {
            if (obj != this)
            {
                Attract(obj);
            }
        }
    }

    void Attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;

        Vector3 direction = rb.position - otherRb.position;

        float distance = direction.magnitude;

        if (distance == 0f) return;

        float forceMagnitude = G * (rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);

        Vector3 gravityForce = forceMagnitude * direction.normalized;

        otherRb.AddForce(gravityForce);
    }
}
