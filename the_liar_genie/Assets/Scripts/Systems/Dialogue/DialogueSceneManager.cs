﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem 
{
    public class DialogueSceneManager : MonoBehaviour
    {
        public static DialogueSceneManager instance;

        [SerializeField] private Dialogue[] scene_dialogues;
        [SerializeField] private Dictionary<CharacterID, SpeechBubbleController> scene_character_to_speech_bubble_controller_mapping;

        private Dialogue currently_running_dialogue = null;
        private int current_running_dialogue_current_line_index;
        private bool currently_has_response_choice_open = false;

        void Awake()
        {
            if(instance != null) 
            {
                Destroy(this);
            }
            else 
            {
                instance = this;
            }
        }

        void Update()
        {
            if(currently_running_dialogue != null)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    HandleDialogueProgression();
                }
            }
        }

        public Dialogue GetDialogueByDialogueInstanceName(string instance_name)
        {
            Dialogue result = null;

            foreach(Dialogue dialogue in scene_dialogues)
            {
                if(dialogue.dialogue_instance_title == instance_name)
                {
                    result = dialogue;
                    break;
                }
            }

            return result;
        }

        public bool TriggerDialogueByDialogueInstanceName(string instance_name)
        {
            bool success_result = false;

            if(currently_running_dialogue == null)
            {
                Dialogue dialogue_to_trigger = GetDialogueByDialogueInstanceName(instance_name);

                if(dialogue_to_trigger != null)
                {
                    SpeechBubbleController raz_speech_bubble_controller = scene_character_to_speech_bubble_controller_mapping[CharacterID.Raz];
                    if(raz_speech_bubble_controller != null)
                    {
                        if(dialogue_to_trigger.is_speaking_with_character)
                        {
                            SpeechBubbleController other_character_speech_bubble_controller = scene_character_to_speech_bubble_controller_mapping[dialogue_to_trigger.character_speaking_with];
                            if(other_character_speech_bubble_controller != null)
                            {
                                StartDialogue(dialogue_to_trigger);
                                success_result = true;
                            }
                        }
                        else
                        {
                            StartDialogue(dialogue_to_trigger);
                            success_result = true;
                        }
                    }
                }
            }

            return success_result;
        }

        public void StartDialogue(Dialogue dialogue_to_start)
        {
            currently_running_dialogue = dialogue_to_start;
            HandleDialogueLineByIndex(0);
        }

        public void ClearDialogue()
        {
            currently_running_dialogue = null;
            current_running_dialogue_current_line_index = -1;
        }

        public void HandleDialogueLineByIndex(int index)
        {
            current_running_dialogue_current_line_index = index;

            DialogueLine current_dialogue_line = currently_running_dialogue.dialogue_lines[current_running_dialogue_current_line_index];
            CharacterID line_speaker = current_dialogue_line.character_speaking_line;

            ClearAllSpeechBubblesExceptFromSpecificCharacter(line_speaker);
            scene_character_to_speech_bubble_controller_mapping[line_speaker].DisplayDialogueLine(current_dialogue_line);
        }

        public void HandleDialogueProgression()
        {
            DialogueLine current_dialogue_line = currently_running_dialogue.dialogue_lines[current_running_dialogue_current_line_index];
            int next_dialogue_line_index = current_running_dialogue_current_line_index + 1;
            
            if(next_dialogue_line_index >= currently_running_dialogue.dialogue_lines.Length)
            {
                ClearDialogue();
            }
            else
            {
                // Check if we need to handle response stuff first before going straight to next dialogue line
                if(current_dialogue_line.available_responses.Length >= 1 && !currently_has_response_choice_open)
                {
                    HandleOpenDialogueResponses();
                }
                else if(current_dialogue_line.available_responses.Length >= 1 && currently_has_response_choice_open)
                {
                    HandleSelectingDialogueResponse();
                }
                else
                {
                    current_running_dialogue_current_line_index = next_dialogue_line_index;
                    current_dialogue_line = currently_running_dialogue.dialogue_lines[current_running_dialogue_current_line_index];
                    CharacterID line_speaker = current_dialogue_line.character_speaking_line;

                    ClearAllSpeechBubblesExceptFromSpecificCharacter(line_speaker);
                    scene_character_to_speech_bubble_controller_mapping[line_speaker].DisplayDialogueLine(current_dialogue_line);
                }
            }
        }

        public void HandleOpenDialogueResponses()
        {
            currently_has_response_choice_open = true;

            DialogueLine current_dialogue_line = currently_running_dialogue.dialogue_lines[current_running_dialogue_current_line_index];
            CharacterID line_speaker = current_dialogue_line.character_speaking_line;

            ClearAllSpeechBubblesExceptFromSpecificCharacter(line_speaker);
            scene_character_to_speech_bubble_controller_mapping[line_speaker].DisplayDialogueResponses(current_dialogue_line.available_responses);
        }

        public void HandleSelectingDialogueResponse()
        {
            currently_has_response_choice_open = false;
            
            DialogueLine current_dialogue_line = currently_running_dialogue.dialogue_lines[current_running_dialogue_current_line_index];
            CharacterID line_speaker = current_dialogue_line.character_speaking_line;

            int selected_response_index = scene_character_to_speech_bubble_controller_mapping[line_speaker].current_dialogue_response_index;
            DialogueResponse selected_response = current_dialogue_line.available_responses[selected_response_index];

            HandleDialogueLineByIndex(selected_response.next_dialogue_line_index);
        }

        public void ClearAllSpeechBubblesExceptFromSpecificCharacter(CharacterID character_to_exclude_from_clearing)
        {
            foreach(CharacterID id in System.Enum.GetValues(typeof(CharacterID)))
            {
                if(id != character_to_exclude_from_clearing)
                {
                    if(scene_character_to_speech_bubble_controller_mapping[id].is_displaying)
                    {
                        scene_character_to_speech_bubble_controller_mapping[id].ClearSpeechBubble();
                    }
                }
            }
        }
    }
}
