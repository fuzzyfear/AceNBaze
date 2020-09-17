using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideOnPlay : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private void Awake()
    {
        transform.parent = null;
        for(int i = 0; i < transform.childCount; i++)
        {
            meshRenderer = transform.GetChild(i).GetComponent<MeshRenderer>();
            if(meshRenderer != null)
                meshRenderer.enabled = false;
        }
    }
}
