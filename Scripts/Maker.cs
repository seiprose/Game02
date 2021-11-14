using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maker : MonoBehaviour, IMaker
{
    [SerializeField] Rigidbody Target;
    private bool makeTarget = false;

    void Update()
    {
        MakeTarget();
    }

    public void Make()
    {
        makeTarget = true;
    }

    void MakeTarget()
    {
        if(makeTarget)
        {
            float x = Random.Range(1f, 2f);
            Rigidbody newTarget = (Rigidbody)Instantiate(Target, transform.position, transform.rotation);
            newTarget.AddForce(transform.right * 4f + transform.up * x, ForceMode.Impulse);
            makeTarget = false;
        }
    }
}
