using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float outlineScaleFactor;
    [SerializeField] private Color outlineColor;
    [SerializeField] private GameObject outlineObject;
    [SerializeField] private Renderer outlineRenderer;

    void Start()
    {
        //outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
        outlineRenderer.enabled = true;
    }

    private void Update()
    {
        CreateOutline(outlineRenderer, outlineMaterial, outlineScaleFactor, outlineColor);
    }

    Renderer CreateOutline(Renderer rend,Material outlineMat, float scaleFactor, Color color)
    {
        //GameObject obj = Instantiate(this.outlineObject, transform.position, transform.rotation, outlineObject.transform);
        //Renderer rend = obj.GetComponentInChildren<Renderer>();

        rend.material = outlineMat;
        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetFloat("_Scale", scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        //obj.GetComponent<Outline>().enabled = false;
        //obj.GetComponent<Collider>().enabled = false;

        //rend.enabled = false;

        return rend;
    }
}
