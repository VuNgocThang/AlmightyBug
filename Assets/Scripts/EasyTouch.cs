using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class EasyTouch : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
    public bool IsTouch = false;
    Action onTouch;
    Action<float> onRelease;

    public void Init(Action onTouch, Action<float> onRelease)
    {
        this.onTouch = onTouch;
        this.onRelease = onRelease;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        IsTouch = true;
        onTouch?.Invoke();
        timer = 0;
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (IsTouch)
        {
            onRelease?.Invoke(timer);
            timer = -1;
        }
        IsTouch = false;
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (IsTouch)
        {
            onRelease?.Invoke(timer);
            timer = -1;
        }
        IsTouch = false;
    }

    float timer = -1;
    void Update()
    {
        //if (timer >= 0) timer += Ez.TimeMod;
    }
}
