using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeManager
{
    private static float slowMoTime = 0.7f, normalTime = 1.0f;

    public static void DoSlowMotion(bool doSlowMo)
    {
        if (doSlowMo)
        {
            Time.timeScale = slowMoTime;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }

        else
        {
            Time.timeScale = normalTime;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }

    }
}

