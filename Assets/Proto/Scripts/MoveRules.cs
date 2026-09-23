using UnityEngine;

public enum MoveIntent
{
    StepUp1,
    StepUp2,
    HopInPlace
}

public class MoveRules : MonoBehaviour
{
    public bool CanMove(Player player, Player other, MoveIntent intent, Judgment judgment, out string reason)
    {
        reason = default;
        return default;
    }

    private bool MustMatchPlayerColor(Player player, int targetStep)
    {
        return default;
    }

    private bool DoubleStepRequiresPerfect(MoveIntent intent, Judgment judgment)
    {
        return default;
    }
}
