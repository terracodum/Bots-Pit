using NUnit.Framework.Interfaces;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NPCTrigger : MonoBehaviour 
{
    [SerializeField] private TextAsset _inkJSON;

    private bool _isPlaerEnter;

    private DialogueController _dialogueController;
    private DialogueWindow _dialogueWindow;

    private void Start()
    {
        _isPlaerEnter = false;
        _dialogueController = FindFirstObjectByType<DialogueController>();
        _dialogueWindow = FindFirstObjectByType<DialogueWindow>();
    }

    private void Update()
    {
        if (_dialogueWindow.IsPlaing == true || _isPlaerEnter == false)
        {
            return;
        };

        if (Input.GetKeyDown(KeyCode.E))
        {
            _dialogueController.EnterDialogueMode(_inkJSON);
            
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        

        if (collider.tag == "Player") 
        {
            Debug.Log("Enter");
            _isPlaerEnter = true;
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        

        if (collider.tag == "Player")
        {
            Debug.Log("Exit");
            _isPlaerEnter = false;
        }
    }

}