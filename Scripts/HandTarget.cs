using Godot;
using System;

public partial class HandTarget : Marker3D {
    public PlacementCast placementCast;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        placementCast = GetNode<PlacementCast>("%PlacementCast");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {}

    public Vector3 getRawGridLoc() {
        return GlobalPosition.Snapped(new Vector3(1, 1, 1));
    }

    public Vector3 getGridLoc() {
        Vector3 pointerLoc = getRawGridLoc();
        (Vector3, Vector3, bool)cascade = placementCast.getNormalVector();

        if (cascade.Item3) {
            Vector3 raycastLoc = cascade.Item2;
            Vector3 normal = cascade.Item1;
            if (normal.X > 0) {
                normal.X -= 0.5f;
            }
            if (normal.Y > 0) {
                normal.Y -= 0.5f;
            }
            if (normal.Z > 0) {
                normal.Z -= 0.5f;
            }
            if (normal.X < 0) {
                normal.X += 0.5f;
            }
            if (normal.Y < 0) {
                normal.Y += 0.5f;
            }
            if (normal.Z < 0) {
                normal.Z += 0.5f;
            }

            Vector3 cascadeInt =
                new Vector3(raycastLoc.X + normal.X, raycastLoc.Y + normal.Y,
                            raycastLoc.Z + normal.Z)
                    .Snapped(new Vector3(1, 1, 1));
            return cascadeInt;
        }
        return pointerLoc;

        // Something's there, adapt
        // TODO: raycast

        // get local positive z of self (points to player)
        // convert to Global vector3
        // get raw x y and z of aformentioned global vector3
        // for positive x y and z's math.max the corresponding axis, else
        // math.min, store as target ideally, this limits the placeable faces to
        // the 3 that face towards the player next, add one to the axis closest
        // to its target.
    }
}
