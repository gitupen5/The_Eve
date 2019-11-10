using System.Collections;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]


public class Tiling : MonoBehaviour
{
    public int offsetX = 2;                 //The offset so that we don't get any weird error.

    // these are used for checking if we need to instantiate stuff.
    public bool hasARightBuddy = false;    
    public bool hasLeftBuddy = false;

    public bool reverseScale = false;       //used if the object is not tilable.
    private float spriteWidth = 0f;          // the width of our element
    private Camera cam;
    private Transform myTransform;

    void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }

    
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
    }

    void Update()
    {
        // does it need buddies if not do nothing.
        if(hasLeftBuddy == false || hasARightBuddy == false)
        {
            // calculate the cameras extend (half the width) of what the camera can see in the world cordinates.
            float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;

            //calculate th ex position where the camera can see the edge of the sprite.
            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePositonLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;
            

            // checking if we can see the edge of the element and then calling MakeNewBuddy
            if(cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false)
            {
                MakeNewBuddy(1);
                hasARightBuddy = true;
            }
            else if (cam.transform.position.x <= edgeVisiblePositonLeft + offsetX && hasLeftBuddy == false)
            {
                MakeNewBuddy(-1);
                hasLeftBuddy = true;
            }


        }

        

    }
    //a function that creates a buddy on the side required.
    void MakeNewBuddy(int rightOrLeft)
    {

        //calculating the new position for our new buddy.
        Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        //instantiating our new buddy and storing him in a variable.
        Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

        //if not tilable just reverse the x size of our object to get rid of ugly seams.
        if (reverseScale == true)
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
        }

        newBuddy.parent = myTransform.parent;
        if (rightOrLeft > 0)
        {
            newBuddy.GetComponent<Tiling>().hasLeftBuddy = true;
        }
        else
        {
            newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
        }
    }


}
