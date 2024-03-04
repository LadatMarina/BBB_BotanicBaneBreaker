using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RecollectableType //ho posam fora de sa clase pq és usat per tot es projecte
{
    attackPotion,
    healthPotion,
    ingredients
}

[CreateAssetMenu(fileName ="New Recolectable",menuName ="Recollectable")]
[System.Serializable]
public class Recollectable : ScriptableObject
{
    //GENERIC FOR ALL:
    public new string name;
    public RecollectableType recollectableType;
    public Sprite sprite;
    public string explanation;

    //HEALTH POTION && ATTACK POTION
    public Diseases cureOf;
    public ScriptableObject[] ingredientsForMakeThePotion;
    public ScriptableObject potionThatUnlocks;

    public int damage;

    public bool stackable;
}
