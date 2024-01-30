using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RecollectableType //ho posam fora de sa clase pq �s usat per tot es projecte
{
    attackPotion,
    healthPotion,
    ingredients
}

[CreateAssetMenu(fileName ="New Recolectable",menuName ="Recollectable")]
public class Recollectable : ScriptableObject
{
    //jo voldria fer 3 classes diferents i poder diferenciar ses variables segons es tipo que fos;
    //osigui, si �s una pocion trair a nes principi es tipo i despr�s que te surtin ses variables de ses pocions
    //si �s un ingredient, pues ses variables des ingredients

    //GENERIC FOR ALL:
    public new string name;

    //aix� guarda es tipus de enum
    public RecollectableType recollectableType;

    public Sprite sprite;
    //public ParticleSystem particleSystem;

    //HEALTH POTION && ATTACK POTION
    public string cureOf;
    public ScriptableObject[] ingredientsForMakeThePotion;
    public ScriptableObject potionThatUnlocks;

    public int damage;

    public bool stackable;

   
}
