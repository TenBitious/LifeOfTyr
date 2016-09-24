using UnityEngine;
using System.Collections;

public class ShootHook : MonoBehaviour {

    public Hook m_Hook;

	// Use this for initialization
	void Start () 
    {
        PlayerEventManager.OnMouseLeft += Shoot;
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    void Shoot()
    {
        if (!PlayerStats.Instance.Shooting_Hook)
        {
            Hook hook = Instantiate(m_Hook) as Hook;
            Vector3 start_Position = Camera.main.transform.position + Camera.main.transform.forward;
            hook.StartShoot(PlayerStats.Instance.shoot_Distance, Camera.main.transform.forward, start_Position, Camera.main.transform.rotation);
        }
    }
}
