using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerManager
{
    public HandleButton btnBack;
    public HandleButton btnForward;
    public float chagreForce;
    public bool isCharge = false;

    private void Start()
    {
        btnBack.Init(Back);
        btnForward.Init(Forward);
        StartCoroutine(StaminaRegen(5));

    }

    private void FixedUpdate()
    {
        UpdateHp();
        UpdateStamina();
    }

    public void Back()
    {
        if (IsGrounded())
        {
            stamina -= jumpCost;
            float offset = forcePower * Random.Range(95, 105) / 100;
            Jump(-offset, 200, 0);
        }
    }

    public void Forward()
    {
        if (IsGrounded())
        {
            StartCoroutine(Charge());
            float offset = chagreForce * forcePower * Random.Range(95, 105) / 100;
            Jump(offset, 200, 0);
        }
    }

    public void ChargePower()
    {
        
        if (btnForward.timer >= 1)
        {
            isCharge = true;
            chagreForce += 1f;
            if (btnForward.timer > 3)
            {
                chagreForce = 3f;
            }
        }
        else
        {
            isCharge = false;
            chagreForce = 1f;
        }
    }

    IEnumerator Charge()
    {
        while (btnForward.IsTouch)
        {
            stamina -= jumpCost;
            ChargePower();
            //Debug.Log("time Charge: " + btnForward.timer);
            yield return new WaitForSeconds(1);
        }
    }

}
