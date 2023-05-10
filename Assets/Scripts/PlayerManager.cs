using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public HandleButton btnBack;
    public HandleButton btnForward;
    public Image progressBarFilling;
    public Player Player;
    public GameObject player;

    public float force = 1f;
    public float power = 100f;
    public float chagreForce;
    public float currentPower;
    public float jump = 3f;
    public float jumpCost = 15f;

    private void Awake()
    {
        btnBack.Init(Back, ReleaseBack);
        btnForward.Init(Forward, ReleaseForward);
        currentPower = power;
        progressBarFilling.fillAmount = currentPower / power;
    }

    public void Back()
    {
        if (Player.IsGrounded() && currentPower > 15f)
        {
            currentPower -= jumpCost; HandlePower();
            float offset = force * Random.Range(95, 105) / 100;
            Vector3 input = new Vector3(-offset, 1, 0);
            Player.rb.AddForce(input * jump, ForceMode.VelocityChange);
        }
    }

    public void ReleaseBack(float abc)
    {

    }

    public void Forward()
    {
        if (Player.IsGrounded())
        {
            StartCoroutine(Charge());
            float offset = force * Random.Range(95, 105) / 100;
            Player.rb.AddForce(new Vector3(offset * chagreForce, 4, 0), ForceMode.Impulse);

        }
    }

    public void ReleaseForward(float abc)
    {

    }

    public void HandlePower()
    {
        if (currentPower > power)
        {
            currentPower = power;
        }
        if (currentPower < 0)
        {
            currentPower = 0;
        }
        progressBarFilling.fillAmount =
            (currentPower > 0 && power > 0) ? ((float)currentPower / (float)power) : 0;
    }

    public void ChargePower()
    {

        if (btnForward.timer < 4)
        {
            chagreForce = 12;
        }
        if (btnForward.timer < 3)
        {
            chagreForce = 9;
        }
        if (btnForward.timer < 2)
        {
            chagreForce = 6;
        }
        if (btnForward.timer < 1)
        {
            chagreForce = 3;
        }

    }

    IEnumerator Charge()
    {
        while (btnForward.IsTouch)
        {
            Debug.Log("charge");
            currentPower -= jumpCost;
            HandlePower();
            ChargePower();
            yield return new WaitForSeconds(1);
        }
    }

    /*public IEnumerator Recuit()
    {
        while (!btnForward.IsTouch && !btnBack.IsTouch)
        {
            Debug.Log("recuit");
            currentPower += 5f;
            HandlePower();
            yield return new WaitForSeconds(1);
            Debug.Log("hmm");
        }
    }*/


}
