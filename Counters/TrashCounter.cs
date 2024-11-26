using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter 
{
    public AudioSource pick;
    public override void Interact(Player player)
    {
        pick.Play();
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
        }

    }
}
