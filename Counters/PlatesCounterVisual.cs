using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform platePrefab;

    private List<GameObject> plateList;

    private void Awake()
    {
        plateList = new List<GameObject>();
    }
    private void Start()
    {
        platesCounter.OnPlatesSpawned += PlatesCounter_OnPlatesSpawned;
        platesCounter.OnPickPlate += PlatesCounter_OnPickPlate;
    }

    private void PlatesCounter_OnPickPlate(object sender, System.EventArgs e)
    {
        GameObject plateGO = plateList[plateList.Count - 1];
        plateList.Remove(plateGO);
        Destroy(plateGO);

    }

    private void PlatesCounter_OnPlatesSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(platePrefab, counterTopPoint);

        float plateOffSetY = .1f;
        plateVisualTransform.localPosition = new Vector3(0,plateOffSetY*plateList.Count,0);

        plateList.Add(plateVisualTransform.gameObject);

    }
}
