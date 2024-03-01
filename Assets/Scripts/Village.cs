using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Diseases //ho posam fora de sa clase pq és usat per tot es projecte
{
    stomachAge,
    cold,
    constipated,
    diarrea
}

[CreateAssetMenu(fileName = "New Village", menuName = "Village")]
public class Village : ScriptableObject
{
    public new string name;
    public int age;
    public Diseases disease;
    public Recollectable potion;
    public Sprite sprite;
    public Color backgroundColor;
    public bool isCured = false;
    public Animator animator;
}
