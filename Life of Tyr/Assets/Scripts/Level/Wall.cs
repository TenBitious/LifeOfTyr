using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Shader_Dissolve_Script))]
public class Wall : MonoBehaviour {

    private BoxCollider m_Colllider;
    
    private Shader_Dissolve_Script m_Shader_Dissolve;

	// Use this for initialization
	void Start () {
        m_Colllider = GetComponent<BoxCollider>();
        m_Shader_Dissolve = GetComponent<Shader_Dissolve_Script>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HookHit()
    {
        m_Shader_Dissolve.StartDissolve();
    }

    public void HookRelease()
    {
        m_Shader_Dissolve.StartDissolve();
    }

    void OnDrawGizmos()
    {
        m_Colllider = GetComponent<BoxCollider>();
        //Show in editor that this object can be grabbed
        Vector3 topOfObject = new Vector3(transform.position.x, transform.position.y + m_Colllider.bounds.extents.y, transform.position.z);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(topOfObject, .5f);
    }
    
    
}
