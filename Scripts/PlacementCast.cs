using Godot;
using System;

public partial class PlacementCast : RayCast3D {
    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) {}

    public Vector3 getNormal() {
        ForceRaycastUpdate();
        return GetCollisionNormal();
    }
}
