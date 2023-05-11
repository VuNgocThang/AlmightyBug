using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerManager
{
    public HandleButton btnBack;
    public HandleButton btnForward;
    public float chagreForce;


    private void Start()
    {
        btnBack.Init(Back);
        btnForward.Init(Forward);
    }

    public void Back()
    {
        if (IsGrounded())
        {
            float offset = forcePower * Random.Range(95, 105) / 100;
            Debug.Log("jump Back");
            Jump(-offset, 200, 0);
        }
    }

    public void ReleaseBack(float abc)
    {

    }

    public void Forward()
    {
        if (IsGrounded())
        {
            StartCoroutine(Charge());
            Debug.Log("charge force" + chagreForce);
            float offset = chagreForce * forcePower * Random.Range(95, 105) / 100;
            Debug.Log("jump Forward");
            Jump(offset, 200, 0);
        }
    }

    public void ReleaseForward(float abc)
    {

    }

    public bool isCharge = false;
    public void ChargePower()
    {
        /*if (btnForward.timer < 4)
        {
            chagreForce = 4;
        }
        if (btnForward.timer < 3)
        {
            chagreForce = 3;
        }
        if (btnForward.timer < 2)
        {
            chagreForce = 2;
        }
        if (btnForward.timer < 1)
        {
            chagreForce = 1;
        }*/

        if (btnForward.timer > 0)
        {
            chagreForce = 1f;
        }

        if (btnForward.timer > 1)
        {
            isCharge = true;
            chagreForce = 2f;
        }
        if (btnForward.timer > 2)
        {
            isCharge = true;
            chagreForce = 3f;
        }
        if (btnForward.timer > 3)
        {
            isCharge = true;
            chagreForce = 3f;
        }

    }

    IEnumerator Charge()
    {
        while (btnForward.IsTouch)
        {
            Debug.Log("charge");
            Debug.Log("timer charge:  " + btnForward.timer);
            stamina -= jumpCost;
            ChargePower();
            yield return new WaitForSeconds(1);
        }
    }

}
