using UnityEngine;
using System.Collections;

public class FutileShader : MonoBehaviour {


    public float GlowAmount;
    public Color GlowColor;

    private Material m_Material;
	// Use this for initialization
	void Start () 
    {
        m_Material = GetComponent<Renderer>().material;
        Apply();
	}

    public void Apply()
    {
        m_Material.SetFloat("_GlowAmount", GlowAmount);
        m_Material.SetColor("_GlowColor", GlowColor);
    }
}
