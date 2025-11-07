using Godot;
using System;

public partial class PlacementCast : RayCast3D {

    public Vector3 getNormalVector() {
        ForceRaycastUpdate();
        if (IsColliding()) {
            return GetCollisionNormal();
        }
        return new Vector3();
    }
}
