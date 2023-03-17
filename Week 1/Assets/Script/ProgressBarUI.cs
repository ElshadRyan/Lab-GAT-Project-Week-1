using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject HasProgressGameObject;
    [SerializeField] private Image BarImage;

    private IProgressUIBar HasProgegress;
    private void Start()
    {
        HasProgegress = HasProgressGameObject.GetComponent<IProgressUIBar>();
        HasProgegress.OnProgressChange += HasProgress_OnProgressChange;
        BarImage.fillAmount = 0;
        Hide();
    }

    private void HasProgress_OnProgressChange(object sender, IProgressUIBar .OnProgressChangedEventArgs e)
    {
        BarImage.fillAmount = e.ProgressNormalized;
        if (e.ProgressNormalized == 0f || e.ProgressNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
