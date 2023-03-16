using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter CuttingCounter;
    [SerializeField] private Image BarImage;

    private void Start()
    {
        CuttingCounter.OnProgressChange += CuttingCounter_OnProgressChange;
        BarImage.fillAmount = 0;
        Hide();
    }

    private void CuttingCounter_OnProgressChange(object sender, CuttingCounter.OnProgressChangedEventArgs e)
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
