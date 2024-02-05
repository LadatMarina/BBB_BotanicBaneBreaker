using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Recollectable;

[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent (typeof(CircleCollider2D))]
[RequireComponent (typeof(RecollectableBehaviour))]
public class RecollectableDisplay : MonoBehaviour
{
    public Recollectable scriptableObject;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    [SerializeField] private RecollectableType recollectableType;
    [SerializeField] private new ParticleSystem particleSystem;

    private Item item;
    private Recollectable itemSO;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        circleCollider = GetComponent<CircleCollider2D> ();
        SetAllTheValues();
    }

    private void SetAllTheValues() //jo he de rebre sa info des de item, no de s'criptable object
    {
        circleCollider.radius = 0.5f;
        circleCollider.isTrigger = true;
        
        spriteRenderer.sprite = item.itemSO.sprite;
        this.name = item.itemSO.name;
        this.recollectableType = item.itemSO.recollectableType;
        //particleSystem = scriptableObject.particleSystem;
    }

    public Sprite GetRecollectableSprite()
    {
        return spriteRenderer.sprite;
    }

    public ParticleSystem GetRecollectableParticleSystem()
    {
        return particleSystem;
    }

    public RecollectableType GetRecollectableType()
    {
        return recollectableType;
    }

    public void SetItem2(Item item)
    {
        this.item.amount = item.amount;
        this.itemSO = item.itemSO;

    }

    public Item GetItem()
    {
        return item;
    }
}
