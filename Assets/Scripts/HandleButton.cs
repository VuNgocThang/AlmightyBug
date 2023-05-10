using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandleButton : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
    public PlayerManager PlayerManager;
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
        //onTouch?.Invoke();
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
            onTouch?.Invoke();
            onRelease?.Invoke(timer);
            timer = -1;
        }
        IsTouch = false;
        StartCoroutine(Recuit());

        if (PlayerManager.currentPower > 100f)
        {
            StopCoroutine(Recuit());
        }
    }

    public float timer = -1;
    void Update()
    {
        timer += Time.deltaTime;
    }

    public IEnumerator Recuit()
    {
        while (!IsTouch)
        {
            Debug.Log("recuit");
            PlayerManager.currentPower += 5f;
            PlayerManager.HandlePower();
            yield return new WaitForSeconds(1);
            Debug.Log("hmm");
        }
    }
}
