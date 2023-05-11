using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{

    [SerializeField] Image hpBar;
    [SerializeField] Image staminaBar;
    public void UpdateHp(float hp, float hpMax)
    {
        hpBar.fillAmount = hp/hpMax;
    }
    public void UpdateStamina(float stamina, float staminaMax)
    {
        staminaBar.fillAmount = stamina/staminaMax;
    }
}
