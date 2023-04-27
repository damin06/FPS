using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FeedBack : MonoBehaviour
{
    public abstract void CreateFeedBack();
    public abstract void CompleteFeedBack();

    protected virtual void OnDestroy()
    {
        CompleteFeedBack();
    }

    protected virtual void OnDisable()
    {
        CompleteFeedBack();
    }
}
