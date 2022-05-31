using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AcheivementManager : MonoBehaviour
{
    //event queue implementation for two achievments that could occur at the same time
    private Queue<Achievement> achievementQueue = new Queue<Achievement>();
    public Text achievmentText;
    public void NotifAchievmentComplete(string ID)
    {
        //upon notification add the acheivment to the queue
        achievementQueue.Enqueue(new Achievement(ID));
    }

    private void Start()
    {
        //indefinitely start the achievement check
        StartCoroutine("AchievmentCheck");
    }

    private void UnlockAchievement(Achievement achievement)
    {
        //when called the acheivement is logged in the console
        Debug.Log("Achievement unlocked:" + achievement.id);

        StartCoroutine(displayAchievment(achievement.id));

    }

    public IEnumerator AchievmentCheck()
    {

        for (; ; )
        {
            //dequeue upon acheivement element found
            if (achievementQueue.Count > 0) UnlockAchievement(achievementQueue.Dequeue());
            yield return new WaitForSeconds(2f);
        }
    }

    public IEnumerator displayAchievment(string ach)
    {
        achievmentText.text = "Achievment Unlocked!! " + ach;
        //adding animations when the text box is set active
        achievmentText.CrossFadeAlpha(0.0f, 1.5f, false);
        achievmentText.gameObject.SetActive(true);
        achievmentText.CrossFadeAlpha(1.0f, 1.5f, false);

        yield return new WaitForSeconds(4);
        achievmentText.gameObject.SetActive(false);
        achievmentText.text = "";

    }
}
