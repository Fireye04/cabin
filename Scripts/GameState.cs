using Godot;
using System;

public partial class GameState : Node {
    [Signal]
    public delegate void MapSetEventHandler(Node3D map);

    /*SINGLETON CODE*/

    public static GameState Instance { get; private set; }

    public override void _Ready() { Instance = this; }

    private Node3D _map;

    public Node3D map {
        get { return _map; }
        set {
            _map = value;
            EmitSignal(SignalName.MapSet, value);
        }
    }
}
