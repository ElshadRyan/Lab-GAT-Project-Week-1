using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisuals : MonoBehaviour
{
    [SerializeField] private PlateCounter PlatesCounter;
    [SerializeField] private Transform CounterTopPoint;
    [SerializeField] private Transform PlateVisualPrevab;

    private List<GameObject> PlateVisualGameObjectList;

    private void Awake()
    {
        PlateVisualGameObjectList = new List<GameObject>();
    }
    private void Start()
    {
        PlatesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        PlatesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject PlateGameObject = PlateVisualGameObjectList[PlateVisualGameObjectList.Count - 1];
        PlateVisualGameObjectList.Remove(PlateGameObject);
        Destroy(PlateGameObject);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform PlateVisualTransform =  Instantiate(PlateVisualPrevab, CounterTopPoint);
        float PlateOffsetY = .1f;
        PlateVisualTransform.localPosition = new Vector3(0, PlateOffsetY * PlateVisualGameObjectList.Count, 0);

        
        PlateVisualGameObjectList.Add(PlateVisualTransform.gameObject);
    }
}
