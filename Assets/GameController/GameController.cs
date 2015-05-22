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

        Camera.main.orthographicSize = 30;
        GameObject ColdCell = PhotonNetwork.Instantiate("Cold Cell", spawnPoint, Quaternion.Euler(90, 0, 0), 0);
        ColdCell.name = "Cold Cell";
        spawnPoint.z += 2;
        GameObject HeatCell = PhotonNetwork.Instantiate("Heat Cell", spawnPoint, Quaternion.Euler(90, 0, 0), 0);
        HeatCell.name = "Heat Cell";
        spawnPoint.z += 2;
        GameObject NeutralCell = PhotonNetwork.Instantiate("Neutral Cell", spawnPoint, Quaternion.Euler(90, 0, 0), 0);
        NeutralCell.name = "Neutral Cell";

    }

    void OnLeftRoom()
    {
        PhotonNetwork.LeaveLobby();
        
    }
    void OnCreatedRoom()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector2 spawnV2 = Random.insideUnitCircle * 30;
            Vector3 spawnV3 = new Vector3(spawnV2.x, 0.0f, spawnV2.y);
            PhotonNetwork.Instantiate("Cold Protein", spawnV3, Quaternion.Euler(90, 0, 0), 0);
        }
        for (int i = 0; i < 10; i++)
        {
            Vector2 spawnV2 = Random.insideUnitCircle * 30;
            Vector3 spawnV3 = new Vector3(spawnV2.x, 0.0f, spawnV2.y);
            PhotonNetwork.Instantiate("Heat Protein", spawnV3, Quaternion.Euler(90, 0, 0), 0);
        }
        for (int i = 0; i < 10; i++)
        {
            Vector2 spawnV2 = Random.insideUnitCircle * 30;
            Vector3 spawnV3 = new Vector3(spawnV2.x, 0.0f, spawnV2.y);
            PhotonNetwork.Instantiate("Neutral Protein", spawnV3, Quaternion.Euler(90, 0, 0), 0);
        }
    }
}
