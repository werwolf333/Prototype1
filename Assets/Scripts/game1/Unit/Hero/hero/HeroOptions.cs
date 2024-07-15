using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Hero : Unit
{
    public enum Options
    {
        walk,
        run,
        sprint
    }

    public Options optionsMoveHero;
    private string moveHero;

    void OnValidate()
    {
        UpdateOptionString(optionsMoveHero);
    }

    public void SetOption(Options newOption)
    {
        UpdateOptionString(optionsMoveHero);
    }

    private void UpdateOptionString(Options option)
    {
        switch (option)
        {
            case Options.walk:
                moveHero = "walk";
                runningSpeed = 1f;
                break;
            case Options.run:
                moveHero = "run";
                runningSpeed = 2f;
                break;
            case Options.sprint:
                moveHero = "sprint";
                runningSpeed = 3f;
                break;
        }
    }
}
