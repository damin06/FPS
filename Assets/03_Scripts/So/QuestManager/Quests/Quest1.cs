using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest1 : QuestScript
{
    [SerializeField] private GlowText _glowTXT;
    private void Start()
    {
        //_glowTXT = GameObject.Find("GlowText").transform.GetComponent<GlowText>();
    }
    public override void OnEnterQuest()
    {
        _glowTXT.ShowingSequence("test1111");
    }

    public override void OnQuestClear()
    {

    }

    public override void OnUpdateQues()
    {

    }

}
