using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{

    public event EventHandler OnPlatesSpawned;
    public event EventHandler OnPickPlate;

    [SerializeField] private KitchenObjectSO plateKitchenObj;

    private float spawnPlateTimer;
    private float spawnPlateTimeMax=4f;
    private int SpawnAmount;
    private int SpawnAmountMax = 4;
    public AudioSource pick;
    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateTimeMax)
        {
            spawnPlateTimer = 0f;

            if (SpawnAmount < SpawnAmountMax)
            {
                SpawnAmount++;
                OnPlatesSpawned?.Invoke(this, EventArgs.Empty);
            }

        }
    }
    public override void Interact(Player player)
    {
        pick.Play();
        if (!player.HasKitchenObject())
        {
            if (SpawnAmount > 0)
            {
                SpawnAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObj, player);

                OnPickPlate?.Invoke(this, EventArgs.Empty);
            }
        }
    }

}
