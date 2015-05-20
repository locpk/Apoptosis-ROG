using UnityEngine;
using System.Collections;

public class Hot_Area : MonoBehaviour {

    public float timer = 2.0f;
    void OnTriggerStay(Collider other)
    {
       
        Cell cellinfo = other.GetComponent<Cell>();
        
        timer -= Time.fixedDeltaTime;
        if (cellinfo.gameObject.tag == "Cold Cell")
        {
            if (timer <= 0.0f)
            {
                timer = 2.0f;
                cellinfo.m_currentProteins -= 4;
            }
        }
        else if (cellinfo.gameObject.tag == "Neutral Cell")
        {
            if (timer <= 0.0f)
            {
                timer = 2.0f;
                cellinfo.m_currentProteins -= 2;
            }
        }
        
    }
}
