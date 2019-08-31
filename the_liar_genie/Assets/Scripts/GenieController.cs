using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenieController : MonoBehaviour, IInteractable
{
    [SerializeField] public DialogueSystem.DialogueSceneManager dialogue_manager;

    [SerializeField] public string[] dialogues_to_run;
    [System.NonSerialized] private int current_dialogue_index = 0
    ;
    [System.NonSerialized] private bool player_in_range;
    [System.NonSerialized] private bool has_been_interacted_with_once = false;

    // Update is called once per frame
    void Update()
    {
        if(player_in_range && !has_been_interacted_with_once)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }

    public void Interact()
    {
        Debug.Log("Interacting once with the Genie to start dialogue");
        has_been_interacted_with_once = true;
        bool result = dialogue_manager.TriggerDialogueByDialogueInstanceName(dialogues_to_run[current_dialogue_index], true);
        Debug.Log("Successfully triggered dialogue: ");
        Debug.Log(result);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player entered range");
            player_in_range = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player exited range");
            player_in_range = false;
        }
    }
}
