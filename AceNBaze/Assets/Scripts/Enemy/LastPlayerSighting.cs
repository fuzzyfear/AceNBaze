using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlayerSighting : MonoBehaviour
{
    public Vector3 postition = new Vector3(1000f, 1000f, 100f);
    public Vector3 resetPosition = new Vector3(1000f, 1000f, 100f);

    private void Update()
    {
        if(postition != resetPosition)
        {
            //Debug.Log("Alarm!");
        }
    }
}
