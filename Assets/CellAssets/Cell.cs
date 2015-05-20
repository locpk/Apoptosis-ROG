using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Cell : MonoBehaviour
{

	public int m_attack;				// Attacls for cold cell
	public int m_Maxproteins;			// Max amount of protein you can have  for cold cell
	
	public int m_currentProteins;       // Current amount of protiens for cold cell
	public int m_defense;				// Defense for cold cell
	public int m_speed;					// Speed for cold cell
	public float lossOfProteinTimer;    // TImer for the loss of proteins per
	public float attackRange =1.5f;
    
	NavMeshAgent navAgent;
	NavMeshObstacle navObstacle;
	bool alive;
	
	AttackBehavior atkBehavior;
	
	GameObject target = null;

    
	//PUBLIC FUNCTIONS------------------------------------------------
	public void SetDestination()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (target == null ? Input.mousePosition : Camera.main.WorldToScreenPoint(target.transform.position));
		
		if (Physics.Raycast (ray, out hit, 100))
		{
			navObstacle.enabled = false;
			navAgent.enabled = true;
			navAgent.SetDestination (hit.point);
            
		}		
	}

	public void SetDestination(Vector3 pos)
	{
		navAgent.SetDestination (pos);
	}
	
	public void SetTarget(GameObject _target)
	{
		navObstacle.enabled = false;
		navAgent.enabled = true;
		if (_target != null) {
			
			target = _target;
		}
	}
	//END OF PUBLIC FUNCTIONS-----------------------------------------
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting == true) {
			stream.SendNext(m_currentProteins);
		} else {
			m_currentProteins = (int)stream.ReceiveNext();
		}
	}
	
	[RPC]
	void ApplyDamage(int damage, PhotonMessageInfo info) {
		m_currentProteins -= damage;
	}
	
	// Use this for initialization
	void Start () 
	{
            
        PhotonView me = GetComponent<PhotonView>();
        if (!me.isMine)
        {
            SpriteRenderer ss = GetComponent<SpriteRenderer>();
            ss.color = Color.black;
        }
		navAgent = GetComponent<NavMeshAgent> ();
		navObstacle = GetComponent<NavMeshObstacle> ();
		alive = true;
		navAgent.updateRotation = false;
		atkBehavior = GetComponent<AttackBehavior> ();
		PhotonNetwork.SetSendingEnabled (1, false);
		PhotonNetwork.SetReceivingEnabled (1, false);

       
	}
	
	// Update is called once per frame
	void Update () 
	{ 
		if (alive) 
		{
			if (target != null)
			{
				if (Vector3.Distance (transform.position, target.transform.position) <= attackRange)
				{
					navAgent.enabled = false;
					navObstacle.enabled = true;
					if (target.tag == "Unit") 
					{
						atkBehavior.Attack(target);
					}
					else if (target.tag == "Protein") 
					{
						//Consume();
					}

				}
				else {
					SetDestination();
				}
			}
			else if ((navAgent.destination - transform.position).sqrMagnitude < Mathf.Pow(navAgent.stoppingDistance, 2)) {
				navAgent.enabled = false;
				navObstacle.enabled = true;
			}
			
			lossOfProteinTimer += 1 * Time.deltaTime;
			if (lossOfProteinTimer >= 20)
			{
				m_currentProteins += 1;
				lossOfProteinTimer = 0;
			}
			if (m_currentProteins <= 0)
			{
				alive = false;
				PhotonNetwork.Destroy(gameObject);
			} 
		}
	}
	
	void OnGUI()
	{
		//GUILayout.Label (lossOfProteinTimer.ToString());
		//GUILayout.Label (m_currentProteins.ToString());
	}
	
	void Attack()
	{
		GameObject closest = null;
		foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
		{
			if(closest == null || Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, closest.transform.position))
			{
				closest = enemy;
			}
		}
		SetTarget (closest);
	}
}
