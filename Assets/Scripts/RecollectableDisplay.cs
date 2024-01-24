using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent (typeof(CircleCollider2D))]
[RequireComponent (typeof(RecollectableBehaviour))]
public class RecollectableDisplay : MonoBehaviour
{
    public Recollectable scriptableObject;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    [SerializeField] private string type;
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
        this.type = scriptableObject.type;
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

    public string GetRecollectableType()
    {
        return type;
    }
}
