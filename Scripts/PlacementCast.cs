using Godot;
using System;

public partial class PlacementCast : RayCast3D {

    public (Vector3, Vector3, bool) getNormalVector() {
        ForceRaycastUpdate();
        if (IsColliding()) {
            return (GetCollisionNormal(), (GetCollisionPoint()), true);
        }
        return (new Vector3(), new Vector3(), false);
    }
}
