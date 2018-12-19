using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeManager
{
    private static float slowMoTime = 0.3f, normalTime = 1.0f;
    private static bool doSlowMo = false;



    static void Start()
    {
        doSlowMo = false;
    }
    public static void DoSlowMotion()
    {
        if (doSlowMo)
        {
            Time.timeScale = slowMoTime;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }

    }

    public static void DoNormalTime()
    {
        if (!doSlowMo)
        {
            Time.timeScale = normalTime;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }

    public static bool GetDoSlowMo()
    {
        return doSlowMo;

    }

    public static void SetDoSlowMo(bool doSlowMO)
    {
        doSlowMo = doSlowMO;

    }
}

