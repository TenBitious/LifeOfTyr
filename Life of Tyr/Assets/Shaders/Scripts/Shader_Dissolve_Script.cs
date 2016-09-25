using UnityEngine;
using System.Collections;

public class Shader_Dissolve_Script : MonoBehaviour {

    private Renderer m_Renderer;
    [Range(.5f,1)]
    public float dissolveAmount;
    [Range(.5f,3)]
    public float dissolveSpeed = 2f;

    private bool isDissolving = false;

	// Use this for initialization
	void Start () {
        m_Renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Set dissolve ammount
        m_Renderer.material.SetFloat("_DissolveAmount", dissolveAmount);
        if (isDissolving) HandleDissolving();

    }

    public void StartDissolve()
    {
        Debug.Log("Start dissolve");
        isDissolving = true;
    }

    void HandleDissolving()
    {
        dissolveAmount += dissolveSpeed * Time.deltaTime;
        if(dissolveAmount > 1)
        {
            dissolveAmount = 1;
            
            StopDissolve();
        }
        if(dissolveAmount < .5f)
        {
            dissolveAmount = .5f;
            StopDissolve();
        }
    }

    void StopDissolve()
    {
        Debug.Log("Stop dissolve");
        dissolveSpeed *= -1;
        isDissolving = false;
    }
}
