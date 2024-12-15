using Ink.Runtime;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.DedicatedServer;

[RequireComponent(typeof(DialogueChoice))]
public class DialogueWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _displayName;
    [SerializeField] private TextMeshProUGUI _displayText;

    [SerializeField] private GameObject _dialogueWindow;

    [SerializeField, Range(0f, 0.5f)] private float _cooldownNewLetter;

    private DialogueChoice _dialogueChoice;

    public bool IsStatusAnswer { get; private set; }
    public bool IsPlaing { get; private set; }
    public bool CanContinueToNextLine { get; private set; }

    public float CoolDownNewLetter
    {
        get => _cooldownNewLetter;
        private set
        {
            _cooldownNewLetter = CheckCooldown(value);
        }

    }

    private float CheckCooldown(float value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Значение задержки между буквами было отрицательное");
        }
        return value;
    }
    public void SetActive(bool active)
    {
        IsPlaing = active;
        _dialogueWindow.SetActive(active);
    }
    public void init()
    {
        IsStatusAnswer = false;
        CanContinueToNextLine = false;
        _dialogueChoice = GetComponent<DialogueChoice>();
        _dialogueChoice.Init();
    }
    public void SetText(string text)
    {
        _displayText.text = text;
    }
    public void Add(string text)
    {
        _displayText.text += text;
    }
    public void Add(char letter)
    {
        _displayText.text += letter;
    }
    public void ClearText()
    {
        SetText("");
    }
    public void SetName(string namePersone)
    {
        _displayName.text = namePersone;
    }
    public void SetCoolDown(float cooldown)
    {
        _cooldownNewLetter = cooldown;
    }
    public void MakeChoice()
    {
        if (CanContinueToNextLine == false)
        {
            return;
        }
        IsStatusAnswer = false;
    }
    public IEnumerator DisplayLine(Story story)
    {
        string line = story.Continue();

        ClearText();

        _dialogueChoice.HideChoices();

        CanContinueToNextLine = false;

        bool isAddingRichText = false;

        yield return new WaitForSeconds(0.001f); //без неё всё ломаеться(не трогать!)

        foreach (char letter in line.ToCharArray())
        {
            if (Input.GetMouseButton(0))
            {
                SetText(line);
                break;
            }

            isAddingRichText= letter == '<' || isAddingRichText;

            if(letter == '>')
            {
                isAddingRichText = false;
            }
            Add(letter);

            if (isAddingRichText == false)
            {
                yield return new WaitForSeconds(_cooldownNewLetter);
            }
        }


        CanContinueToNextLine = true;
        IsStatusAnswer = _dialogueChoice.DisplayChoices(story);
    } 
}

