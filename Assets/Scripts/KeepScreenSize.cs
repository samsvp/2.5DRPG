using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepScreenSize : MonoBehaviour
{
    SpriteRenderer sr;
    Vector2 spriteDims;

#pragma warning disable IDE0051 // Remove unused private members
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        spriteDims = GetSpriteDimensions();
    }

    // Update is called once per frame
    void Update()
    {
        if (!CameraProjectionChange.isChanging)
        {
            spriteDims = GetSpriteDimensions();
            return;
        }
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        Vector2 currentDimensions = GetSpriteDimensions();
        // Get how much the GO needs to be scaled by to keep the same dimensions
        Vector2 scale2 = spriteDims / currentDimensions;
        Vector3 scale = new Vector3(scale2.x, scale2.y, 1);
        // If the scale is one do nothing. Floating point errors occur when multiplying
        // the localScale by one and reassigning the value to transform.localScale
        if (scale != Vector3.one) transform.localScale = Vector3.Scale(transform.localScale, scale);
        
        spriteDims = GetSpriteDimensions();
    }
#pragma warning restore IDE0051 // Remove unused private members

    /// <summary>
    /// Gets the sprite dimensions on screen
    /// </summary>
    /// <returns></returns>
    private Vector2 GetSpriteDimensions()
    {
        Camera camera = Camera.main;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);

        Vector3 cMin = camera.WorldToScreenPoint(sr.bounds.min);
        Vector3 cMax = camera.WorldToScreenPoint(sr.bounds.max);

        cMin.x -= camera.pixelRect.x;  // adjust for camera position on screen
        cMin.y -= camera.pixelRect.y;
        cMax.x -= camera.pixelRect.x;
        cMax.y -= camera.pixelRect.y;

        float width = Mathf.Abs(cMax.x - cMin.x);
        float height = Mathf.Abs(cMax.y - cMin.y);

        if (float.IsInfinity(width) || float.IsInfinity(height)) return Vector2.one;

        return new Vector2(width / camera.pixelWidth, height / camera.pixelHeight);
    }
}
