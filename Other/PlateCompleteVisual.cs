using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenobjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] public Image bread;
    [SerializeField] public Image chesse;
    [SerializeField] public Image tomato;
    [SerializeField] public Image meat;
    [SerializeField] public Image cabbage;

    [SerializeField] private List<KitchenobjectSO_GameObject> kitchenobjectSO_GameObjectList;

    [SerializeField] public PlateKitchenObj plateKitchenObj;


    private void Start()
    {
        plateKitchenObj.OnIngredientAdded += PlateKitchenObj_OnIngredientAdded;
        foreach (KitchenobjectSO_GameObject kitchenobjectSO_GameObject in kitchenobjectSO_GameObjectList)
        {
            kitchenobjectSO_GameObject.gameObject.SetActive(false);
        }
        bread.gameObject.SetActive(false);
        chesse.gameObject.SetActive(false);
        tomato.gameObject.SetActive(false);
        meat.gameObject.SetActive(false);
        cabbage.gameObject.SetActive(false);
    }

    private void PlateKitchenObj_OnIngredientAdded(object sender, PlateKitchenObj.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenobjectSO_GameObject kitchenobjectSO_GameObject in kitchenobjectSO_GameObjectList)
        {
            if(kitchenobjectSO_GameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenobjectSO_GameObject.gameObject.SetActive(true);
                Debug.Log(kitchenobjectSO_GameObject.kitchenObjectSO.name);
                if(kitchenobjectSO_GameObject.kitchenObjectSO.objectName.Equals("Bread")){
                    bread.gameObject.SetActive(true);
                }
                if(kitchenobjectSO_GameObject.kitchenObjectSO.objectName.Equals("Cheese Block Slices")){
                    chesse.gameObject.SetActive(true);
                }
                if(kitchenobjectSO_GameObject.kitchenObjectSO.objectName.Equals("Tomato Slice")){
                    tomato.gameObject.SetActive(true);
                }
                if(kitchenobjectSO_GameObject.kitchenObjectSO.objectName.Equals("MeatCooked")){
                    meat.gameObject.SetActive(true);
                }
                if(kitchenobjectSO_GameObject.kitchenObjectSO.objectName.Equals("Cabbage Slices")){
                    cabbage.gameObject.SetActive(true);
                }
            }
        }
    }
    public string GetBoolActive(){
        string Chesse = chesse.IsActive() ? "true" : "false";
        string Tomato = tomato.IsActive() ? "true" : "false";
        string Cabbage = cabbage.IsActive() ? "true" : "false";
        return Chesse + Tomato + Cabbage;
    }
    void Update()
    {
        GetBoolActive();
    }
}

