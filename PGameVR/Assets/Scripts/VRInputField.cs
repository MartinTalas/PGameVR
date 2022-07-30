using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRInputField : InputField
{
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------[ INHERITED FROM INPUTFIELD ]-----------------------------------------------------------------

    protected override void Start()
    {
        keyboardType = (TouchScreenKeyboardType)(-1);
        base.Start();
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
}