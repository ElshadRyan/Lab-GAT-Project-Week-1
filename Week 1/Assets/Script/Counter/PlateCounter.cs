using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;


    [SerializeField] private KitchenObjectSO PlateKitchenObjectSO;
    [SerializeField] private float SpawnPlateTimerMax = 4f;
    private float SpawnPlateTimer;
    private int PlatesSpawnedAmount;
    private int PlatesSpawnedAmountMAX = 4;

    private void Update()
    {
        SpawnPlateTimer += Time.deltaTime;
        if(SpawnPlateTimer > SpawnPlateTimerMax)
        {
            SpawnPlateTimer = 0f;
            

            if (PlatesSpawnedAmount < PlatesSpawnedAmountMAX)
            {
                PlatesSpawnedAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public override void Interact(Player Player)
    {
        if (!Player.HasKitchenObject())
        {
            if (PlatesSpawnedAmount > 0)
            {
                PlatesSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(PlateKitchenObjectSO, Player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }

        }
    }
}
