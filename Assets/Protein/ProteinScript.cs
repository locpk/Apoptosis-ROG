using UnityEngine;
using System.Collections;

public enum ProteinTypes 
{
	N_PROTEIN,C_PROTEIN,H_PROTEIN
}
public class ProteinScript : MonoBehaviour {

	public float m_value;
	public float M_value 
	{
		get {return m_value;}
		set {m_value = value;}
	}

	public ProteinTypes m_type;
	public ProteinTypes M_type 
	{
		get {return m_type;}
		set {m_type = value;}
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//testing
        //M_value -= Time.deltaTime;

        if (M_value / 100 <= 0.5f)
        {
            this.transform.localScale = new Vector3(0.5f, 0.0f, 0.5f);
        }

        if (M_value <= 0.0f)
        {
            
            PhotonNetwork.Destroy(gameObject);
        }
	}

    [RPC]
    void DepleteProtein(int value, PhotonMessageInfo info)
    {
        m_value -= value;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting == true)
        {
            stream.SendNext(m_value);
        }
        else
        {
            m_value = (float)stream.ReceiveNext();
        }
    }
   
}
