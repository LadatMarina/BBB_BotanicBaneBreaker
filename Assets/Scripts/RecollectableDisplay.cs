using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Recollectable;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Jobs;

[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent (typeof(CircleCollider2D))]
[RequireComponent (typeof(RecollectableBehaviour))]
public class RecollectableDisplay : MonoBehaviour
{
    //public Recollectable scriptableObject;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    [SerializeField] private RecollectableType recollectableType;
    [SerializeField] private new ParticleSystem particleSystem;

    [SerializeField] private TextMeshPro amountText;
    //[SerializeField] private TextMeshPro recollectedText;


    private Item item;
    //private Recollectable itemSO;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        amountText = transform.Find("amountText").GetComponent<TextMeshPro>();
    }
    void Start()
    {
        SetAllTheValues();
    }

    private void SetAllTheValues() //jo he de rebre sa info des de item, no de s'criptable object
    {
        circleCollider.radius = 0.5f;
        circleCollider.isTrigger = true;

        //recollectedText.gameObject.SetActive(false); //set it to false until the player go near
        //recollectedText.text = item.itemSO.name; //set the name

        spriteRenderer.sprite = item.itemSO.sprite;
        this.name = item.itemSO.name;
        this.recollectableType = item.itemSO.recollectableType;
        //particleSystem = scriptableObject.particleSystem;

        if (amountText != null)
        {
            amountText.text = $"{item.amount}";
            //Debug.Log("amount changet succesfully");
        }
        else
        {
            Debug.Log("text malament");
        }

    }

    public Sprite GetRecollectableSprite() { return spriteRenderer.sprite; }

    public ParticleSystem GetRecollectableParticleSystem() { return particleSystem; }

    public RecollectableType GetRecollectableType() { return recollectableType; }

    public void SetItem(Item item) { this.item = item; }
    public Item GetItem() { return item; }
    public void Reset() { amountText.text = $"{item.amount}"; }
}
