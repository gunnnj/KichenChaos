using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Instruction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject showObj;
    public GameObject btnQuest;
    // Start is called before the first frame update
    void Start()
    {
        showObj.SetActive(false);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (btnQuest != null)
        {
            showObj.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (btnQuest != null)
        {
            showObj.SetActive(false);
        }
    }
}
