using System.Collections;
using System.Collections.Generic;

public class Player
{

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------[ CONSTRUCTORS ]-----------------------------------------------------------------------
    
    public Player() { achievements = new List<int>(); }
    public Player(string username_arg, string password_arg): this() 
    {
        this.Username = username_arg;
        this.password = password_arg;
        this.Score = 0; //DEFAULT
    }


    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------[ VARIABLESS ]------------------------------------------------------------------------
    
    private int id;
    private string username;
    private string password;
    private int score;
    private List<int> achievements; // by achievement id
    private bool isSpeaker;

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------[ GETTER / SETTER FUNCTIONS ]----------------------------------------------------------------

    public int ID { get { return this.id; } set { this.id = value; } }
    public string Username { get { return this.username; } set { this.username = value; } }
    public string Password { get { return this.password; } set { this.password = value; } }  
    public int Score { get { return this.score; } set { this.score = value; } }
    public List<int> Achievements { get { return this.achievements; } set { this.achievements = value; } }
    public bool IsSpeaker { get { return this.isSpeaker; } set { this.isSpeaker = value; } }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
}
