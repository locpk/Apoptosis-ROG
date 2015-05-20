using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
	public Menu currentMenu;

	public void Start(){
		ShowMenu (currentMenu);
	}

	public void ShowMenu(Menu menu){
		if (currentMenu != null) {
			currentMenu.IsOpen = false;
		}

        if (menu != null)
        {
            currentMenu = menu;
            currentMenu.IsOpen = true;
        }
		

		
	}

	public void CloseMenu(Menu menu){
		currentMenu.IsOpen = false;
	}
}