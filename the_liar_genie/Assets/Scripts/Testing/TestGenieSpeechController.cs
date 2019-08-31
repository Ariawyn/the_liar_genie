using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGenieSpeechController : MonoBehaviour, IInteractable
{
    [SerializeField] public DialogueSystem.DialogueSceneManager dialogue_manager;

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
        has_been_interacted_with_once = true;
        dialogue_manager.TriggerDialogueByDialogueInstanceName("genie_test_dialogue");
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
