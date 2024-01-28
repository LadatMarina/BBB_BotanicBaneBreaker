using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName ="New Recolectable",menuName ="Recollectable")]
public class Recollectable : ScriptableObject
{
    //jo voldria fer 3 classes diferents i poder diferenciar ses variables segons es tipo que fos;
    //osigui, si és una pocion trair a nes principi es tipo i després que te surtin ses variables de ses pocions
    //si és un ingredient, pues ses variables des ingredients

    //GENERIC FOR ALL:
    public new string name;

    public string recollectableType;

    /*public enum RecollectableType
    {
        attackPotion,
        healthInteractions,
        ingredients
    }*/

    public Sprite sprite;
    //public ParticleSystem particleSystem;

    //HEALTH POTION && ATTACK POTION
    public string cureOf;
    public ScriptableObject[] ingredientsForMakeThePotion;
    public ScriptableObject potionThatUnlocks;

    public int damage;

    //INGREDIENT


    //public class[] typeOfRecolectable;
    /*public class HealthPotion
    {
        
    }*/
}
