using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter //Kế thừa lớp Interface
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public AudioSource pick;


    public override void Interact(Player player)
    {
        pick.Play();
        if (!HasKitchenObject()) // There is no obj
        {
            if (player.HasKitchenObject()) // Player is carrying something
            {
                player.GetKitchenObject().SetKitchanObjectParent(this); 
            }
        }
        else
        { // There is has KC Object
            if (player.HasKitchenObject()) // Player carrying something
            {
                              
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObj plateKitchenObj))
                {// Player hoding a plate
                    if (plateKitchenObj.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                    }
                }
                else
                {
                    //Player is not carrying a plate but something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObj))
                    {
                        //Counter is holding a Plate
                        if (plateKitchenObj.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }


                }
                    
                
            }
            else
            {
                GetKitchenObject().SetKitchanObjectParent(player);
            }
        }
    }
    


}
