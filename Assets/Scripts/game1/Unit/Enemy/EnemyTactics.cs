using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy : Unit
{
    public enum Options
    {
        patrol,
        attack,
        comeback,
        wait
    }
    public Options optionsTactics;
    protected string curTactics;

    void OnValidate()
    {
        UpdateOption(optionsTactics);
    }

    private void UpdateOptionTactics(Options option)
    {
        optionsTactics = option;
        UpdateOption(option);
    }

    private void UpdateOption(Options option)
    {
        if(redy)
        {
            if (coroutinePatrol != null)
            {
                StopCoroutine(coroutinePatrol);
                StopCoroutine(moveToNextPointCoroutine);
                coroutinePatrol = null;
            }
            switch (option)
            {
                case Options.patrol:
                if(curTactics != "patrol")
                {
                    ToPatrol();
                }
                    curTactics = "patrol";
                    break;
                case Options.attack:
                    curTactics = "attack";
                    break;
                case Options.comeback:
                    curTactics = "comeback";
                    break;
                case Options.wait:
                    curTactics = "wait";
                    break;
            }
        }
    }
}
