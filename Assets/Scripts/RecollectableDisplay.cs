using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
[RequireComponent (typeof(CircleCollider2D))]
public class RecollectableDisplay : MonoBehaviour
{
    [SerializeField] private Recollectable scriptableObject;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    public string type;

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
    }
}
