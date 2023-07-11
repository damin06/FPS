using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestScript : MonoBehaviour
{
    public abstract void OnEnterQuest();
    public abstract void OnUpdateQues();
    public abstract void OnQuestClear();

}
