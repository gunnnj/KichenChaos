using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    

    private IKitchenObjectParent kitchenObjectParent;
    public KitchenObjectSO GetKitchenObjectSO() { 
        return kitchenObjectSO;
    }

    public void SetKitchanObjectParent(IKitchenObjectParent kitchenObjectParent)
    {   // Kiểm tra trên bàn có gì không? -> Clear
        if(this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;
        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.Log("Co tren ban");
        }

        //Set lại Bàn
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetKitchenobjectFollowTransform(); //Trả lại đối tượng
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent() { 
        return kitchenObjectParent;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObj plateKitchenObj)
    {
        if(this is PlateKitchenObj)
        {
            plateKitchenObj = this as PlateKitchenObj;
            return true;
        }
        else
        {
            plateKitchenObj=null;
            return false;
        }
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {     
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject =  kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchanObjectParent(kitchenObjectParent);
        return kitchenObject;

    }
    
}
