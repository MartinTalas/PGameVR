using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using Firebase.Database;

enum FIELDENUM //enum for the textfields [INPUTS]
{
    LOGIN_USERNAME,
    LOGIN_PASSWORD,
    SIGN_UP_USERNAME,
    SIGN_UP_PASSWORD,
    SIGN_UP_PASSWORD_AGAIN,
    GAME_CODE,
    TOPIC,
    NON
}

public class UIController : MonoBehaviour
{
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------[ VARIABLESS ]------------------------------------------------------------------------

    public Canvas SwitchCanvas;
    public Canvas SignUpCanvas;
    public Canvas LoginCanvas;
    public Canvas GameChooserCanvas;
    public Canvas KeyboardCanvas;

    public VRInputField login_username;
    public VRInputField login_password;
    public VRInputField sign_up_username;
    public VRInputField sign_up_password;
    public VRInputField sign_up_again_password; 
    public VRInputField game_id_input; //GameIDInput
    public VRInputField topic_input;

    public Text info_text;
    public Text internet_connection_lost_text;
    public Text game_id_text;
    public Text game_id_content_text;

    public VRInputField keyboard_input_field;
    private static FIELDENUM field_enum;

    private DataBaseManager dataBaseManager;
    private FirebaseDatabase db;

    private JsonParser jsonParser;

    //TESTOBJECTS
    public Text TESTTEXT;

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------[ INHERITED FROM MONOBEHAVIOUS ]---------------------------------------------------------------

