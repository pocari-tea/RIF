using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    private static string ID;

    public static void GetTest(string t)
    {
        ID = t;
    }
    
    public static string SetTest()
    {
        return ID;
    }
}
