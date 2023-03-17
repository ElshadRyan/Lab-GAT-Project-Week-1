using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter BaseCounter;
    [SerializeField] private GameObject[] VisualGameObjectArray;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChange += Player_OnSelectedCounterChange;
    }

    private void Player_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangeEventArgs e)
    {
        if(e.SelectedCounter == BaseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject VisualGameObject in VisualGameObjectArray)
        {
            VisualGameObject.SetActive(true);
        }
        
    }

    private void Hide()
    {
        foreach (GameObject VisualGameObject in VisualGameObjectArray)
        {
            VisualGameObject.SetActive(false);
        }
    }
}
