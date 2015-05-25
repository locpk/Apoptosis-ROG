using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackBehavior : MonoBehaviour
{
    float lasttimeAttack;

    void Awake()
    {
        lasttimeAttack  = Time.time;
    }

	public void Attack(GameObject _target)
	{
        if (Time.time - lasttimeAttack >= 1.0f)
        {
            lasttimeAttack = Time.time;
            _target.GetComponent<PhotonView>().RPC("ApplyDamage", _target.GetComponent<PhotonView>().owner, GetComponent<Cell>().m_attack);
            _target.transform.Find("DamagePrefab").GetComponent<Floating>().damageText.text = GetComponent<Cell>().m_attack.ToString();
            
        }
		
	}
}