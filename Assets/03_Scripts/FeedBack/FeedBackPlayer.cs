using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBackPlayer : MonoBehaviour
{
    [SerializeField] private List<FeedBack> _feedbackToPlay = null;

    public void PlayerFeedback()
    {
        FinishFeedback();
        foreach (FeedBack f in _feedbackToPlay)
        {
            f.CreateFeedBack();
        }
    }

    public void FinishFeedback()
    {
        foreach (FeedBack f in _feedbackToPlay)
        {
            f.CompleteFeedBack();
        }
    }
}
