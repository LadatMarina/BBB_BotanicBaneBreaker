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
    private float radiustouchingZone = 0.8f;
    private bool zone1;
    private bool zone2;
    private bool touchingZone;

    private LayerMask playerLayer;
    private RecollectableDisplay recollectableDisplay;
    private new Collider2D collider;
    private Player player;
    private Item item;
    private void Awake()
    {
        //get the player sorting layer to asign it in every recollectable
        player = FindObjectOfType<Player>();
        recollectableDisplay = gameObject.GetComponent<RecollectableDisplay>();


        playerLayer = LayerMask.GetMask("PlayerLayer");
        collider = GetComponent<Collider2D>();
        //set circle cast values
        
        direction = Vector2.one;
        distance = 2f;
    }
    private void Start()
    {
        item = recollectableDisplay.GetItem();
    }
    private void Update()
    {
        origin = new Vector2(transform.position.x, transform.position.y);

        zone1 = Physics2D.CircleCast(origin, radiusZone1, direction, distance, playerLayer);
        zone2 = Physics2D.CircleCast(origin, radiusZone2, direction, distance, playerLayer);
        touchingZone = Physics2D.CircleCast(origin, radiustouchingZone, direction, distance, playerLayer);


        if (!touchingZone) //if the player is not in the touching zone
        {
            //ensure that after the player goes away the item returns to its initial state
            if (!collider.isTrigger)
            {
                collider.isTrigger = true;
            }
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
        
        //touchingZone circle
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin, radiustouchingZone);
    }
}
