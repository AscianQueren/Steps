using UnityEngine;

public enum Judgment
{
    Perfect,
    Good,
    Miss
}

public class BeatClock : MonoBehaviour
{
    private void Update()
    {
    }

    public void Play()
    {
    }

    public void Stop()
    {
    }

    public float GetBeatPosition()
    {
        return default;
    }

    public double GetNextBeatTime()
    {
        return default;
    }

    public Judgment GetJudgment(double inputTime)
    {
        return default;
    }

    private void TickBeat(int beat)
    {
    }
}
