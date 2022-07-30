using Firebase.Database;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------[ VARIABLESS ]------------------------------------------------------------------------

    private Data data;
    private JsonParser jsonParser;
    private Rating rating;
    private DataBaseManager dataBaseManager;
    private FirebaseDatabase db;
    private string current_speaker = "ERR";
    private int current_speaker_points = 0;
    private int my_point = 0;
    private bool is_voted = false;
    private bool once = true;
    private List<string> player_list;
    private Dictionary<int, string> player_dictionary;

    public Canvas rate_canvas;
    public Canvas menu_canvas;
    public Canvas speaker_canvas;
    public Canvas start_canvas;
    public Canvas wait_canvas; 
    public Canvas finished_canvas;

    public Text topic;
    public Text current_topic;
    public Text username;
    public Text current_username;
    public Text rate_current_speaker;

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------[ INHERITED FROM MONOBEHAVIOUS ]---------------------------------------------------------------

    void Awake()
    {
        dataBaseManager = DataBaseManager.Instance;

    }

    // Start is called before the first frame update
    void Start()
    {
        canvasLoader();
        rating = Rating.Instance;

        jsonParser = JsonParser.Instance;

        data = jsonParser.toObject<Data>("userdata");
        player_list = new List<string>();
        player_dictionary = new Dictionary<int, string>();

        FirebaseDatabase.DefaultInstance.GetReference("current_speaker").Child(data.game_id).ValueChanged += currentSpeakerChanged;
        FirebaseDatabase.DefaultInstance.GetReference("games").Child(data.game_id).ChildAdded += playerListChanged;
        FirebaseDatabase.DefaultInstance.GetReference("games").Child(data.game_id).ValueChanged += playerListValueChanged;

        InvokeRepeating("getPlayerList", 0.5f, 1.0f); //getPlayerList();

        getTopic();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------[ RATING FUNCTIONS ]---------------------------------------------------------------------

    public void rateOne()
    {
        rating.rateOneR();
    }
    public void rateTwo()
    {
        rating.rateTwoR();
    }
    public void rateThree()
    {
        rating.rateThreeR();
    }
    public void rateFour()
    {
        rating.rateFourR();
    }
    public void rateFive()
    {
        rating.rateFiveR();
    }

    public void hoverOne()
    {
        rating.hoverOneR();
    }
    public void hoverTwo()
    {
        rating.hoverTwoR();
    }
    public void hoverThree()
    {
        rating.hoverThreeR();
    }
    public void hoverFour()
    {
        rating.hoverFourR();
    }
    public void hoverFive()
    {
        rating.hoverFiveR();
    }

    public void hoverExit()
    {
        rating.hoverExitR();
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------[ DATABASE MANAGING ]--------------------------------------------------------------------

    private async void getPlayerList()
    {
        string debug = "";
        string str = "";
        try
        {
            db = dataBaseManager.getConnection();
            await db.GetReference("games").Child(data.game_id)
                                           .GetValueAsync()
                                           .ContinueWith(task =>
                                           {
                                               if (task.IsCompleted)
                                               {
                                                   Debug.Log("task.IsCompleted: Succeeded [get playerList]");
                                                   DataSnapshot data_snapshot = task.Result;

                                                   if (data_snapshot.Exists)
                                                   {
                                                       Debug.Log(task.Result.ToString());
                                                       var dts = data_snapshot.Value as Dictionary<string, object>;
                                                       Dictionary<int, string> temp = new Dictionary<int, string>();
                                                       player_list.Clear();
                                                       player_dictionary.Clear();
                                                       temp.Clear();

                                                       foreach (var item in dts)
                                                       {
                                                           if (!player_list.Contains(item.Key))
                                                           {
                                                               player_list.Add(item.Key);
                                                               temp.Add(Int32.Parse(item.Value.ToString()), item.Key);
                                                               str += " " + item.Key;
                                                           }
                                                       }

                                                       foreach(var item in temp.OrderBy(order => order.Key))
                                                       {
                                                           player_dictionary.Add(item.Key, item.Value);
                                                       }

                                                       foreach (var item in player_dictionary)
                                                       {
                                                           Debug.Log("DICTIONARY: " + item.Key + " " + item.Value);
                                                       }

                                                       Debug.Log("Players:" + str);

                                                   }
                                                   else
                                                   {
                                                       Debug.Log(task.Result.ToString() + " ERROR");
                                                   }
                                               }
                                               else
                                               {
                                                   Debug.LogError("task.IsCompleted: Failed [get playerlist]");
                                               }

                                           });
        }
        catch (Exception ex)
        {
            Debug.Log("Exception: " + ex);
            debug = "\nplayerlist " + ex.Message;
        }
        GameObject.Find("TESTTEXT").GetComponent<Text>().text += debug;
    }

    private async void confirmation()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
        }
        else
        {
            db = dataBaseManager.getConnection();
            try
            {
                int pnts = 0;
                await db.GetReference("points")
                        .Child(current_speaker)
                        .GetValueAsync()
                        .ContinueWith(task => 
                        {
                            if (task.IsCompleted)
                            {
                                Debug.Log("task.IsCompleted: Succeeded [get playerdata]");
                                DataSnapshot data_snapshot = task.Result;

                                if (data_snapshot.Exists)
                                {
                                    Debug.Log("PLAYER POINT:" + data_snapshot.Value);
                                    pnts = int.Parse(data_snapshot.Value.ToString());
                                }
                            }

                            pnts += rating.getVoteRate();

                        }).ContinueWith(async task => { await db.GetReference("points").Child(current_speaker).SetValueAsync(pnts); });
                
            }
            catch (Exception ex)
            {
                Debug.Log("Exception: " + ex);
            }

        }
    }

    private async void setCurrentSpeaker()
    {
        Debug.Log("SET CURRENT");
        string debug = "";
        string next_speaker = "";

        for (int i = 0; i < player_dictionary.Count; i++)
        {
            if (current_speaker == player_dictionary.ElementAt(i).Value)
            {
                if (i != player_dictionary.Count - 1)
                {
                    next_speaker = player_dictionary.ElementAt(i + 1).Value;
                }
                else
                {
                    next_speaker = "END";
                }
                break;
            }
            else if (next_speaker == player_dictionary.ElementAt(player_dictionary.Count - 1).Value)
            {
                next_speaker = "END";
            }
            else
            {
                next_speaker = "ERR";
            }
            Debug.Log("NEXT: " + next_speaker);
        }

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
        }
        else
        {
            db = dataBaseManager.getConnection();
            try
            {
                await db.GetReference("current_speaker").Child(data.game_id).SetValueAsync(next_speaker);
                current_speaker = next_speaker;
            }
            catch (Exception ex)
            {
                Debug.Log("Exception: " + ex);
                debug = "\n scsp" + ex.Message;
            }

        }
        GameObject.Find("TESTTEXT").GetComponent<Text>().text += debug;

        Debug.Log("Current: " + current_speaker);
        if(current_speaker == "END")
        {
            showMyPoints();
        }
    }

    private async void setCurrentSpeaker(string speaker)
    {
        string debug = "";
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
        }
        else
        {
            db = dataBaseManager.getConnection();
            try
            {
                await db.GetReference("current_speaker").Child(data.game_id).SetValueAsync(speaker);
                current_speaker = speaker;
            }
            catch (Exception ex)
            {
                Debug.Log("Exception: " + ex);
                debug = "\n scsp" + ex.Message;
            }

        }
        GameObject.Find("TESTTEXT").GetComponent<Text>().text += debug;
    }

    private async void getCurrentSpeaker()
    {
        string debug = "";
        try
        {
            db = dataBaseManager.getConnection();
            await db.GetReference("current_speaker").Child(data.game_id).GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("task.IsCompleted: Succeeded [CURRENT SPEAKER]");
                    DataSnapshot data_snapshot = task.Result;

                    if (data_snapshot.Exists)
                    {
                        Debug.Log("GETCURRENT" + data_snapshot.Value.ToString());
                        current_speaker = data_snapshot.Value.ToString();
                        debug = "get current: " + current_speaker + ", ";
                        if(current_speaker == "")
                        {
                            current_speaker = "ERR";
                        }

                    }


                }
            
            });
            if (current_speaker == data.username)
            {
                Debug.Log("change IF:" + current_speaker + ":");
                rate_canvas.enabled = false;
                menu_canvas.enabled = false;
                speaker_canvas.enabled = true;
                start_canvas.enabled = false;
                wait_canvas.enabled = false;
                finished_canvas.enabled = false;

            }
            else if (current_speaker == "")
            {
                Debug.Log("change ELSE IF:" + current_speaker + ":");

                rate_canvas.enabled = false;
                menu_canvas.enabled = false;
                speaker_canvas.enabled = false;
                start_canvas.enabled = false;
                wait_canvas.enabled = false;
                finished_canvas.enabled = false;
            }
            else if (current_speaker == "ERR")
            {
                Debug.Log("change ELSE IF:" + current_speaker + ":");

                rate_canvas.enabled = false;
                menu_canvas.enabled = false;
                speaker_canvas.enabled = false;
                start_canvas.enabled = false;
                wait_canvas.enabled = false;
                finished_canvas.enabled = false;
            }
            else if (current_speaker == "END")
            {
                Debug.Log("change ELSE IF:" + current_speaker + ":");

                rate_canvas.enabled = false;
                menu_canvas.enabled = false;
                speaker_canvas.enabled = false;
                start_canvas.enabled = false;
                wait_canvas.enabled = false;
                finished_canvas.enabled = true;
                showMyPoints();
            }
            else
            {
                Debug.Log("change ELSE speaker:" + current_speaker + ":");
                rate_canvas.enabled = true;
                menu_canvas.enabled = false;
                speaker_canvas.enabled = false;
                start_canvas.enabled = false;
                wait_canvas.enabled = false;
                finished_canvas.enabled = false;
            }

            if (username != null)
            {
                username.text = data.username;
            }

            if (current_username != null)
            {
                current_username.text = data.username;
            }

            if (topic != null)
            {
                topic.text = data.topic;
            }

            if (current_topic != null)
            {
                current_topic.text = data.topic;
            }

            if (rate_current_speaker != null)
            {
                rate_current_speaker.text = current_speaker;
            }

            rating.resetButtons();
        }
        catch (Exception ex)
        {
            debug = "\ngetCurrentSpeaker: " + ex.Message;
        }

        GameObject.Find("TESTTEXT").GetComponent<Text>().text += debug;
    }

    private void currentSpeakerChanged(object sender, ValueChangedEventArgs args)
    {
        JsonParser jp = JsonParser.Instance;
        Data dt = jp.toObject<Data>("userdata");

        if (username != null)
        {
            username.text = dt.username;
        }

        if (current_username != null)
        {
            current_username.text = dt.username;
        }

        if (topic != null)
        {
            topic.text = dt.topic;
        }

        if (current_topic != null)
        {
            current_topic.text = dt.topic;
        }

        getCurrentSpeaker();

        is_voted = false;

        if (once)
        {
            setCurrentSpeaker();
            once = false;
        }

    }

    private void playerListChanged(object sender, ChildChangedEventArgs args)
    {
        getPlayerList(); 
    }

    private void playerListValueChanged(object sender, ValueChangedEventArgs args)
    {
        getPlayerList();
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void confirm()
    {
        Debug.Log("CONFIRM");
        getPlayerList();
        if (!is_voted)
        {
            Debug.Log("ISVOTED TRUE");
            confirmation();
            is_voted = true;

            Debug.Log("CONFIRMED");
        }
    }

    public void next()
    {
        setCurrentSpeaker();
        canvasChanger();
    }

    public void menu()
    {
        rate_canvas.enabled = false;
        menu_canvas.enabled = true;
        speaker_canvas.enabled = false;
    }

    public void back()
    {
        if (current_speaker == data.username)
        {
            rate_canvas.enabled = false;
            menu_canvas.enabled = false;
            speaker_canvas.enabled = true;
        }
        else
        {
            rate_canvas.enabled = true;
            menu_canvas.enabled = false;
            speaker_canvas.enabled = false;
        }
    }

    public void quit()
    {
        Application.Quit();
    }

    private void canvasLoader()
    {
        GameObject temp;

        //CANVAS [CURRENT]
        {
            temp = GameObject.Find("CurrentCanvas");
            if (temp != null)
            {
                speaker_canvas = temp.GetComponent<Canvas>();
                if (speaker_canvas == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        //CANVAS [RATE]
        {
            temp = GameObject.Find("RateCanvas");
            if (temp != null)
            {
                rate_canvas = temp.GetComponent<Canvas>();
                if (rate_canvas == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        //CANVAS [MENU]
        {
            temp = GameObject.Find("GameMenuCanvas");
            if (temp != null)
            {
                menu_canvas = temp.GetComponent<Canvas>();
                if (menu_canvas == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        //CANVAS [START]
        {
            temp = GameObject.Find("StartCanvas");
            if (temp != null)
            {
                start_canvas = temp.GetComponent<Canvas>();
                if (start_canvas == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        //CANVAS [WAIT]
        {
            temp = GameObject.Find("WaitCanvas");
            if (temp != null)
            {
                wait_canvas = temp.GetComponent<Canvas>();
                if (wait_canvas == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        //CANVAS [FINISHED]
        {
            temp = GameObject.Find("GameOverCanvas");
            if (temp != null)
            {
                finished_canvas = temp.GetComponent<Canvas>();
                if (finished_canvas == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        JsonParser jp = JsonParser.Instance;
        Data dt = jp.toObject<Data>("userdata");
        //TEXT [TOPIC]
        {
            temp = GameObject.Find("TopicText");
            if (temp != null)
            {
                topic = temp.GetComponent<Text>();
                topic.text = dt.topic;

                if (topic == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        //TEXT [CURRENT TOPIC]
        {
            temp = GameObject.Find("CurrentTopicText");
            if (temp != null)
            {
                current_topic = temp.GetComponent<Text>();
                current_topic.text = dt.topic;

                if (current_topic == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        //TEXT [USERNAME]
        {
            temp = GameObject.Find("ControlUsernameText");
            if (temp != null)
            {
                username = temp.GetComponent<Text>();
                username.text = dt.username;

                if (username == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        //TEXT [CURRENT USERNAME]
        {
            temp = GameObject.Find("CurrentControlUsernameText");
            if (temp != null)
            {
                current_username = temp.GetComponent<Text>();
                current_username.text = dt.username;

                if (current_username == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        //TEXT [RATE CURRENT SPEAKER]
        {
            temp = GameObject.Find("RateCurrentSpeakerText");
            if (temp != null)
            {
                rate_current_speaker = temp.GetComponent<Text>();
                if (rate_current_speaker == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        wait_canvas.enabled = false;
        start_canvas.enabled = false; // true
        speaker_canvas.enabled = false;
        rate_canvas.enabled = false;
        menu_canvas.enabled = false;
        finished_canvas.enabled = false;
    }

    private void canvasChanger()
    {
        if (current_speaker == data.username)
        {
            rate_canvas.enabled = false;
            menu_canvas.enabled = false;
            speaker_canvas.enabled = true;
            start_canvas.enabled = false;
            wait_canvas.enabled = false;
            finished_canvas.enabled = false;
        }
        else if (current_speaker == "ERR")
        {
            Debug.Log("SAVE CANVAS ELSE IF (END):" + current_speaker + ":");

            rate_canvas.enabled = false;
            menu_canvas.enabled = false;
            speaker_canvas.enabled = false;
            start_canvas.enabled = false;
            wait_canvas.enabled = false;
            finished_canvas.enabled = true;
        }
        else
        {
            rate_canvas.enabled = true;
            menu_canvas.enabled = false;
            speaker_canvas.enabled = false;
            start_canvas.enabled = false;
            wait_canvas.enabled = false;
            finished_canvas.enabled = false;
        }
        Debug.Log("Current speaker: " + current_speaker + "; Me: " + data.username);
    }

    public void startSet()
    {
        current_speaker = player_dictionary.ElementAt(0).Value;
        setCurrentSpeaker(current_speaker);
        canvasChanger();
    }


    //multiple player points [NOT IMPLEMENTED]
    private void setPoints()
    {
        /*
        foreach (var element in player_dictionary)
        {
          
        }
        */
    }

    private async void showMyPoints()
    {
        int pnts = 0;
        try
        {
            await db.GetReference("points")
                           .Child(data.username)
                           .GetValueAsync()
                           .ContinueWith(task =>
                           {
                               if (task.IsCompleted)
                               {
                                   Debug.Log("task.IsCompleted: Succeeded [get playerdata]");
                                   DataSnapshot data_snapshot = task.Result;

                                   if (data_snapshot.Exists)
                                   {
                                       Debug.Log("PLAYER POINT:" + data_snapshot.Value);
                                       pnts = int.Parse(data_snapshot.Value.ToString());
                                   }
                               }
                           });
        }
        catch (Exception ex)
        {
            Debug.Log("ERROR OCCURED IN GET POINTS -> showMyPoints(): " + ex);
        }
        GameObject.Find("playerDataText1").GetComponent<Text>().text = data.username;
        GameObject.Find("playerPoints1").GetComponent<Text>().text = pnts.ToString();
    }

    private async void getTopic()
    {
        try
        {
            db = dataBaseManager.getConnection();
            await db.GetReference("VR_topics")
                    .Child(data.game_id)
                    .GetValueAsync()
                    .ContinueWith(task =>
                    {
                        if (task.IsCompleted)
                        {
                            DataSnapshot data_snapshot = task.Result;

                            if (data_snapshot.Exists)
                            {
                                Debug.Log("PLAYER POINT:" + data_snapshot.Value);
                                data.topic = data_snapshot.Value.ToString();
                            }
                        }
                    });
        }
        catch(Exception ex)
        {
            Debug.Log("GETTOPIC ERROR: " + ex);
        }
    }
}

