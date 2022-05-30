using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTriggerCollider : MonoBehaviour
{
    private Trap_Spike trap_Spike;
    private BoxCollider2D bCol;

    private void Awake()
    {
        trap_Spike = GetComponentInParent<Trap_Spike>();
        bCol = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            trap_Spike.SpikeTrap();
        }
    }
}
