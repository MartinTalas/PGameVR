using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    private Data data; //data for avatars and positioning
    private JsonParser jsonParser; //handle json

    void Start()
    {
        ConnectToServer();
    }

    void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try to connect to server...");
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to Lobby.");

    }

    public override void OnConnectedToMaster()
    {
        setData();
        try
        {
            base.OnConnectedToMaster();

            RoomOptions room_options = new RoomOptions();
            room_options.MaxPlayers = 8;
            room_options.IsVisible = true;
            room_options.IsOpen = true;

            PhotonNetwork.JoinOrCreateRoom(data.game_id.ToUpper(), room_options, TypedLobby.Default);
            Debug.Log("Connected to server.");
        }
        catch(Exception ex)
        {
            
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined to the room.");

        //data.player_position = PhotonNetwork.CountOfPlayers; //try other option: from db
        //jsonParser.toJson<Data>(data, "userdata");
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
    }

    private void setData()
    {
        jsonParser = JsonParser.Instance;
        data = jsonParser.toObject<Data>("userdata");
    }
}
