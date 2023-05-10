using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float hpEnemyMax = 300f;
    public float hpCurrentEnemy;
    public Image progressBarFilling;
    public Rigidbody rb;
    public bool isHit = false;
    Coroutine hitCoroutine = null;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        hpCurrentEnemy = hpEnemyMax;
        progressBarFilling.fillAmount = hpCurrentEnemy / hpEnemyMax;

    }

    public void HandleHPPlayer()
    {
        if (hpCurrentEnemy > hpEnemyMax)
        {
            hpCurrentEnemy = hpEnemyMax;
        }
        if (hpCurrentEnemy < 0)
        {
            hpCurrentEnemy = 0;
        }
        progressBarFilling.fillAmount =
            (hpCurrentEnemy > 0 && hpEnemyMax > 0) ? ((float)hpCurrentEnemy / (float)hpEnemyMax) : 0;
    }

    public void TakeDamage(float amount)
    {
        if (hitCoroutine != null)
        {
            StartCoroutine(IsHit());
        }

        if (!isHit)
        {
            isHit = true;
            hpCurrentEnemy -= amount;
            HandleHPPlayer();
            if (hpCurrentEnemy <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void KnockBack()
    {
        float offset = Random.Range(0.2f, 0.4f);
        Vector3 input = new Vector3(offset, 1, 0);
        rb.AddForce(input, ForceMode.VelocityChange);
    }
    IEnumerator IsHit()
    {
        yield return new WaitForSeconds(1);
        isHit = false;
        hitCoroutine = null;
    }
}
