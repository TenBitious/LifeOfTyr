using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    BasicAI ai = new DefaultAI();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ai.UpdateBehaviour();
	}
}
