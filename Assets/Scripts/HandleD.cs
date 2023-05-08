using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;

public class HandleD : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsTouch = false;
    public GameObject player;
    public Player instance;

    float timer = -1;
    float force = 1f;
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        IsTouch = true;
        timer = 0;
        StartCoroutine(Delay());
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (IsTouch)
        {
            timer = -1;
        }
        IsTouch = false;
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (IsTouch)
        {
            timer = -1;
        }
        IsTouch = false;
        Movement();
        StopCoroutine(Delay());
    }

    private void HandleForce()
    {
        /*if (timer >= 0)
        {
            force = 1f;
            instance.currentPower -= 25f;
        }

        if (timer >= 1)
        {
            force = 4f;
            instance.currentPower -= 25f;
        }

        if (timer >= 2)
        {
            force = 7f;
            instance.currentPower -= 25f;
        }*/
        instance.currentPower -= 2f;
    }
    public void Movement()
    {
        float nextPosition = player.transform.position.x;
        player.transform.DOMoveX(nextPosition + force, 1f);
    }
    IEnumerator Delay()
    {
        while (IsTouch)
        {
            Debug.Log("Cong timeline");
            timer++;
            HandleForce();
            instance.HandlePower();
            yield return new WaitForSeconds(0.05f);
        }
    }
}
