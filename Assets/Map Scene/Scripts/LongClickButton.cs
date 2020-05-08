using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class LongClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown;
    private float pointerDownTimer;

    public float requiredHoldTime;

    public UnityEvent onLongClick;

    public GameObject panel;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pointerDown)
        {
           
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer > requiredHoldTime)
            {
                if (onLongClick != null)
                    onLongClick.Invoke();
                panel.gameObject.SetActive(true);
                Reset();
            }
        }
    }
    private void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;
    }


}
