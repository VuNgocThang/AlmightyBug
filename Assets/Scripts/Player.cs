using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public CapsuleCollider coll;
    public Transform checkGround;
    public LayerMask ground;
    public Image progressBarFilling;

    public float hpPlayerMax = 100f;
    public float hpPlayerCurrent;

    public float dame = 43f;
    public float range = 0.6f;
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
        hpPlayerCurrent = hpPlayerMax;
        progressBarFilling.fillAmount = hpPlayerCurrent / hpPlayerMax;

    }
    private void Start()
    {
        range = coll.radius + 0.5f;
    }
    private void Update()
    {
        RayTest();
    }


    public bool IsGrounded()
    {
        return Physics.CheckSphere(checkGround.position, 0.1f, ground);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("ddaasm nhau");
        }
    }
    public void HandleHPPlayer()
    {
        if (hpPlayerCurrent > hpPlayerMax)
        {
            hpPlayerCurrent = hpPlayerMax;
        }
        if (hpPlayerCurrent < 0)
        {
            hpPlayerCurrent = 0;
        }
        progressBarFilling.fillAmount =
            (hpPlayerCurrent > 0 && hpPlayerMax > 0) ? ((float)hpPlayerCurrent / (float)hpPlayerMax) : 0;
    }

    private void RayTest()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.right, out hit, range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            enemy.TakeDamage(dame);
          
           
            enemy.KnockBack();
            //Debug.Log("enemy.hpCurrentEnemy " + enemy.hpCurrentEnemy);
        }
        else
        {
            //Debug.Log("falseeee");
        }

    }
   
    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + transform.right * range, Color.red);
    }
}
