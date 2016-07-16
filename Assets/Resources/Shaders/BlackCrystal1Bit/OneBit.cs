using UnityEngine;
using System.Collections;
 
[ExecuteInEditMode]
public class OneBit : MonoBehaviour {
 
  public Color white, black;
  public bool dithering;
  private Material material;

  void Awake () {
    material = new Material(Shader.Find("Hidden/Onebit"));
  }
  
  void OnRenderImage (RenderTexture source, RenderTexture destination) {
    material.SetColor("_White", white);
    material.SetColor("_Black", black);
    material.SetFloat("_Dithering", dithering ? 1f : 0f);

    Graphics.Blit(source, destination, material);
  }
}