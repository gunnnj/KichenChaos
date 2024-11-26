using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVS : MonoBehaviour
{

    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualObjArray;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Instance_OnSelectedCounterChanged;
    }

    private void Instance_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach(var item in visualObjArray)
        {
            item.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (var item in visualObjArray)
        {
            item.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
