using UnityEngine;
using System.Collections;

public class RaycastShootTriggerable : MonoBehaviour
{

    [HideInInspector] public LineRenderer laserLine;                    // Reference to the LineRenderer component which will display our laserline.
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);     // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible.


    public void Initialize()
    {
        //Get and store a reference to our LineRenderer component
        laserLine = GetComponent<LineRenderer>();
    }

}