using UnityEngine;
using System.Collections;
using Global;

public class Split : Photon.MonoBehaviour {
	
	const float SPLIT_COOLDOWN = 5.0f;
	
	private bool m_move;		
	public bool M_move
	{ 
		get {return m_move;}
		set	{m_move = value; }
	}		
	
	private bool m_split;		
	public bool M_split
	{ 
		get {return m_split;}
		set	{m_split = value; }
	}		
	
	public float m_MoveTimer = 3.0f;     // Current amount of protiens for cold cell
	public float M_MoveTimer
	{ 
		get {return m_MoveTimer;}
		set	{m_MoveTimer = value; }
	}
	
	public float m_SplitTimer = SPLIT_COOLDOWN;     // Cooldown for Cancer Cell split
	public float M_SplitTimer
	{ 
		get {return m_SplitTimer;}
		set	{m_SplitTimer = value; }
	}	
	
	
	public GameObject originalCell;
	
	/*** Game Engine methods, all can be overridden by subclass ***/
	
	public void Awake() {
		
	}
	
	public void Start () {
		originalCell = gameObject;
	}
	
	public void Update () {
		
		/* split based on condition
		 * if(m_split)
			Split();
			*/
		
		//split based on a timer
		if(m_SplitTimer <= 0.0f)
		{
			m_SplitTimer = SPLIT_COOLDOWN;
            if (GetComponent<Cell>().m_currentProteins % 2 == 0 && GetComponent<Cell>().m_currentProteins > 0 )
			{
				Divide();
			}
		}
		else
		{
			m_SplitTimer -= Time.deltaTime;
		} 
		if(M_move)
		{
			SeprateCell();
		}
	}
	
	public void OnGUI() {
		
	}
	
	public void ToggleSplit()
	{
		if(M_split)
			M_split = false;
		else
			M_split = true;
	}	
	
	public void Divide()
	{

        if (Global.GlobalVariables.cap <= Global.GlobalVariables.MAX_CAP)
        {
            //half the proteins for new cells
            GetComponent<Cell>().m_currentProteins /= 2;
            Vector3 newposition = this.transform.position;
            newposition += Quaternion.Euler(0, 0, Random.Range(0, 360)) * new Vector3(GetComponent<SphereCollider>().radius, 0, 0);
            GameObject newCell1 = PhotonNetwork.Instantiate(gameObject.name, newposition, Quaternion.Euler(90, 0, 0), 0);
            newCell1.name = gameObject.name;
            newCell1.GetComponent<Cell>().m_currentProteins = GetComponent<Cell>().m_currentProteins;
            // move the second new cell out of the first one
            newCell1.GetComponent<Split>().M_move = true;

            //add 1 cap
            Global.GlobalVariables.cap++; 
        }
	}
	
	private void SeprateCell()
	{
		Vector3 dir = new Vector3(GetComponent<SphereCollider>().radius,0,0);
		dir = Quaternion.Euler (0, 0, Random.Range(0, 360)) * dir;
		Vector3 position = transform.position + dir;
		transform.Translate(position*Time.deltaTime);
		M_MoveTimer -= Time.deltaTime;
		if(M_MoveTimer <= 0) M_move = false;
	}
}
