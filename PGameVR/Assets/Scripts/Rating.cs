using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class Rating
{
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------[ VARIABLESS ]------------------------------------------------------------------------
    
    Button btn1;
    Button btn2;
    Button btn3;
    Button btn4;
    Button btn5;

    private int vote_rate;

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------[ SINGLETON PATTERN ]--------------------------------------------------------------------

    private static readonly Lazy<Rating> lazy = new Lazy<Rating>(() => new Rating());

    public static Rating Instance { get { return lazy.Value; } }

    private Rating()
    {
        btn1 = GameObject.Find("One").GetComponent<Button>();
        btn2 = GameObject.Find("Two").GetComponent<Button>();
        btn3 = GameObject.Find("Three").GetComponent<Button>();
        btn4 = GameObject.Find("Four").GetComponent<Button>();
        btn5 = GameObject.Find("Five").GetComponent<Button>();

        vote_rate = 0;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------[ RATING FUNCTIONS ]---------------------------------------------------------------------

    public void rateOneR()
    {
        vote_rate = 1;
        Button btn = GameObject.Find("One").GetComponent<Button>();
        highlightRateButtons(btn, new Color32(49, 200, 61, 255));
    }
    public void rateTwoR()
    {
        vote_rate = 2;
        Button btn = GameObject.Find("Two").GetComponent<Button>();
        highlightRateButtons(btn, new Color32(49, 200, 61, 255));
    }
    public void rateThreeR()
    {
        vote_rate = 3;
        Button btn = GameObject.Find("Three").GetComponent<Button>();
        highlightRateButtons(btn, new Color32(49, 200, 61, 255));
    }
    public void rateFourR()
    {
        vote_rate = 4;
        Button btn = GameObject.Find("Four").GetComponent<Button>();
        highlightRateButtons(btn, new Color32(49, 200, 61, 255));
    }
    public void rateFiveR()
    {
        vote_rate = 5;
        Button btn = GameObject.Find("Five").GetComponent<Button>();
        highlightRateButtons(btn, new Color32(49, 200, 61, 255));
    }

    public void hoverOneR()
    {
        Button btn = GameObject.Find("One").GetComponent<Button>();
        highlightRateButtons(btn, new Color32(100, 205, 115, 255));
    }
    public void hoverTwoR()
    {
        Button btn = GameObject.Find("Two").GetComponent<Button>();
        highlightRateButtons(btn, new Color32(100, 205, 115, 255));
    }
    public void hoverThreeR()
    {
        Button btn = GameObject.Find("Three").GetComponent<Button>();
        highlightRateButtons(btn, new Color32(100, 205, 115, 255));
    }
    public void hoverFourR()
    {
        Button btn = GameObject.Find("Four").GetComponent<Button>();
        highlightRateButtons(btn, new Color32(100, 205, 115, 255));
    }
    public void hoverFiveR()
    {
        Button btn = GameObject.Find("Five").GetComponent<Button>();
        highlightRateButtons(btn, new Color32(100, 205, 115, 255));
    }

    public void hoverExitR()
    {
        Button btn = GameObject.Find("NONE").GetComponent<Button>();

        Debug.Log(vote_rate);
        switch (vote_rate)
        {
            case 0:
                highlightRateButtons(btn, new Color32(255, 255, 255, 255));
                break;
            case 1:
                highlightRateButtons(btn1, new Color32(49, 200, 61, 255));
                break;
            case 2:
                highlightRateButtons(btn2, new Color32(49, 200, 61, 255));
                break;
            case 3:
                highlightRateButtons(btn3, new Color32(49, 200, 61, 255));
                break;
            case 4:
                highlightRateButtons(btn4, new Color32(49, 200, 61, 255));
                break;
            case 5:
                highlightRateButtons(btn5, new Color32(49, 200, 61, 255));
                break;
            default:
                highlightRateButtons(btn, new Color32(255, 255, 255, 255));
                break;
        }
    }



    void highlightRateButtons(Button btn, Color32 color)
    {
        Color32 default_color = new Color32(255, 255, 255, 255);

        switch (btn.name)
        {
            case "One":
                btn1.image.color = color; //change color
                btn2.image.color = default_color; //default color
                btn3.image.color = default_color; //default color
                btn4.image.color = default_color; //default color
                btn5.image.color = default_color; //default color
                break;

            case "Two":
                btn1.image.color = color; //change color
                btn2.image.color = color; //change color
                btn3.image.color = default_color; //default color
                btn4.image.color = default_color; //default color
                btn5.image.color = default_color; //default color
                break;

            case "Three":
                btn1.image.color = color; //change color
                btn2.image.color = color; //change color
                btn3.image.color = color; //change color
                btn4.image.color = default_color; //default color
                btn5.image.color = default_color; //default color
                break;

            case "Four":
                btn1.image.color = color; //change color
                btn2.image.color = color; //change color
                btn3.image.color = color; //change color
                btn4.image.color = color; //change color
                btn5.image.color = default_color; //default color
                break;

            case "Five":
                btn1.image.color = color; //change color
                btn2.image.color = color; //change color
                btn3.image.color = color; //change color
                btn4.image.color = color; //change color
                btn5.image.color = color; //change color
                break;

            default:
                btn1.image.color = default_color; //default color
                btn2.image.color = default_color; //default color
                btn3.image.color = default_color; //default color
                btn4.image.color = default_color; //default color
                btn5.image.color = default_color; //default color
                break;
        }
    }

    public void resetButtons()
    {
        Color32 default_color = new Color32(255, 255, 255, 255);
        btn1.image.color = default_color; //default color
        btn2.image.color = default_color; //default color
        btn3.image.color = default_color; //default color
        btn4.image.color = default_color; //default color
        btn5.image.color = default_color; //default color
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------[ GETTER / SETTER FUNCTIONS ]----------------------------------------------------------------
    
    public int getVoteRate()
    {
        return this.vote_rate;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
}
