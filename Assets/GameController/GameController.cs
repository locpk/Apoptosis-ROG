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

        PhotonNetwork.room.maxPlayers = 2;

        if (PhotonNetwork.player.ID == 1)
        {
            spawnPoint = GameObject.Find("Player1SpawnPoint").transform.position;
                Camera.main.transform.position = new Vector3(spawnPoint.x, Camera.main.transform.position.y, spawnPoint.z);
        }
        else if (PhotonNetwork.player.ID == 2)
        {
            spawnPoint = GameObject.Find("Player2SpawnPoint").transform.position;
            Camera.main.transform.position = new Vector3(spawnPoint.x, Camera.main.transform.position.y, spawnPoint.z);
        }

        Camera.main.orthographicSize = 14;
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
