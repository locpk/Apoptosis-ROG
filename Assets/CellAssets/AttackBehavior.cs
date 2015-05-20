using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackBehavior : MonoBehaviour
{
	public void Attack(GameObject _target)
	{
		_target.GetComponent<PhotonView> ().RPC ("ApplyDamage", _target.GetComponent<PhotonView> ().owner, GetComponent<Cell>().m_attack);
	}
}