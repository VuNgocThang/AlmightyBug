using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float power = 100f;
    public float currentPower;
    public Image progressBarFilling;

    private void Awake()
    {
        currentPower = power;
        progressBarFilling.fillAmount = currentPower / power;
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
}
