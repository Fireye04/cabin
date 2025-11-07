using Godot;
using System;

public partial class HandTarget : Marker3D {
    public GridMap grid;
    public PlacementCast placementCast;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        placementCast = GetNode<PlacementCast>("%PlacementCast");

        GameState.Instance.GridSet += (val) => setGrid(val);
    }

    private void setGrid(GridMap g) { grid = g; }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {}

    public Vector3I getRawGridLoc() {
        return grid.LocalToMap(grid.ToLocal(GlobalPosition));
    }

    public Vector3I getGridLoc() {
        Vector3I pointerLoc = getRawGridLoc();
        Vector3 cascade = placementCast.getNormalVector();
        GD.Print(cascade);

        if (grid.GetCellItem(pointerLoc) == -1) {
            return pointerLoc;
        }
        // Something's there, adapt
        // TODO: raycast
        Vector3I cascadeInt = new Vector3I(pointerLoc.X + (int)cascade.X,
                                           pointerLoc.Y + (int)cascade.Y,
                                           pointerLoc.Z + (int)cascade.Z);
        return cascadeInt;

        // get local positive z of self (points to player)
        // convert to Global vector3
        // get raw x y and z of aformentioned global vector3
        // for positive x y and z's math.max the corresponding axis, else
        // math.min, store as target ideally, this limits the placeable faces to
        // the 3 that face towards the player next, add one to the axis closest
        // to its target.
    }
}
