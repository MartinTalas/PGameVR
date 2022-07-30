using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions;

public sealed class DataBaseManager
{
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------[ VARIABLESS ]------------------------------------------------------------------------

    //https://p-game-a75c2-default-rtdb.europe-west1.firebasedatabase.app/

    private FirebaseDatabase db;
    public string exception;

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------[ SINGLETON PATTERN ]--------------------------------------------------------------------

    private static readonly Lazy<DataBaseManager> lazy = new Lazy<DataBaseManager>(() => new DataBaseManager());

    public static DataBaseManager Instance { get { return lazy.Value; } }

    private DataBaseManager()
    {
        try
        {
            this.db = FirebaseDatabase.GetInstance("https://p-game-a75c2-default-rtdb.europe-west1.firebasedatabase.app/");
        }
        catch (Exception ex)
        {
            Debug.Log("DB_EXCEPTION: " + ex.Message + "\n" + ex);
        }

    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------[ CONNECTION FUNCTION ]--------------------------------------------------------------------

    public FirebaseDatabase getConnection()
    {
        if (db == null)
        { 
            db = FirebaseDatabase.GetInstance("https://p-game-a75c2-default-rtdb.europe-west1.firebasedatabase.app/");
            return db;
        }
        else
        {
            return db;
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
}
