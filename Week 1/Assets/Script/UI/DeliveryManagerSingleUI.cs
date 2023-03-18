using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI RecipeNameText;
    [SerializeField] private Transform IconCountainer;
    [SerializeField] private Transform IconTemplate;

    private void Awake()
    {
        IconTemplate.gameObject.SetActive(false);
    }
    public void SetRecipeSO(RecipeSO recipeSO)
    {
        RecipeNameText.text = recipeSO.RecipeName;
        
        foreach (Transform child in IconCountainer)
        {
            if (child == IconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.KitchenOBjectSOList)
        {
            Transform iconTransform = Instantiate(IconTemplate, IconCountainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.Sprite;
        }
    }
}
