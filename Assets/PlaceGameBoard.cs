
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This allows us to user the AR Foundation simulator functions
using cs294_137.hw2;
public class PlaceGameBoard : MonoBehaviour
{
    // Public variables can be set from the unity UI.
    // We will set this to our Game Board object.
    public GameObject gameBoard;
    // These will store references to our other components.
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    // This will indicate whether the game board is set.
    private bool placed = false;

    // Start is called before the first frame update.
    void Start()
    {
        // GetComponent allows us to reference other parts of this game object.
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();

        //We want to place our board only on hortizontal planes. So we tell the plane manager only to detect those
        planeManager.detectionMode = PlaneDetectionMode.Horizontal;
    }

    // Update is called once per frame.
    void Update()
    {
        if (!placed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 touchPosition = Input.mousePosition;

                // Raycast will return a list of all planes intersected by the
                // ray as well as the intersection point.
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (raycastManager.Raycast(
                    touchPosition, ref hits, TrackableType.PlaneWithinPolygon))
                {
                    // The list is sorted by distance so to get the location
                    // of the closest intersection we simply reference hits[0].
                    var hitPosition = hits[0].hitPosition;
                    // Now we will activate our game board and place it at the
                    // chosen location.
                    gameBoard.SetActive(true);
                    gameBoard.transform.position = hitPosition;
                    placed = true;
                    // After we have placed the game board we will disable the
                    // planes in the scene as we no longer need them.
                    
                    planeManager.detectionMode = PlaneDetectionMode.None;

                }
            }
        }
        else
        {
            // The plane manager will set all detected planes to active by 
            // default so we will continue to disable these.
            //planeManager.SetTrackablesActive(false); //For older versions of AR foundation
            planeManager.detectionMode = PlaneDetectionMode.None;
        }
    }

    // If the user places the game board at an undesirable location we 
    // would like to allow the user to move the game board to a new location.
    public void AllowMoveGameBoard()
    {
        placed = false;
        //planeManager.SetTrackablesActive(true);
        planeManager.detectionMode = PlaneDetectionMode.Horizontal;
    }

    // Lastly we will later need to allow other components to check whether the
    // game board has been places so we will add an accessor to this.
    public bool Placed()
    {
        return placed;
    }
}
