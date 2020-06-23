using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public SwipeBody parent;
    float maxLength
    {
        get
        {
            return Screen.width * currentOptions.MaxLengthPerc;
        }
    }
    public bool interact = false;
    Vector2 startSwipe;
    public SwipeAnimPoint targetPoint;
    SwipeOptions currentOptions = new SwipeOptions();

    public void SetOptions(SwipeOptions opt)
    {
        currentOptions = opt;
    }

    // Update is called once per frame
    void Update()
    {
        if (!interact)
        {
            transform.position = Vector3.Lerp(transform.position, targetPoint.position, currentOptions.backToPosSpeed * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, targetPoint.scale, currentOptions.backToPosSpeed * Time.deltaTime);
            return;
        }

        if (parent.current != this)
            return;

        if (Input.GetMouseButtonUp(0))
        {
            MouseUP();
            return;
        }
        float dif = Input.mousePosition.x - startSwipe.x;
        float perc = Mathf.Clamp01(Mathf.Abs(dif / maxLength));
        if (perc < currentOptions.minPercentForAnim)
            return;
        if(perc >= 1)
        {
            MouseUP();
            return;
        }

        if (dif > 0)
        {
            parent.SetPercAnim(parent.centerPoint, parent.rightPoint, perc * currentOptions.animationPercent, -1);
        }
        else if (dif < 0)
        {
            parent.SetPercAnim(parent.centerPoint, parent.leftPoint, perc * currentOptions.animationPercent, 1);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        interact = true;
        startSwipe = Input.mousePosition;
    }

    void MouseUP()
    {
        interact = false;
        if (parent.current != this)
            return;
        float dif = Input.mousePosition.x - startSwipe.x;
        float perc = Mathf.Clamp01(Mathf.Abs(dif / maxLength));

        if (dif > 0)
        {
            if (perc == 1)
            {
                parent.SwipeRight();
            }
            else
            {
                parent.Refresh();
            }
        }
        else if (dif < 0)
        {
            if (perc == 1)
            {
                parent.SwipeLeft();
            }
            else
            {
                parent.Refresh();
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        MouseUP();
    }


}
