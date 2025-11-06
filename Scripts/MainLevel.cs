using Godot;
using System;

public partial class MainLevel : Node3D {
    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        GameState.Instance.grid = GetNode<GridMap>("%Grid");
    }
}
