using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class touchBtn : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public GameObject button;
    public TextMeshProUGUI text;
    public AudioSource btnClick;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button != null)
        {
            text.fontSize +=10f;
            btnClick.Play();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (button != null)
        {
            text.fontSize-=10f;
        }
    }
}
