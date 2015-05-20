using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	/*public GUISkin minimapSkin, selectedunitsSkin, keyboardcommandsWidth;
	private const int MINI_MAP_WIDTH = 200, MINI_MAP_HEIGHT = 200;
	private const int UNITS_INFO_WIDTH = 1523, UNITS_INFO_HEIGHT = 150;
	private const int KEY_BOARD_COMMANDS_WIDTH = 200, KEY_BOARD_COMMANDS_HEIGHT = 200;

	
	// Use this for initialization
	void Start () {
		//allows reference to Player and Player script
		//player = transform.root.GetComponent<Player> ();
	}
	
	// OnGUI is called once per frame
	// allows acces to GUI elements(showing a HUD)
	void OnGUI () {
			DrawMiniMap();
			DrawUnitsInfo();
			DrawKeyBoardCommands ();
	}
	
	private void DrawMiniMap(){
		GUI.skin = minimapSkin;
		//BeginGroup defines the rect area of screen that the HUD gets drawn at
		//This rect sits on the far right of screen
		//starting at the bottom of where the resource bar will sit
		GUI.BeginGroup (new Rect (0, Screen.height - MINI_MAP_HEIGHT, MINI_MAP_WIDTH, Screen.height));
		//Draws the coloured background inside of the group box
		//"" means we are not writing anything in the box(yet)
		// 0, 0 indicates the top left corner of the box not the top left corner of screen
		GUI.Box (new Rect (0, 0, MINI_MAP_WIDTH, Screen.height - MINI_MAP_HEIGHT), "Mini Map");
		//Always remember to end the group or it will crash
		GUI.EndGroup ();
	}
	
	private void DrawUnitsInfo(){
		GUI.skin = selectedunitsSkin;
		GUI.BeginGroup(new Rect(MINI_MAP_WIDTH, Screen.height - UNITS_INFO_HEIGHT, UNITS_INFO_WIDTH, Screen.height));
		GUI.Box(new Rect(0,0,UNITS_INFO_WIDTH, Screen.height - UNITS_INFO_HEIGHT),"Selected Unit Info");
		GUI.EndGroup();
	}

	private void DrawKeyBoardCommands(){
		GUI.skin = keyboardcommandsWidth;
		GUI.BeginGroup (new Rect (UNITS_INFO_WIDTH + MINI_MAP_WIDTH, Screen.height - KEY_BOARD_COMMANDS_HEIGHT, KEY_BOARD_COMMANDS_WIDTH, Screen.height));
		GUI.Box(new Rect(0,0,KEY_BOARD_COMMANDS_WIDTH,Screen.height - KEY_BOARD_COMMANDS_HEIGHT),"Key Commands");
		GUI.EndGroup();
	}*/





}