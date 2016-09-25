using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyBehaviour : MonoBehaviour
{
    protected EnemyInfo enemyInfo;
    private Rigidbody rigidbody;
    public BasicAI ai;
    private GameObject[] players;
    private GameObject target;
    private Animation anim;
    public List<AnimationState> states;
    public Animation Anim { get { return anim; } }
    public Rigidbody Rigidbody { get { return rigidbody; } }
    public EnemyInfo EnemyInfo { get { return enemyInfo; } }
    public GameObject[] Players { get { return players; } }
    public GameObject Target { get { return target; } set { target = value; } }

    // Use this for initialization
    void Awake()
    {
        
        rigidbody = gameObject.GetComponent<Rigidbody>();
        players = GameObject.FindGameObjectsWithTag("Player");
        enemyInfo = GetComponent<EnemyInfo>();
        anim = GetComponent<Animation>();
        target = GameObject.FindGameObjectWithTag("Player");
        ChangeState("DefaultAI");
    }

    // Update is called once per frame
    void Update()
    {
        ai.UpdateBehaviour();

    }


    public void ChangeState(string state)
    {
        if (ai != null) ai.EndBehaviour();
        ai = gameObject.AddComponent(System.Type.GetType(state)) as BasicAI;
        ai.StartBehaviour();
    }
}

   
