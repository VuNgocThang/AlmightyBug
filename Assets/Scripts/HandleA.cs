using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HandleA : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsTouch = false;
    public GameObject player;

    public Player instance;

    float force = 1f;
    
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        IsTouch = true;
        instance.currentPower -= 15f;
        instance.HandlePower();
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        IsTouch = false;
        Movement();
    }
    /*private void FixedUpdate()
    {
        instance.currentPower += 0.75f;
        instance.HandlePower();
    }*/

    public void Movement()
    {
        float nextPosition = player.transform.position.x;
        player.transform.DOMoveX(nextPosition - force, 1f);
    }
}
