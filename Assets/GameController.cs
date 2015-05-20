using UnityEngine;
using System.Collections;

public class GameController : Photon.MonoBehaviour
{


    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("DANK MEMES");
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        Vector3 spawnPoint = Vector3.zero;
        spawnPoint.x += 100 * PhotonNetwork.player.ID;
        GameObject ColdCell = PhotonNetwork.Instantiate("Cold Cell", spawnPoint, Quaternion.Euler(90, 0, 0), 0);
        ColdCell.name = "Cold Cell";
        spawnPoint.z += 5;
        GameObject HeatCell = PhotonNetwork.Instantiate("Heat Cell", spawnPoint, Quaternion.Euler(90, 0, 0), 0);
        HeatCell.name = "Heat Cell";

        
    }

   void OnLeftRoom()
    {
        PhotonNetwork.LeaveLobby();
    }
}
