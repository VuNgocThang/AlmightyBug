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
    public bool isKnockBack;

    Coroutine immuneCoroutine;
    public LayerMask damageable;
    public Transform checkGround;
    public LayerMask ground;
    private Rigidbody rb;
    //private BoxCollider perfectHitBox;
    private CapsuleCollider playerCollider;

    public Image hpBar;
    public Image staminaBar;

    private void OnValidate()
    {
        rb = GetComponent<Rigidbody>();
        //perfectHitBox = GetComponentInChildren<BoxCollider>();
        playerCollider = GetComponent<CapsuleCollider>();
    }
    void Awake()
    {
        hp = hpMax;
        stamina = staminaMax;
    }
    private void Start()
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
            Debug.Log("hp" + hp);
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
            PlayerManager enemy = collision.transform.GetComponent<PlayerManager>();
            if (Physics.Raycast(transform.position, rb.velocity, 10f, damageable) && player.isCharge)
            {
                Debug.Log("hit true and dame..." + player.isCharge);
                enemy.TakeDamage(damage);
                enemy.KnockBack(3, 5);
            }
            else if (!Physics.Raycast(transform.position, rb.velocity, 10f, damageable) && player.isCharge)
            {
                Debug.Log("hit false but dame..." + player.isCharge);
                float damageReduce = damage * 0.2f;
                enemy.TakeDamage(damageReduce);
                enemy.KnockBack(2, 4);
            }
            else
            {
                Debug.Log("hit true and no dame, no charge..." + player.isCharge);
                enemy.TakeDamage(0);
            }

        }
    }
    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, rb.velocity, Color.green);
    }
    public void KnockBack(float a, float b)
    {
        float offset = Random.Range(a, b);
        Debug.Log("offset knockBack: " + offset);
        isKnockBack = true;
        Vector3 input = new Vector3(offset, 2f, 0f);
        rb.AddForce(input, ForceMode.VelocityChange);
    }
    public void UpdateHp()
    {
        if (hp > hpMax) hp = hpMax;
        if (hp < 0) hp = 0;
        hpBar.fillAmount = hp / hpMax;
    }
    public void UpdateStamina()
    {
        if (stamina > staminaMax) stamina = staminaMax;
        if (stamina < 0) stamina = 0;
        staminaBar.fillAmount = stamina / staminaMax;
    }
    public IEnumerator StaminaRegen(float speedRegen)
    {
        while (true)
        {
            if (IsGrounded())
            {
                if (stamina < staminaMax)
                {
                    stamina += speedRegen;
                    Debug.Log("regen stamina");
                }
                else
                {
                    stamina = staminaMax;
                }

            }
            yield return new WaitForSeconds(1);
        }
    }
}
