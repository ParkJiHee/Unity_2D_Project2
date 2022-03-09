using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    public float scrollSpeed = 0.05f;

    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float newOffsetX = material.mainTextureOffset.x + scrollSpeed * Time.deltaTime;
        Vector2 newOffset = new Vector2(newOffsetX, 0);

        material.mainTextureOffset = newOffset;
    }
}
