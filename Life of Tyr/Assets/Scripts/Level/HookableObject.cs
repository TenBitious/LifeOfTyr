using UnityEngine;
using System.Collections;

public class HookableObject : MonoBehaviour {

    private Collider m_Colllider;

	// Use this for initialization
	void Start () {
        m_Colllider = GetComponent<BoxCollider>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HookHit()
    {

    }

    public void HookRelease()
    {

    }

    void OnDrawGizmos()
    {
        m_Colllider = GetComponent<Collider>();
        //Show in editor that this object can be grabbed
        Vector3 topOfObject = new Vector3(transform.position.x, transform.position.y + m_Colllider.bounds.extents.y, transform.position.z);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(topOfObject, .5f);
    }
    
    
}
