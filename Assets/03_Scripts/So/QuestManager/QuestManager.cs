using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[Serializable]
public struct Quests
{
    public string QuestName;
    [Tooltip("Quest설명")][Multiline] public string QuestExplanation;
    [Tooltip("Quest스크립트로 조종")] public QuestScript _quest;
    [Space]
    public UnityEvent OnEnterQuest;
    public UnityEvent OnUpdateQues;
    public UnityEvent OnQuestClear;
    [Space]
    [Header("콜라이더")]
    public Colider _col;
}

[Serializable]
public struct Colider
{
    public Vector3 ColiderSize;
    //public Quaternion rotation;
    public Vector3 ColiderPos;
    public LayerMask _lay;
    [ColorUsage(true)] public Color ColiderColor;
}

public class QuestManager : MonoBehaviour
{
    [Header("Quest Panel")]
    [SerializeField] private TextMeshProUGUI _curQuestNameTXT;
    [SerializeField] private TextMeshProUGUI _curQuestTXT;

    [Space]

    [Header("Quests")]
    public Quests[] _questList;


    public static QuestManager Instance;
    public Quests _currentQuest { get; private set; }
    public int _currentIndex { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }


        foreach (var a in _questList)
        {
            if (a._quest != null)
                a._quest.gameObject.SetActive(false);
        }

        ChangeQuest(_questList[0]);
    }

    private void Update()
    {
        _currentQuest.OnUpdateQues?.Invoke();

        if (_currentQuest._quest != null)
            _currentQuest._quest.OnUpdateQues();
    }

    [Tooltip("string이 null이면 다음 인덱스")]
    public void ChangeQuestAsColider(string _name = null)
    {
        //Collider[] _cols;

        //_cols = Physics.OverlapBox(_currentQuest.ColiderPos, _currentQuest.ColiderSize, transform.rotation, _currentQuest._lay);

        if (Physics.CheckBox(_currentQuest._col.ColiderPos, _currentQuest._col.ColiderSize, transform.rotation, _currentQuest._col._lay))
        {
            if (_name == "")
                ChangeQuestNext();
            else
                ChangeQuestAsName(_name);
        }

        // if (_cols.Length > 0)
        // {
        //     if (_name == "")
        //         ChangeQuestNext();
        //     else
        //         ChangeQuestAsName(_name);
        // }
    }

    [Tooltip("Quest이름으로 넘김")]
    public void ChangeQuestAsName(string name)
    {
        for (int i = 0; i < _questList.Length; i++)
        {
            if (_questList[i].QuestName == name)
            {
                ChangeQuest(_questList[i]);
                _currentIndex = i;
                return;
            }
        }
    }

    [Tooltip("다음 Quest로 넘김 없으면 그대로")]
    public void ChangeQuestNext()
    {
        if (_currentIndex >= _questList.Length)
        {
            Debug.LogError("인덱스 범위를 초과함");
            return;
        }

        ChangeQuest(_questList[++_currentIndex]);
    }

    private void ChangeQuest(Quests qu)
    {
        _currentQuest.OnQuestClear?.Invoke();
        if (_currentQuest._quest != null)
        {
            _currentQuest._quest.gameObject.SetActive(true);
            _currentQuest._quest?.OnQuestClear();
            _currentQuest._quest?.gameObject.SetActive(false);
        }

        _currentQuest = qu;

        _currentQuest.OnEnterQuest?.Invoke();
        if (_currentQuest._quest != null)
        {
            _currentQuest._quest?.gameObject.SetActive(true);
            _currentQuest._quest.OnEnterQuest();
        }

        Debug.Log($"CurrentQuest : {_currentQuest.QuestName}");

        // if ((_curQuestNameTXT = null) || (_curQuestTXT = null))
        //     return;

        _curQuestNameTXT.text = _currentQuest.QuestName.ToString();
        _curQuestTXT.text = _currentQuest.QuestExplanation.ToString();
    }

    private void OnDrawGizmos()
    {
        foreach (var item in _questList)
        {
            Gizmos.color = item._col.ColiderColor;
            Gizmos.DrawCube(item._col.ColiderPos, item._col.ColiderSize);
        }
    }
}
