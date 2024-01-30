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
        spriteRenderer.sprite = scriptableObject.sprite;
        this.name = scriptableObject.name;
        this.recollectableType = scriptableObject.recollectableType;
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
}
