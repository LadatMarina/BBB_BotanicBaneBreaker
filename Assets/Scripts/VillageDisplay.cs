using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class VillageDisplay : MonoBehaviour
{
    //all the buttons and UI elements are asigned in the inspector
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI diseaseText;
    public TextMeshProUGUI potionText;
    public Image npcImage;
    public Image potionImage;
    public GameObject background;
    public Transform potionField;
    public Transform givePotionButton;
    //public GameObject inventoryPanel;   

    public Village village;
    public bool hasSelectedAPotion;
    Animator npcAnimator;

    private Recollectable potion = null;

    void Start()
    {
        SetVillage();
        hasSelectedAPotion = false; //això tampoc se exactament on me conve més, si aqui o a nes potionmanger
        givePotionButton.gameObject.SetActive(false);
        potion = null;
    }
    private void SetVillage()
    {
        village = DataPersistanceManager.Instance.LoadVillage();

        nameText.text = village.name;
        diseaseText.text = "Disease: " + village.disease;
        potionText.text = "Potion needed: " + village.potion.name;
        potionImage.sprite = village.potion.sprite;
        npcAnimator = npcImage.GetComponent<Animator>();
        npcAnimator.runtimeAnimatorController = village.animator.runtimeAnimatorController;

        Image imageBackground = background.GetComponent<Image>();
        imageBackground.color = village.backgroundColor;

        //set at the begining the value of the animator and the potion field button 
        SetPotionField(potionField, village);
    }

    //això és UI per tant se fa des de aquis
    public void RefreshPotionField(Recollectable recollectableToRefresh)
    {
        Image potionFieldImage = potionField.GetComponent<Image>();
        potionFieldImage.sprite = recollectableToRefresh.sprite;
    }

    public void ShowInventoryAndUnableButton(Button potionFieldButton)
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.toggleButtonSound);
        UI_Inventory.Instance.ToggleInventoryButton();
        potionFieldButton.interactable = false;
    }

    public void SetPotionField(Transform potionField, Village village)
    {
        //if the npc is cured the button unables and the image of the button is the correct potion
        if (village.isCured == true)
        {
            npcAnimator.SetBool("isCured", true);

            Button potionFieldButton = potionField.GetComponent<Button>();
            potionFieldButton.interactable = false; //if it's cured, there's no option to choose again the potion

            Image potionFieldImage = potionField.GetComponent<Image>();
            potionFieldImage.sprite = PotionManager.Instance.GetHealthPotionFromDisease(village.disease).sprite; //set the sprite of the potion 
        }
        //if the npc isn't cured the button is able and a function is set to it
        else
        {
            Button potionFieldButton = potionField.GetComponent<Button>();
            potionFieldButton.interactable = true;
            potionFieldButton.onClick.AddListener(() => ShowInventoryAndUnableButton(potionFieldButton));

            npcAnimator.SetBool("isCured", false);
        }
    }
    public void CheckPotion()
    {
        //if the potion is the one that cures the village's disease,
        if (GetPotion() == PotionManager.Instance.GetHealthPotionFromDisease(village.disease))
        {
            village.isCured = true;
            npcAnimator.SetBool("isCured", true);
            ExplanationManagerUI.Instance.ShowAnExplanation($"CONGRAT'S YOU HAVE GIVE {village.name} THE CORRECT POTION", 20);
            givePotionButton.gameObject.SetActive(false);
            GameManager.Instance.HasWon();
        }
        else
        {
            ExplanationManagerUI.Instance.ShowAnExplanation($"YOU ALMOST KILL {village.name}!!!", 15);
        }

        UI_Inventory.Instance.HideInventory(); //--> mirar perquè aquí no me funciona, no se tanca quant se pitja es give
        SetPotion(null);
    }
    public void ChooseThePotionToGive(Item item)
    {
        //remove the item directly from the saved list in the json file
        DataPersistanceManager.Instance.RemoveOneItem(item); 

        ///if the player has not selected a potion from the inventory, will add the
        ///item that the button represents to the field potion in the village display and hide the inventoy
        if (hasSelectedAPotion == false)
        {
            SetPotion(item.itemSO); 
            RefreshPotionField(item.itemSO); 
            givePotionButton.gameObject.SetActive(true);
            UI_Inventory.Instance.HideInventory(); 
            hasSelectedAPotion = true;
        }
    }

    public void BackButton()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.toggleButtonSound);
        UI_Inventory.Instance.HideInventory();
        Loader.Load(SceneIndex.GamePlay);
    }

    public void SetPotion(Recollectable potionToSet) { potion = potionToSet; }
    public Recollectable GetPotion() { return potion; }
}
