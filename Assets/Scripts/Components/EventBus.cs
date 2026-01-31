using UnityEngine;

public class EventBus {

    public delegate void GameTimerAction();

    public GameTimerAction MaskHappyStart;
    public GameTimerAction MaskSadStart;
    public GameTimerAction MaskCreepyStart;
}
