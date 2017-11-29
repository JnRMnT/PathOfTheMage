using JMGames.Framework;
using JMGames.Scripts.UI;
using System.Collections;
using UnityEngine;

public class Experience : JMBehaviour
{
    public int CurrentLevel = 1;
    public long CurrentExperience;

    public override void DoStart()
    {
        StartCoroutine(InitializeBar());
        base.DoStart();
    }

    private IEnumerator InitializeBar()
    {
        yield return new WaitUntil(() => { return ExperienceBar.Instance != null; });
        ExperienceBar.Instance.UpdateBar(FloatingExperiencePercentage);
        GainExperience(5200);
    }

    public float FloatingExperiencePercentage
    {
        get
        {
            return (float)CurrentExperience / (float)RequiredExpToLevelUp;
        }
    }

    private long requiredExpToLevelUp = -1;
    public long RequiredExpToLevelUp
    {
        get
        {
            if (requiredExpToLevelUp == -1)
            {
                requiredExpToLevelUp = (long)(Mathf.Round(Mathf.Pow(2, CurrentLevel / 10) * 1000));
            }
            return requiredExpToLevelUp;
        }
    }

    public void GainExperience(int amount)
    {
        while (amount >= RequiredExpToLevelUp)
        {
            amount = (int)(amount - RequiredExpToLevelUp);
            LevelUp();
        }

        CurrentExperience += amount;
        ExperienceBar.Instance.UpdateBar(FloatingExperiencePercentage);
    }

    private void LevelUp()
    {
        CurrentLevel++;
        CurrentExperience = 0;
        requiredExpToLevelUp = -1;
    }
}
