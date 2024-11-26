using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ContainerCounter : BaseCounter 
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public AudioSource pick;

    public override void Interact(Player player)
    {
        pick.Play();
        KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);

    }

   
}
