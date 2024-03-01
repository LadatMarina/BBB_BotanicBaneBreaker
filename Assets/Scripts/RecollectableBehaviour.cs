using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecollectableBehaviour :MonoBehaviour
{
    private Vector2 origin;
    private Vector2 direction;
    private float distance;
    private float radiusZone1 = 3f;
    private float radiusZone2 = 1.2f;
    private bool zone1;
    private bool zone2;
    private LayerMask playerLayer;
    private RecollectableDisplay recollectableDisplay;

    private void Awake()
    {
        //get the player sorting layer to asign it in every recollectable
        //player = GameObject.Find("Player");
        playerLayer = LayerMask.GetMask("PlayerLayer");
        
        //set circle cast values
        
        direction = Vector2.one;
        distance = 2f;
    }
    private void Start()
    {
        recollectableDisplay = gameObject.GetComponent<RecollectableDisplay>();
    }
    private void Update()
    {
        origin = new Vector2(transform.position.x, transform.position.y);

        zone1 = Physics2D.CircleCast(origin, radiusZone1, direction, distance, playerLayer);
        zone2 = Physics2D.CircleCast(origin, radiusZone2, direction, distance, playerLayer);

        if (zone1)
        {
            recollectableDisplay.ShowRecollectedText();

            //particle system play or light is shown
            //Debug.Log("the player has entered in the zone 1 ");
        }
        else
        {
            recollectableDisplay.HideRecollectedText();
        }

        if (zone2)
        {
            //descrition pop-up
            //Debug.Log("the player has entered in the zone 2");
        }
        else
        {
            recollectableDisplay.HideRecollectedText();

        }
    }

    private void OnDrawGizmos()
    {
        //zone 1 circle
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(origin, radiusZone1);

        //zone 2 circle
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(origin, radiusZone2);
    }
}
