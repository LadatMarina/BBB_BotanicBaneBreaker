using UnityEngine;
using TMPro;

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
    private Item item;


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

    private void SetAllTheValues() 
    {
        circleCollider.radius = 0.5f;
        circleCollider.isTrigger = true;

        spriteRenderer.sprite = item.itemSO.sprite;
        this.name = item.itemSO.name;
        this.recollectableType = item.itemSO.recollectableType;

        if (amountText != null)
        {
            amountText.text = $"{item.amount}";
        }
    }

    public Sprite GetRecollectableSprite() { return spriteRenderer.sprite; }

    public ParticleSystem GetRecollectableParticleSystem() { return particleSystem; }

    public RecollectableType GetRecollectableType() { return recollectableType; }

    public void SetItem(Item item) { this.item = item; }
    public Item GetItem() { return item; }
    public void Reset() { amountText.text = $"{item.amount}"; }
}
