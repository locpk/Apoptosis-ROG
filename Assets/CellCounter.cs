using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
public class CellCounter : MonoBehaviour {
    System.Collections.Generic.List<GameObject> units;
	// Use this for initialization
	bool IsNotNCell(GameObject _object)
    {
        if (_object.name == "Neutral Cell" && _object.GetComponent<PhotonView>().isMine)
        {
            return false;
        }
        else
            return true;
    }

    bool IsNotCCell(GameObject _object)
    {
        if (_object.name == "Cold Cell" && _object.GetComponent<PhotonView>().isMine)
        {
            return false;
        }
        else
            return true;
    }

    bool IsNotHCell(GameObject _object)
    {
        if (_object.name == "Heat Cell" && _object.GetComponent<PhotonView>().isMine)
        {
            return false;
        }
        else
            return true;
    }

	// Update is called once per frame
	void Update () {
        //count Neutral Cells
        units = GameObject.FindGameObjectsWithTag("Unit").ToList();
        units.RemoveAll(IsNotNCell);
        GameObject.Find("NCount").GetComponent<Text>().text = units.Count.ToString();
        units.Clear();
        //count Cold Cells
        units = GameObject.FindGameObjectsWithTag("Unit").ToList();
        units.RemoveAll(IsNotCCell);
        GameObject.Find("CCount").GetComponent<Text>().text = units.Count.ToString();
        units.Clear();
        //count Heat Cells
        units = GameObject.FindGameObjectsWithTag("Unit").ToList();
        units.RemoveAll(IsNotHCell);
        GameObject.Find("HCount").GetComponent<Text>().text = units.Count.ToString();
        units.Clear();
	}
}
