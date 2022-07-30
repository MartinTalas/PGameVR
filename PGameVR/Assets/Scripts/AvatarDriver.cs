using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AvatarDriver : MonoBehaviour
{
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------[ VARIABLES ]------------------------------------------------------------------------

    //Json parser
    public JsonParser jsonParser;
    public Data user_data;

    //temporapy avatar list (for singleton AvatarManager)
    public GameObject[] casual_females = new GameObject[8];
    public GameObject[] casual_males = new GameObject[8];
    public GameObject[] elegant_females = new GameObject[8];
    public GameObject[] elegant_males = new GameObject[8];

    //avatar for first load!!! [Casual female]
    public GameObject firstLoader;

    AvatarManager avatar_manager;

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------[ INHERITED FROM MONOBEHAVIOUS ]---------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        //build singleton (sealed) AvatarManager class
        this.avatar_manager = AvatarManager.Instance;
        this.avatar_manager.buildLists(casual_females, casual_males, elegant_females, elegant_males);

        this.jsonParser = JsonParser.Instance;
        this.loadUserData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            GameObject.Find("InternetConnectionLostText").GetComponentInChildren<Text>().enabled = true;
        }
        else
        {
            GameObject.Find("InternetConnectionLostText").GetComponentInChildren<Text>().enabled = false; 
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------[ FUNCTIONS ]------------------------------------------------------------------------

    //click event for next avatar
    public void toRight()
    {
        firstLoader.SetActive(false);
        this.avatar_manager.currentAvatar(true);
        this.avatar_manager.setPreview();
    }

    //click event for previous avatar
    public void toLeft()
    {
        firstLoader.SetActive(false);
        this.avatar_manager.currentAvatar(false);
        this.avatar_manager.setPreview();
    }

    //set sex ("gender") property
    public void setSexProperty()
    {
        bool sex = this.avatar_manager.getProperties()[0], outfit = this.avatar_manager.getProperties()[1];
        firstLoader.SetActive(false);
        if (sex)
        {
            sex = false;
            GameObject.Find("SexButton").GetComponentInChildren<Text>().text = "Female";
        }
        else
        {
            sex = true;
            GameObject.Find("SexButton").GetComponentInChildren<Text>().text = "Male";
        }

        avatar_manager.setProperties(sex, outfit);
        avatar_manager.setPreview();
    }

    //set outfit property
    public void setOutfitProperty()
    {
        bool sex = avatar_manager.getProperties()[0], outfit = avatar_manager.getProperties()[1];
        firstLoader.SetActive(false);
       
        if (outfit)
        {
            outfit = false;
            GameObject.Find("OutfitButton").GetComponentInChildren<Text>().text = "Casual";
        }
        else
        {
            outfit = true;
            GameObject.Find("OutfitButton").GetComponentInChildren<Text>().text = "Elegant";
        }

        avatar_manager.setProperties(sex, outfit);
        avatar_manager.setPreview();
    }

    //Ok button: load the RoomScene!!!
    public void ok()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //DO NOTHING
        }
        else
        {
            //save data:
            user_data.avatar = avatar_manager.getJsonAvatarArray();
            jsonParser.toJson<Data>(user_data, "userdata");

            //go to RoomScene
            SceneManager.LoadScene("RoomScene");
        }
    }


    //load user data;
    private void loadUserData()
    {
        user_data = jsonParser.toObject<Data>("userdata");
        GameObject.Find("UsernameDataText").GetComponent<Text>().text = user_data.username;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
}
