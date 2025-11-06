using Godot;
using System;

public partial class HandTarget : Marker3D {
    public GridMap grid;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {

        GameState.Instance.GridSet += (val) => setGrid(val);
    }

    private void setGrid(GridMap g) { grid = g; }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {}

    public Vector3I getGridLoc() {
        return grid.LocalToMap(grid.ToLocal(GlobalPosition));
    }
}
