using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandleButton : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
    public PlayerController playerController;

    public bool IsTouch = false;
    Action onTouch;
    //Action<float> onRelease;

    /*public void Init(Action onTouch, Action<float> onRelease)
    {
        this.onTouch = onTouch;
        this.onRelease = onRelease;
    }*/
    public void Init(Action onTouch)
    {
        this.onTouch = onTouch;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        IsTouch = true;
        //onTouch?.Invoke();
        timer = 0;
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
            onTouch?.Invoke();
            timer = -1;
        }
        IsTouch = false;

    }
    public float timer = -1;
    void Update()
    {
        timer += Time.deltaTime;
    }
}
