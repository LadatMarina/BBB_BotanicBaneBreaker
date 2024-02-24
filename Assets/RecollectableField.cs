using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecollectableField : MonoBehaviour
{
    public Recollectable recollectable;
    public Transform potionField;
    // Start is called before the first frame update
    void Start()
    {
        recollectable = null;
        Button potionFieldButton = potionField.GetComponent<Button>();
        potionFieldButton.interactable = true;
        potionFieldButton.onClick.AddListener(() => ShowInventoryAndUnableButton(potionFieldButton));
    }


    private void ShowInventoryAndUnableButton(Button potionFieldButton)
    {
        GameManager.Instance.ToggleInventoryButton();
        potionFieldButton.interactable = false;
    }


}
