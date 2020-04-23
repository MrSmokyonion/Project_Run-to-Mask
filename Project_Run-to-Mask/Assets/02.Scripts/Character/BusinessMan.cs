using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusinessMan : BaseCharacter
{
    private void Start()
    {
        Init_Variable();
    }

    private void Update()
    {
        Do_RefillJumpCnt();
    }
}//end class