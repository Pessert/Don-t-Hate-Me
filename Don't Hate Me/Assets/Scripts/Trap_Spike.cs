using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Spike : Trap
{
    public string direction;
    public float force;

    private Rigidbody2D rb;
    private bool isTriggered;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SpikeTrap()
    {
        if (isTriggered) return;

        isTriggered = true;
        StartCoroutine(DisappearRoutine());
        if (direction.Equals("Horizontal"))
        {
            rb.AddForce(Vector3.right * force);
        }
        else
        {
            rb.AddForce(Vector3.up * force);
        }
    }

    private IEnumerator DisappearRoutine()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
