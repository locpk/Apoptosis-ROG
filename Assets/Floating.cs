using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Floating : MonoBehaviour {
    float time_;
    float time_to_fade = 3.0f;
    public GUIText damageText;
	// Use this for initialization
	void Start () {
       
        damageText = GetComponent<GUIText>();
        damageText.color = Color.red;
        time_ = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        
        transform.Translate(new Vector3(0.0f, 0.001f, 0.0f));
        Color fadingColor = damageText.color;
        fadingColor.a = Mathf.Cos((Time.time - time_) * ((Mathf.PI / 2) / time_to_fade));
        damageText.color = fadingColor;
        Destroy(gameObject, time_to_fade);
	}
}
