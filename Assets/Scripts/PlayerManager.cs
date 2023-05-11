using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float hp;
    public float hpMax;
    public float stamina;
    public float staminaMax;
    public float damage;
    public float forcePower;
    public float jumpCost;
    bool isHit;
    bool isIdle;
    Coroutine immuneCoroutine;
    public LayerMask damageable;
    public Transform checkGround;
    public LayerMask ground;
    private Rigidbody rb;
    private BoxCollider perfectHitBox;
    private CapsuleCollider playerCollider;

    // Start is called before the first frame update
    private void OnValidate()
    {
        rb = GetComponent<Rigidbody>();
        perfectHitBox = GetComponentInChildren<BoxCollider>();
        playerCollider = GetComponent<CapsuleCollider>();
    }
    void Start()
    {
        hp = hpMax;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Jump(float x, float y, float z)
    {
        rb.AddForce(new Vector3(x, y, z));
    }

    public void TakeDamage(float damage)
    {
        if (!isHit)
        {
            if (immuneCoroutine == null)
            {
                StartCoroutine(ImmuneCoroutine(1f));
            }
            hp -= damage;
        }
    }
    IEnumerator ImmuneCoroutine(float timeImmune)
    {
        isHit = true;
        yield return new WaitForSeconds(timeImmune);
        isHit = false;
    }
    public bool IsGrounded()
    {
        return Physics.CheckSphere(checkGround.position, 0.1f, ground);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            PlayerController player = GetComponent<PlayerController>();
            if (Physics.Raycast(transform.position, rb.velocity, 10f, damageable) && player.isCharge)
            {
                Debug.Log("hit true");
                PlayerManager enemy = collision.transform.GetComponent<PlayerManager>();
                enemy.TakeDamage(damage);
                enemy.KnockBack();
            }
            else
            {
                Debug.Log("hit false");
            }
        }
    }
    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, rb.velocity, Color.green);
    }
    public void KnockBack()
    {
        float offset = Random.Range(2, 4);
        Debug.Log("offset knockBack: " + offset);
        Vector3 input = new Vector3(offset, 2f, 0f);
        rb.AddForce(input, ForceMode.VelocityChange);
    }
    public IEnumerator StaminaRegen(float speedRegen)
    {
        while (isIdle)
        {
            if (stamina < staminaMax)
            {
                stamina += speedRegen;
                yield return new WaitForSeconds(1);
            }
            else
            {
                stamina = staminaMax;
                break;
            }
        }
    }
}
