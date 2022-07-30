using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class SpawnPlayer : MonoBehaviourPunCallbacks
{
    private GameObject spawned_player; //player who spawned
    private Data data; //data for avatars and positioning
    private JsonParser jsonParser; //handle json
    private int position; //position (AS GAME QUEUE)
    private Vector3 real_position = new Vector3(0, 0, 0);
    private Vector3 real_rotation = new Vector3(0, 0, 0);

    private Vector3 control_position = new Vector3(0, 0, 0);
    private Vector3 control_rotation = new Vector3(0, 0, 0);

    private GameObject camera; //camera
    private string avatar = "PLAYER_";



    public override void OnJoinedRoom()
    {
        setData();
        
        base.OnJoinedRoom();
        Debug.Log("New player joined to the room!");
        
        spawned_player = PhotonNetwork.Instantiate(avatar, real_position, UnityEngine.Quaternion.Euler(real_rotation));
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("Player left!");

        PhotonNetwork.Destroy(spawned_player);
    }

    private void setData()
    {
        camera = GameObject.Find("CameraRig");

        jsonParser = JsonParser.Instance;
        data = jsonParser.toObject<Data>("userdata");

        position = data.player_position;

        switch(position)
        {
            case 1:
                real_rotation = new Vector3(0, 0, 0);
                real_position = new Vector3(0, 0, -2.01f);
                camera.transform.rotation = UnityEngine.Quaternion.Euler(real_rotation);
                camera.transform.position = new Vector3(0, 1.5f, -1.87f);

                control_position = new Vector3(0, 1.1f, -1.25f);
                control_rotation = new Vector3(50, 0, 0);
                GameObject.Find("StartCanvas").GetComponent<Canvas>().enabled = true;
                GameObject.Find("WaitCanvas").GetComponent<Canvas>().enabled = false;
                break;
            case 2:
                real_rotation = new Vector3(0, 270, 0);
                real_position = new Vector3(1.303125f, 0, -0.62f);
                camera.transform.rotation = UnityEngine.Quaternion.Euler(real_rotation);
                camera.transform.position = new Vector3(1.17f, 1.5f, -0.66f);

                control_position = new Vector3(0.56f, 1.1f, -0.62f);
                control_rotation = new Vector3(50, 270, 0);
                GameObject.Find("StartCanvas").GetComponent<Canvas>().enabled = false;
                GameObject.Find("WaitCanvas").GetComponent<Canvas>().enabled = true;
                break;
            case 3:
                real_rotation = new Vector3(0, 90, 0);
                real_position = new Vector3(-1.356875f, 0, -0.62f);
                camera.transform.rotation = UnityEngine.Quaternion.Euler(real_rotation);
                camera.transform.position = new Vector3(-1.18f, 1.5f, -0.66f);

                control_position = new Vector3(-0.56f, 1.1f, -0.62f);
                control_rotation = new Vector3(50, 90, 0);
                GameObject.Find("StartCanvas").GetComponent<Canvas>().enabled = false;
                GameObject.Find("WaitCanvas").GetComponent<Canvas>().enabled = true;
                break;
            case 4:
                real_rotation = new Vector3(0, 270, 0);
                real_position = new Vector3(1.303125f, 0, 1.25f);
                camera.transform.rotation = UnityEngine.Quaternion.Euler(real_rotation);
                camera.transform.position = new Vector3(1.17f, 1.5f, 1.24f);

                control_position = new Vector3(0.56f, 1.1f, 1.24f);
                control_rotation = new Vector3(50, 270, 0);
                GameObject.Find("StartCanvas").GetComponent<Canvas>().enabled = false;
                GameObject.Find("WaitCanvas").GetComponent<Canvas>().enabled = true;
                break;
            case 5:
                real_rotation = new Vector3(0, 90, 0);
                real_position = new Vector3(-1.356875f, 0, 1.25f);
                camera.transform.rotation = UnityEngine.Quaternion.Euler(real_rotation);
                camera.transform.position = new Vector3(-1.18f, 1.5f, 1.24f);

                control_position = new Vector3(-0.56f, 1.1f, 1.24f);
                control_rotation = new Vector3(50, 90, 0);
                GameObject.Find("StartCanvas").GetComponent<Canvas>().enabled = false;
                GameObject.Find("WaitCanvas").GetComponent<Canvas>().enabled = true;
                break;
            case 6:
                real_rotation = new Vector3(0, 270, 0);
                real_position = new Vector3(1.303125f,0, 1.25f); 
                camera.transform.rotation = UnityEngine.Quaternion.Euler(real_rotation);
                camera.transform.position = new Vector3(1.17f, 1.5f, 3.15f);

                control_position = new Vector3(0.56f, 1.1f, 3.15f);
                control_rotation = new Vector3(50, 270, 0);
                GameObject.Find("StartCanvas").GetComponent<Canvas>().enabled = false;
                GameObject.Find("WaitCanvas").GetComponent<Canvas>().enabled = true;
                break;
            case 7:
                real_rotation = new Vector3(0, 90, 0);
                real_position = new Vector3(-1.356875f, 0, 3.15f);
                camera.transform.rotation = UnityEngine.Quaternion.Euler(real_rotation);
                camera.transform.position = new Vector3(-1.18f, 1.5f, 3.15f);

                control_position = new Vector3(-0.56f, 1.1f, 3.15f);
                control_rotation = new Vector3(50, 90, 0);
                GameObject.Find("StartCanvas").GetComponent<Canvas>().enabled = false;
                GameObject.Find("WaitCanvas").GetComponent<Canvas>().enabled = true;
                break;
            case 8:
                real_rotation = new Vector3(0, 180, 0);
                real_position = new Vector3(0, 0, 3.15f);
                camera.transform.rotation = UnityEngine.Quaternion.Euler(real_rotation);
                camera.transform.position = new Vector3(0, 1.5f, 4.35f);

                control_position = new Vector3(0, 1.1f, 3.78f);
                control_rotation = new Vector3(50, 180, 0);
                GameObject.Find("StartCanvas").GetComponent<Canvas>().enabled = false;
                GameObject.Find("WaitCanvas").GetComponent<Canvas>().enabled = true;
                break;
            default:
                real_rotation = new Vector3(0, 0, 0);
                real_position = new Vector3(-1.356875f, -100, 1.25f);
                camera.transform.rotation = UnityEngine.Quaternion.Euler(real_rotation);
                break;
        }

        GameObject.Find("Control").transform.position = control_position;
        GameObject.Find("Control").transform.rotation = UnityEngine.Quaternion.Euler(control_rotation);

        string sex = "", outfit = "", index = "";

        if (data.avatar[0] == 0) //female
        {
            sex = "0";

            if (data.avatar[1] == 0) //casual
            {
                outfit = "0";
            }
            else //elegant
            {
                outfit = "1";
            }
        }
        else //male
        {
            sex = "1";

            if (data.avatar[1] == 0) //casual
            {
                outfit = "0";
            }
            else //elegant
            {
                outfit = "1";
            }
        }

        avatar += sex;
        avatar += outfit;
        avatar += data.avatar[2].ToString();

    }
}
