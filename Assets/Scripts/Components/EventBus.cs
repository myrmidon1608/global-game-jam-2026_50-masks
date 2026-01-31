using UnityEngine;

public class EventBus {

    public delegate void GameAction();

    public GameAction WinGame;
    public GameAction LoseGame;

    public delegate void GameTimerAction();

    public GameTimerAction MaskHappyStart;
    public GameTimerAction MaskSadStart;
    public GameTimerAction MaskCreepyStart;
}
