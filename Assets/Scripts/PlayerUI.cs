using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerUI : MonoBehaviour
{
    //[SerializeField] private TextMeshPro recollectedText;
    //private Player player;

    // Start is called before the first frame update
    void Start()
    {
        //recollectedText.gameObject.SetActive(false); //set it to false until the player go near
        ////player = gameObject.GetComponentInParent<Player>();
    }

    public void ShowRecollectableNameText(string text, Vector2 pos)
    {
        ////recollectedText.transform.position = pos;
        //recollectedText.gameObject.SetActive(true);
        //recollectedText.text = text; //set the name
    }
    public void HideRecollectableNameText() { 
        //recollectedText.gameObject.SetActive(false); 
    }
    //private Vector2 SetTextPos()
    //{
    //    Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
    //    int i = 0;
    //    foreach (Vector2 dir in directions)
    //    {
    //        if (player.RecollectableInFrontOf(dir))
    //        {
    //            switch (dir)
    //            {
    //                case Vector2 when dir == Vector2.up:
    //                    return new Vector2(0, 2.5f);
    //                case Vector2 when dir == Vector2.right:
    //                    return new Vector2(2.5f, 0);
    //                case Vector2 when dir == Vector2.down:
    //                    return new Vector2(0, -2.5f);
    //                case Vector2 when dir == Vector2.left:
    //                    return new Vector2(-2.5f, 0);
    //            }
    //        }
    //    }
    //    // Default position if no recollectable found in any direction
    //    return Vector2.zero;
    //}
}
