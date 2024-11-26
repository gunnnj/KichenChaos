using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter
{
    private enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingRecipeSO[] FryingRecipeSOArray;

    [SerializeField] private GameObject[] visualObjArray;

   



    //private void Start()
    //{
    //    StartCoroutine(HandleFryTimer());
    //}
    //private IEnumerator HandleFryTimer()
    //{
    //    yield return new WaitForSeconds(1f);
    //}

    private State state;
    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;

    public AudioSource SoundPan;
    private Coroutine coroutine;
    public AudioSource pick;
    private void Start()
    {
        state = State.Idle;
    }
    IEnumerator SoundPlay(){
        SoundPan.Play();
        yield return new WaitForSeconds(1f);
    }
    private void Update()
    {
        Hide();
       
        if (HasKitchenObject())
        {
            if(coroutine==null){
                coroutine = StartCoroutine(SoundPlay());
            }
            
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    
                    fryingTimer += Time.deltaTime;
                    Show();
                   
                    if (fryingTimer > fryingRecipeSO.fryingTimeMax)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        Debug.Log("Object Fried!");

                        state = State.Fried;
                      
                        
                    }
                    
                    break;
                case State.Fried:
                    SoundPan.Stop();
                    coroutine = null;
                    
                    
                    break;
                case State.Burned:
                    break;
            }
            Debug.Log(state);

        }
    }
    public override void Interact(Player player)
    {   
        pick.Play();
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchanObjectParent(this);


                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;
                    fryingTimer = 0f;
                }
                else
                {
                    Debug.Log("No cutting");
                }

            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObj plateKitchenObj))
                {// Player hoding a plate
                    if (plateKitchenObj.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchanObjectParent(player);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(kitchenObjectSO);

        return fryingRecipeSO != null;
    }
    private KitchenObjectSO GetOutForIn(KitchenObjectSO kitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(kitchenObjectSO);

        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }
    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO item in FryingRecipeSOArray)
        {
            if (item.input == inputKitchenObjectSO)
            {
                return item;
            }
        }
        return null;
    }


    private void Show()
    {
        foreach (var item in visualObjArray)
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

}
