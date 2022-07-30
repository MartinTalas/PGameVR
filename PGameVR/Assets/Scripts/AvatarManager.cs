using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public sealed class AvatarManager
{
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------[ VARIABLES ]------------------------------------------------------------------------

    GameObject current_avatar; // the current avatar
    
    //all avatars
    public GameObject[] casual_females = new GameObject[8];
    public GameObject[] casual_males = new GameObject[8];
    public GameObject[] elegant_females = new GameObject[8];
    public GameObject[] elegant_males = new GameObject[8];

    //actual avatar list (by gender and outfit)
    public GameObject[] actual_avatar_set = new GameObject[8];

    private int current_avatar_index = 0; //current avatar index 

    public Vector3 selector_central_position;// default position
    public Vector3 default_rotation; // default rotation: 180 

    private bool sex = false;// false = female | true = male
    private bool outfit = false;// false = casual | true = elegant

    public int[] int_avatar = { 0, 0, 0 }; //default (for json) [sex, outfit, avatarindex]

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------[ SINGLETON PATTERN ]--------------------------------------------------------------------


    private static readonly Lazy<AvatarManager> lazy = new Lazy<AvatarManager>(() => new AvatarManager());

    public static AvatarManager Instance { get { return lazy.Value; } }

    private AvatarManager()
    {
        selector_central_position = new Vector3(0.5f, 0.81f, 3.03f); //set default position
        default_rotation = new Vector3(0, 180, 0); //set default rotation
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------[ FUNCTIONS ]------------------------------------------------------------------------

    //set actual avatar set (outfit and sex ("gender"))
    GameObject[] setActualAvatarSet(bool actual_sex, bool actual_outfit)
    {
        this.sex = actual_sex;
        this.outfit = actual_outfit;

        if (this.sex)
        {
            int_avatar[0] = 1; //male

            if (this.outfit)
            {
                int_avatar[1] = 1;
                return elegant_males;
            }
            else
            {
                int_avatar[1] = 0;
                return casual_males;
            }
        }
        else
        {
            int_avatar[0] = 0; //female

            if (outfit)
            {
                int_avatar[1] = 1;
                return elegant_females;
            }
            else
            {
                int_avatar[1] = 0;
                return casual_females;
            }
        }
    }

    //change current avatar by navigation buttons
    public void currentAvatar(bool move)
    {
        if (move)
        {
            if (current_avatar_index >= 7)
            {
                current_avatar_index = 0;
            }
            else
            {
                current_avatar_index++;
            }
            
            current_avatar = actual_avatar_set[current_avatar_index];    
        }
        else
        {
            if(current_avatar_index <= 0)
            {
                current_avatar_index = 7;
            }
            else
            {
                current_avatar_index--;
            }

            current_avatar = actual_avatar_set[current_avatar_index];
        }

        int_avatar[2] = current_avatar_index;
    }

    //load the avatar lists
    public void buildLists(GameObject[] arg1, GameObject[] arg2, GameObject[] arg3, GameObject[] arg4) 
    {
        //get all avatars
        this.casual_females = arg1;
        this.casual_males = arg2;
        this.elegant_females = arg3;
        this.elegant_males = arg4;
        
        //set default avatar set
        this.actual_avatar_set = this.setActualAvatarSet(false, false);

        //set avatar positions & rotations
        for(int i = 0; i < actual_avatar_set.Length; i++)
        {
            actual_avatar_set[i].transform.rotation = UnityEngine.Quaternion.Euler(default_rotation);
            actual_avatar_set[i].transform.position = selector_central_position;

            casual_females[i].transform.rotation = UnityEngine.Quaternion.Euler(default_rotation);
            casual_females[i].transform.position = selector_central_position;

            casual_males[i].transform.rotation = UnityEngine.Quaternion.Euler(default_rotation);
            casual_males[i].transform.position = selector_central_position;

            elegant_females[i].transform.rotation = UnityEngine.Quaternion.Euler(default_rotation);
            elegant_females[i].transform.position = selector_central_position;

            elegant_males[i].transform.rotation = UnityEngine.Quaternion.Euler(default_rotation);
            elegant_males[i].transform.position = selector_central_position;
        }

        //set current avatar firstly
        this.current_avatar = actual_avatar_set[0];
        //I do not wanted to instanticate the current avatar because it runs 8 times!!! It would cause absolute chaos! I used firstly burnt prefab
        
    }

    //set sex and outfit and change the actual avatar list
    public void setProperties(bool sex1, bool outfit1)
    {
        this.actual_avatar_set = this.setActualAvatarSet(sex1, outfit1);
    }

    //make preview
    public void setPreview() 
    {
        this.current_avatar = actual_avatar_set[current_avatar_index];
        this.current_avatar.tag = "Clone";

        //destroy clones for memory saving
        var clones = GameObject.FindGameObjectsWithTag("Clone");

        foreach (var clone in clones)
        {
            MonoBehaviour.Destroy(clone);
        }

        //set current active and "generate"
        this.current_avatar.SetActive(true);
        MonoBehaviour.Instantiate(this.current_avatar, this.current_avatar.transform.position, this.current_avatar.transform.rotation); 
    }

    //get avatar properties (sex and outfit)
    public bool[] getProperties()
    {
        bool[] result = { this.sex, this.outfit }; 
        return result;
    }

    public int[] getJsonAvatarArray()
    {
        return this.int_avatar;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
}
