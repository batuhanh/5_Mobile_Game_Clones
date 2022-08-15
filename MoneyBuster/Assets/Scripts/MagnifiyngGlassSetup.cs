using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifiyngGlassSetup : MonoBehaviour {

    [SerializeField] private Material material;

    private void Update() {
        Vector2 screenPixels = Camera.main.WorldToScreenPoint(transform.position);
        screenPixels = new Vector2(screenPixels.x / Screen.width, screenPixels.y / Screen.height);

        material.SetVector("_ObjectScreenPosition", screenPixels);
    }

}