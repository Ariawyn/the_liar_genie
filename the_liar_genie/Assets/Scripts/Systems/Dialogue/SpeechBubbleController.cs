using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class SpeechBubbleController : MonoBehaviour
    {
        public bool is_displaying = false;
        public int current_dialogue_response_index = -1;

        public void DisplayDialogueLine(DialogueLine line)
        {
            is_displaying = true;
        }

        public void DisplayDialogueResponses(DialogueResponse[] responses)
        {
            is_displaying = true;
            current_dialogue_response_index = 0;
        }

        public void ClearSpeechBubble()
        {
            is_displaying = false;
            current_dialogue_response_index = -1;
        }
    }
}
