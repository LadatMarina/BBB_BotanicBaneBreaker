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
    [SerializeField] private Recollectable.RecollectableType recollectableType;
    [SerializeField] private new ParticleSystem particleSystem;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        circleCollider = GetComponent<CircleCollider2D> ();
        SetAllTheValues();
    }

    private void SetAllTheValues() 
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

    public Recollectable.RecollectableType GetRecollectableType()
    {
        return recollectableType;
    }
}
