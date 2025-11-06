using Godot;
using System;

public partial class GameState : Node {
    [Signal]
    public delegate void GridSetEventHandler(GridMap grid);

    /*SINGLETON CODE*/

    public static GameState Instance { get; private set; }

    public override void _Ready() { Instance = this; }

    private GridMap _grid;

    public GridMap grid {
        get { return _grid; }
        set {
            _grid = value;
            EmitSignal(SignalName.GridSet, value);
        }
    }
}