    // Awake is called first
    void Awake()
    {
        //dataBaseManager = DataBaseManager.Instance; // EZ OKOZZA A HIBÁT!!!!!!!!!
        jsonParser = JsonParser.Instance;
        
        //Set json to default
        Data default_data = new Data();
        jsonParser.toJson<Data>(default_data, "userdata");
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObjectLoader();

        dataBaseManager = DataBaseManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            internet_connection_lost_text.enabled = true;
        }
        else
        {
            internet_connection_lost_text.enabled = false; 
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------[ LOADERS AND OTHERS ]--------------------------------------------------------------------

    private void gameObjectLoader()
    {
        GameObject temp;

        //CANVAS [SIGNSWITCH]
        {
            temp = GameObject.Find("SignSwitchCanvas");
            if (temp != null)
            {
                SwitchCanvas = temp.GetComponent<Canvas>();
                if (SwitchCanvas == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        //CANVAS [SIGNUP]
        {
            temp = GameObject.Find("SignUpCanvas");
            if (temp != null)
            {
                SignUpCanvas = temp.GetComponent<Canvas>();
                if (SignUpCanvas == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
                SignUpCanvas.enabled = false;
            }
            temp = null;
        }

        //CANVAS [LOGIN]
        {
            temp = GameObject.Find("LoginCanvas");

            if (temp != null)
            {
                LoginCanvas = temp.GetComponent<Canvas>();
                if (LoginCanvas == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
                LoginCanvas.enabled = false;
            }
            temp = null; 
        }

        //CANVAS [GAMECHOOSER]
        {
            temp = GameObject.Find("GameChooserCanvas");
            if (temp != null)
            {
                GameChooserCanvas = temp.GetComponent<Canvas>();
                if (SwitchCanvas == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
                GameChooserCanvas.enabled = false; 
            }
            temp = null;
        }

        //CANVAS [KEYBOARD]
        {
            temp = GameObject.Find("KeyboardCanvas");
            if (temp != null)
            {
                KeyboardCanvas = temp.GetComponent<Canvas>();
                if (KeyboardCanvas == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
                KeyboardCanvas.enabled = false;
            }
            temp = null;
        }

        //INPUT [LOGIN.USERNAME]
        {
            temp = GameObject.Find("UsernameInput");
            if (temp != null)
            {
                login_username = temp.GetComponent<VRInputField>();
                if (login_username == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        //INPUT [LOGIN.PASSWORD]
        {
            temp = GameObject.Find("PasswordInput");
            if (temp != null)
            {
                login_password = temp.GetComponent<VRInputField>();
                if (login_password == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        //INPUT [SIGNUP.USERNAME]
        {
            temp = GameObject.Find("SignUpUsernameInput");
            if (temp != null)
            {
                sign_up_username = temp.GetComponent<VRInputField>();
                if (sign_up_username == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        //INPUT [SIGNUP.PASSWORD]
        {
            temp = GameObject.Find("SignUpPasswordInput");
            if (temp != null)
            {
                sign_up_password = temp.GetComponent<VRInputField>();
                if (sign_up_password == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        //INPUT [SIGNUP.PASSWORD.AGAIN]
        {
            temp = GameObject.Find("SignUpPasswordAgainInput");
            if (temp != null)
            {
                sign_up_again_password = temp.GetComponent<VRInputField>();
                if (sign_up_again_password == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        //INPUT [KEYBOARD]
        {
            temp = GameObject.Find("KeyboardInput");
            if (temp != null)
            {
                keyboard_input_field = temp.GetComponent<VRInputField>();
                if (keyboard_input_field == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        //INPUT [GAMEID]
        {
            temp = GameObject.Find("GameIDInput");
            if (temp != null)
            {
                game_id_input = temp.GetComponent<VRInputField>();
                if (game_id_input == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        //INPUT [TOPIC]
        {
            temp = GameObject.Find("TopicInput");
            if (temp != null)
            {
                topic_input = temp.GetComponent<VRInputField>();
                if (topic_input == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        //TEXT [GAMEID.CONTENT]
        {
            temp = GameObject.Find("GeneratedGameIDText");
            if (temp != null)
            {
                game_id_content_text = temp.GetComponent<Text>();
                if (game_id_content_text == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        //TEXT [GAMEID.INFO]
        {
            temp = GameObject.Find("GameIDInfoText");
            if (temp != null)
            {
                game_id_text = temp.GetComponent<Text>();
                if (game_id_text == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
                game_id_text.enabled = false;
            }
            temp = null;
        }

        //TEXT [INFO]
        {
            temp = GameObject.Find("InfoText");
            if (temp != null)
            {
                info_text = temp.GetComponent<Text>();
                if (info_text == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }

        //TEXT [NETWORK.CONNECTION.INTERNET]
        {
            temp = GameObject.Find("InternetConnectionLostText");
            if (temp != null)
            {
                internet_connection_lost_text = temp.GetComponent<Text>();
                if (internet_connection_lost_text == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
                internet_connection_lost_text.enabled = false;
            }
            temp = null;
        }

        //TESTING
        //TEXT [TESTTEXT]
        {
            temp = GameObject.Find("TESTTEXT");
            if (temp != null)
            {
                TESTTEXT = temp.GetComponent<Text>();
                if (TESTTEXT == null)
                {
                    Debug.LogError("Could not locate Canvas component on " + temp.name);
                }
            }
            temp = null;
        }
        
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------[ CLICK EVENTS ]-----------------------------------------------------------------------

    //-------------------------------------------------------[CANVAS CHANGE]
    //click event to the sign up canvas
    public void signUpClickEvent()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
        }
        else
        {
            if (!isNull<Canvas>(SwitchCanvas)) { SwitchCanvas.enabled = false; } else { Debug.LogError("SwitchCanvas is null"); }
            if (!isNull<Canvas>(SignUpCanvas)) { SignUpCanvas.enabled = true; } else { Debug.LogError("SignUpCanvas is null"); }
            if (!isNull<Canvas>(GameChooserCanvas)) { GameChooserCanvas.enabled = false; } else { Debug.LogError("LoginCanvas is null"); }
            if (!isNull<Canvas>(KeyboardCanvas)) { KeyboardCanvas.enabled = true; } else { Debug.LogError("SwitchCanvas is null"); }
            selectField(sign_up_username);
        }
    }

    //click event to the login canvas
    public void loginClickEvent()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
        }
        else
        {
            if (!isNull<Canvas>(SwitchCanvas)) { SwitchCanvas.enabled = false; } else { Debug.LogError("SwitchCanvas is null"); }
            if (!isNull<Canvas>(LoginCanvas)) { LoginCanvas.enabled = true; } else { Debug.LogError("LoginCanvas is null"); }
            if (!isNull<Canvas>(GameChooserCanvas)) { GameChooserCanvas.enabled = false; } else { Debug.LogError("LoginCanvas is null"); }
            if (!isNull<Canvas>(KeyboardCanvas)) { KeyboardCanvas.enabled = true; } else { Debug.LogError("SwitchCanvas is null"); }
            selectField(login_username);
        }
    }

    //click event to the first canvas (back button)
    public void backToSignHubButtonEvent()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
        }
        else
        {
            if (!isNull<Canvas>(SwitchCanvas)) { SwitchCanvas.enabled = true; } else { Debug.LogError("SwitchCanvas is null"); }
            if (!isNull<Canvas>(SignUpCanvas)) { SignUpCanvas.enabled = false; } else { Debug.LogError("SignUpCanvas is null"); }
            if (!isNull<Canvas>(LoginCanvas)) { LoginCanvas.enabled = false; } else { Debug.LogError("LoginCanvas is null"); }
            if (!isNull<Canvas>(KeyboardCanvas)) { KeyboardCanvas.enabled = false; } else { Debug.LogError("SwitchCanvas is null"); }
        }
    }

    //click event to join canvas
    public void goToJoinCanvas()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
        }
        else
        {
            if (!isNull<Canvas>(SwitchCanvas)) { SwitchCanvas.enabled = false; } else { Debug.LogError("SwitchCanvas is null"); }
            if (!isNull<Canvas>(SignUpCanvas)) { SignUpCanvas.enabled = false; } else { Debug.LogError("SignUpCanvas is null"); }
            if (!isNull<Canvas>(LoginCanvas)) { LoginCanvas.enabled = false; } else { Debug.LogError("LoginCanvas is null"); }
            if (!isNull<Canvas>(GameChooserCanvas)) { GameChooserCanvas.enabled = true; } else { Debug.LogError("LoginCanvas is null"); }
            if (!isNull<Canvas>(KeyboardCanvas)) { KeyboardCanvas.enabled = true; } else { Debug.LogError("SwitchCanvas is null"); }
            selectField(game_id_input);
        }
    }
    //-------------------------------------------------------[EOF CANVAS CHANGE]            <(EOF = end of)>

    //-------------------------------------------------------[SCENE CHANGE]
    public void goToCharacterSelectorScene()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
        }
        else
        {
            SceneManager.LoadScene("AvatarSelectorScene");
        }
    }
    //-------------------------------------------------------[EOF SCENE CHANGE]

    //-------------------------------------------------------[LOGIN]
    //click event to login
    public async void loginToGameButtonEvent()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
        }
        else
        {
            int result = -1; //default

            if (login_username.text.ToString().Length == 0 || login_password.text.ToString().Length == 0) //empty field check
            {
                result = 3; //empty fields
            }
            else
            {
                db = dataBaseManager.getConnection();
                try
                {
                    await db.GetReference("player").Child(login_username.text.ToString())
                                            .Child("password")
                                            .GetValueAsync()
                                            .ContinueWith(task =>
                                            {
                                                if (task.IsCompleted)
                                                {
                                                    Debug.Log("task.IsCompleted: Succeeded [Login]");
                                                    DataSnapshot data_snapshot = task.Result;

                                                    if (data_snapshot.Exists)
                                                    {
                                                        Debug.Log(task.Result.ToString() + "completed");
                                                        if (data_snapshot.Value.Equals(login_password.text.ToString()))
                                                        {
                                                            result = 0; //succeeded
                                                        Debug.Log("LOGIN SUCCEEDED");
                                                        }
                                                        else
                                                        {
                                                            result = 1; //wrong password
                                                        Debug.Log("LOGIN FAILED");
                                                        }

                                                    }
                                                    else
                                                    {
                                                        result = 2; //sign up first
                                                    Debug.Log(task.Result.ToString() + " USER DOES NOT EXISTS");
                                                    }
                                                }
                                                else
                                                {
                                                    Debug.LogError("task.IsCompleted: Failed [Login]");
                                                }


                                            });
                    
                }
                catch (Exception ex)
                {
                    Debug.Log("Exception: " + ex);
                }
            }

            switch (result) //for user messages
            {
                case -1:
                    info_text.text = "Something went wrong!";
                    break;

                case 0:
                    Data data = new Data();
                    data.username = login_username.text.ToString();
                    data.password = login_password.text.ToString();

                    jsonParser.toJson<Data>(data, "userdata");
                    goToJoinCanvas();

                    info_text.text = "";

                    break;

                case 1:
                    info_text.text = "Incorrect password!";
                    break;

                case 2:
                    info_text.text = "Incorrect username!";
                    break;

                case 3:
                    info_text.text = "Input fields are empty!";
                    break;

                default: break;
            }
        }
    }
    //-------------------------------------------------------[EOF LOGIN]

    //-------------------------------------------------------[SIGN UP]
    //click event to sign up
    public async void signUpToGameButtonEvent()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
        }
        else
        {
            int result = -1; //default

            if (sign_up_username.text.ToString().Length == 0 || sign_up_password.text.ToString().Length == 0 || sign_up_again_password.text.ToString().Length == 0) //empty field check
            {
                result = 3; //empty fields
            }
            else
            {
                db = dataBaseManager.getConnection();
                try
                {
                    await db.GetReference("player").Child(sign_up_username.text.ToString())
                                            .GetValueAsync()
                                            .ContinueWith(task =>
                                            {
                                                if (task.IsCompleted)
                                                {
                                                    Debug.Log("task.IsCompleted: Succeeded [Sign Up]");
                                                    DataSnapshot data_snapshot = task.Result; 

                                                    if (data_snapshot.Exists)
                                                    {
                                                        result = 1; //Username is exists
                                                    }
                                                    else
                                                    {
                                                        if (sign_up_password.text.ToString().Equals(sign_up_again_password.text.ToString()))
                                                        {
                                                        
                                                        //SAVE DATA
                                                        db.GetReference("player").Child(sign_up_username.text.ToString()).Child("password").SetValueAsync(sign_up_password.text.ToString());

                                                            result = 0; //Succeeded
                                                    }
                                                        else
                                                        {
                                                            result = 2; //passwords are not matching
                                                    }

                                                        Debug.Log(task.Result.ToString() + " USER DOES NOT EXISTS");
                                                    }
                                                }
                                                else
                                                {
                                                    Debug.LogError("task.IsCompleted: Failed [Sign Up]");
                                                }


                                            });
                    
                }
                catch (Exception ex)
                {
                    Debug.Log("Exception: " + ex);
                }
            }

            switch (result) //for user messages
            {
                case -1:
                    info_text.text = "Something went wrong!";
                    break;

                case 0:
                    Data data = new Data();
                    data.username = sign_up_username.text.ToString();
                    data.password = sign_up_password.text.ToString();

                    jsonParser.toJson<Data>(data, "userdata");
                    goToJoinCanvas();

                    info_text.text = "";

                    break;

                case 1:
                    info_text.text = "Username is already taken!";
                    break;

                case 2:
                    info_text.text = "Confirmation password is not match with the password!";
                    break;

                case 3:
                    info_text.text = "Input fields are empty!";
                    break;

                default: break;
            }
        }

    }
    //-------------------------------------------------------[EOF SIGN UP]

    public async void join()
    {
        Data data = new Data();
        data = jsonParser.toObject<Data>("userdata");

        bool is_joined = false;
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
        }
        else
        {
            int player_num = 999;
            bool is_game_id_exists = false;

            if (game_id_input.text.Length == 8)
            {
                string id = game_id_input.text.ToString();

                db = dataBaseManager.getConnection();
                await db.GetReference("games").Child(id.ToUpper())
                                              .GetValueAsync()
                                              .ContinueWith(task =>
                                              {
                                                  if (task.IsCompleted)
                                                  {
                                                      Debug.Log("task.IsCompleted: Succeeded [Join]");
                                                      DataSnapshot data_snapshot = task.Result;

                                                      if (data_snapshot.Exists)
                                                      {
                                                          Debug.Log(task.Result.ToString() + " GAME ID IS EXISTS");
                                                          is_game_id_exists = true; //true if game id is exists
                                                          string DEBUG = "-------------DEBUG:";
                                                          var dts = data_snapshot.Value as Dictionary<string, object>;
                                                          
                                                          player_num = dts.Count + 1;
                                                          foreach(var item in dts)
                                                          {
                                                              DEBUG += item.Key +" = " + item.Value + "\n";
                                                              if(data.username == item.Key.ToString())
                                                              {
                                                                  is_joined = true;
                                                                  string str_helper = item.Value.ToString();
                                                                  player_num = Int32.Parse(str_helper);
                                                              }
                                                          }
                                                          Debug.Log(DEBUG);
                                                      }
                                                      else
                                                      {
                                                          Debug.Log(task.Result.ToString() + " GAME ID DOES NOT EXISTS");
                                                          is_game_id_exists = false; //false, if game id is exists
                                                      }
                                                  }
                                                  else
                                                  {
                                                      Debug.LogError("task.IsCompleted: Failed [Join]");
                                                  }


                                              });
                if (is_game_id_exists)
                {
                    info_text.text = "";
                    saveGameID(id); //save game ID to json
                }
                else
                {
                    info_text.text = "Incorrect game ID!";
                }
            }
            else
            {
                info_text.text = "Game ID requires 8 letters!";
            }

            saveParticipant(player_num, is_joined);
        }
    }
    
    public async void host() 
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
        }
        else
        {
            Data dt = jsonParser.toObject<Data>("userdata");
            Debug.Log(dt.game_id + " " + dt.username);
            dt.player_position = 1;
            
            if (topic_input.text.Length == 0)
            {
                dt.topic = "NO TOPIC";
            }
            else
            {
                dt.topic = topic_input.text;
            }

            jsonParser.toJson<Data>(dt, "userdata");

            if (dt.game_id.Length > 0)
            {
                db = dataBaseManager.getConnection();
                await db.GetReference("games").Child(dt.game_id).Child(dt.username).SetValueAsync(1);

                await db.GetReference("VR_topics").Child(dt.game_id).SetValueAsync(topic_input.text);
                info_text.text = "";

                saveJoinStatus(true); // save host status (for multiplayer)
                goToCharacterSelectorScene();
            }
            else
            {
                info_text.text = "Please generate game ID first!";
            }
        }
    }

    public async void generateGameIDButton()
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
                bool is_game_id_exists = false; //check the game id -> true if exists, else false
                string generated_game_id = generateGameID(); //generate random id [8 character] 26^8 variation                              "ZYRGIMUV" <- this game ID is exists

                await db.GetReference("games").Child(generated_game_id)
                                              .GetValueAsync()
                                              .ContinueWith(task =>
                                              {
                                                  if (task.IsCompleted)
                                                  {
                                                      Debug.Log("task.IsCompleted: Succeeded [Generate]");
                                                      DataSnapshot data_snapshot = task.Result;

                                                      if (data_snapshot.Exists)
                                                      {
                                                          Debug.Log(task.Result.ToString() + " GAME ID IS EXISTS");
                                                          is_game_id_exists = true; //true if game id is exists
                                                      }
                                                      else
                                                      {
                                                          Debug.Log(task.Result.ToString() + " GAME ID DOES NOT EXISTS");
                                                          is_game_id_exists = false; //false, if game id is exists
                                                      }
                                                  }
                                                  else
                                                  {
                                                      Debug.LogError("task.IsCompleted: Failed [Generate]");
                                                  }


                                              });

                if (is_game_id_exists)
                {
                    info_text.text = "Please press the \"Generate ID\" button again, error occured!";//game ID was generated redundantly!";
                    game_id_content_text.text = "";//there is no game ID => empty string
                    game_id_text.enabled = false; //there is no game ID generated yet, so no need the title text
                }
                else
                {
                    info_text.text = ""; //there is no error
                    game_id_content_text.text = generated_game_id; //show game ID
                    game_id_text.enabled = true; //title text
                    saveGameID(generated_game_id); //save game id to json
                }

            }
            catch (Exception ex)
            {
                Debug.Log("Exception: " + ex);
            }
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------[ DATA FUNCTIONS ]----------------------------------------------------------------------

    private async void saveParticipant(int player_num, bool is_joined)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
        }
        else
        {
            Data dt = jsonParser.toObject<Data>("userdata");
            Debug.Log(dt.game_id + " " + dt.username + " " + player_num);
            if (dt.game_id.Length > 0)
            {
                if (!is_joined)
                {
                    db = dataBaseManager.getConnection();
                    await db.GetReference("games").Child(dt.game_id).Child(dt.username).SetValueAsync(player_num);
                }

                info_text.text = "";

                saveJoinStatus(false); // save host status (for multiplayer)

                dt.player_position = player_num;
                jsonParser.toJson<Data>(dt, "userdata");

                goToCharacterSelectorScene();
            }
            else
            {
                info_text.text = "Something went wrong! Please press the button again!";
            }
        }
    }

    private void saveGameID(string game_id)
    {
        Data data = jsonParser.toObject<Data>("userdata");
        data.game_id = game_id.ToUpper();
        jsonParser.toJson<Data>(data, "userdata");
    }

    private void saveJoinStatus(bool is_hosting)
    {
        Data data = jsonParser.toObject<Data>("userdata");
        data.is_host = is_hosting;
        jsonParser.toJson<Data>(data, "userdata");
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------[ GAME-ID RANDOM GENERATOR ]-----------------------------------------------------------------

    private string generateGameID()
    {
        string result_id = "";
        
        System.Random rand = new System.Random();
        for (int i = 0; i < 8; i++)
        {
            char ch = (char)(rand.Next(26) + 'a');
            result_id += ch;
        }
        
        return result_id.ToUpper();
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------[ KEYBOARD FUNCTIONS ]--------------------------------------------------------------------

    //handle selected text field
    public void selectField(VRInputField field)
    {
        inputHightlight(field);//highlight the selected text field

        if (!isNull<Canvas>(KeyboardCanvas)) { KeyboardCanvas.enabled = true; } else { Debug.LogError("SwitchCanvas is null"); } //activate keyboard 

        switch (field.name) 
        {
            case "UsernameInput":
                field_enum = FIELDENUM.LOGIN_USERNAME;
                keyboard_input_field.text = login_username.text;
                break;

            case "PasswordInput":
                field_enum = FIELDENUM.LOGIN_PASSWORD;
                keyboard_input_field.text = login_password.text;
                break;

            case "SignUpUsernameInput":
                field_enum = FIELDENUM.SIGN_UP_USERNAME;
                keyboard_input_field.text = sign_up_username.text;
                break;

            case "SignUpPasswordInput":
                field_enum = FIELDENUM.SIGN_UP_PASSWORD;
                keyboard_input_field.text = sign_up_password.text;
                break; 

            case "SignUpPasswordAgainInput":
                field_enum = FIELDENUM.SIGN_UP_PASSWORD_AGAIN;
                keyboard_input_field.text = sign_up_again_password.text;
                break;

            case "GameIDInput":
                field_enum = FIELDENUM.GAME_CODE;
                keyboard_input_field.text = game_id_input.text;
                break;

            case "TopicInput":
                field_enum = FIELDENUM.TOPIC;
                keyboard_input_field.text = topic_input.text;
                break;

            default:
                field_enum = FIELDENUM.NON;
                keyboard_input_field.text = "";
                break;
        }

        //keyboard_input_field.text = ""; //clear the keyboard field
    }

    //handle changes in the keyboard text field
    public void updateTextFields()
    {
        string temp = keyboard_input_field.text;

        if (field_enum != FIELDENUM.NON )//&& temp.Length > 0) // without this: works! (!?)
        {
            switch (field_enum)
            {
                case FIELDENUM.LOGIN_USERNAME:
                    login_username.text = temp;
                    break;

                case FIELDENUM.LOGIN_PASSWORD:
                    login_password.text = temp;
                    break;

                case FIELDENUM.SIGN_UP_USERNAME:
                    sign_up_username.text = temp;
                    break;

                case FIELDENUM.SIGN_UP_PASSWORD:
                    sign_up_password.text = temp;
                    break;

                case FIELDENUM.SIGN_UP_PASSWORD_AGAIN:
                    sign_up_again_password.text = temp;
                    break;

                case FIELDENUM.GAME_CODE:
                    game_id_input.text = temp;
                    break;
                
                case FIELDENUM.TOPIC:
                    topic_input.text = temp;
                    break;

                default:
                    field_enum = FIELDENUM.NON;
                    break;
            }
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------[ INPUT HIGHLIGHTING FUNCTION ]----------------------------------------------------------------
    
    // [INPUT FIELDS]:

    /*
        login_username;
        login_password;
        sign_up_username;
        sign_up_password;
        sign_up_again_password;
        game_id_input;
        topic_input;
    */

    private void inputHightlight(VRInputField highlight_this)
    {
        switch (highlight_this.name)
        {
            case "UsernameInput":
                login_username.image.color = new Color32(202,229,255,255);// [HIGHLIGHTED]>-----[*]
                login_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_username.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_again_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                game_id_input.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                topic_input.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                break;

            case "PasswordInput":
                login_username.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                login_password.image.color = new Color32(202, 229, 255, 255);// [HIGHLIGHTED]>-----[*]
                sign_up_username.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_again_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                game_id_input.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                topic_input.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                break;

            case "SignUpUsernameInput":
                login_username.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                login_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_username.image.color = new Color32(202, 229, 255, 255);// [HIGHLIGHTED]>-----[*]
                sign_up_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_again_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                game_id_input.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                topic_input.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                break;

            case "SignUpPasswordInput":
                login_username.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                login_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_username.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_password.image.color = new Color32(202, 229, 255, 255);// [HIGHLIGHTED]>-----[*]
                sign_up_again_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                game_id_input.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                topic_input.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                break;

            case "SignUpPasswordAgainInput":
                login_username.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                login_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_username.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_again_password.image.color = new Color32(202, 229, 255, 255);// [HIGHLIGHTED]>-----[*]
                game_id_input.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                topic_input.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                break;

            case "GameIDInput":
                login_username.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                login_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_username.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_again_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                game_id_input.image.color = new Color32(202, 229, 255, 255);// [HIGHLIGHTED]>-----[*]
                topic_input.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                break;

            case "TopicInput":
                login_username.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                login_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_username.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_again_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                game_id_input.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                topic_input.image.color = new Color32(202, 229, 255, 255);// [HIGHLIGHTED]>-----[*]
                break;

            default:
                login_username.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                login_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_username.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                sign_up_again_password.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                game_id_input.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                topic_input.image.color = new Color32(255, 255, 255, 255);// [DEFAULT]
                break;
        }

    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //---------------------------------------------------[ PRIVATE CHECK FUNCTIONS AND MISCELLANEOUS FUNCTIONS ]----------------------------------------------------

    //null check for any object 
    private bool isNull<T>(T obj)
    {
       return obj == null ? true : false;
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
}
