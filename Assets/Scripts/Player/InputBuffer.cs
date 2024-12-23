using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBuffer
{
    private Queue<BufferedInput> buffer = new Queue<BufferedInput>();
    private float bufferTime;

    public InputBuffer(float bufferTime)
    {
        this.bufferTime = bufferTime;
    }

    public void AddInput(ActionType action)
    {
        buffer.Enqueue(new BufferedInput(action, Time.time));
    }

    public bool TryConsumeInput(ActionType action)
    {
        while (buffer.Count > 0)
        {
            BufferedInput input = buffer.Peek();

            // Check if input is still valid
            if (Time.time - input.TimeStamp > bufferTime)
            {
                buffer.Dequeue(); // Discard expired input
                continue;
            }

            if (input.Action == action)
            {
                buffer.Dequeue(); // Consume input
                return true;
            }

            break;
        }

        return false;
    }

    private struct BufferedInput
    {
        public ActionType Action { get; }
        public float TimeStamp { get; }

        public BufferedInput(ActionType action, float timeStamp)
        {
            Action = action;
            TimeStamp = timeStamp;
        }
    }
}

public enum ActionType
{
    Roll,
    Attack
}

