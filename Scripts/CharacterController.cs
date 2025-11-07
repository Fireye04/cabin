using Godot;
using System;

// Loosely interpreted from
// https://github.com/Brackeys/brackeys-proto-controller/blob/main/proto_controller/proto_controller.gd

public partial class CharacterController : CharacterBody3D {
    public const float Speed = 5.0f;
    public const float JumpVelocity = 4.5f;

    public bool mouseCaptured = false;
    public Vector2 lookRotation;

    [Export]
    public float lookSpeed = 0.002f;

    public Vector3I focusedLoc;

    public Node3D head;
    public HandTarget handTarget;
    public GridMap grid;

    public override void _Ready() {
        head = GetNode<Node3D>("%Head");
        handTarget = GetNode<HandTarget>("%HandTarget");
        lookRotation.Y = Rotation.Y;
        lookRotation.X = head.Rotation.X;
        focusedLoc = new Vector3I();

        GameState.Instance.GridSet += (val) => setGrid(val);
    }

    private void setGrid(GridMap g) { grid = g; }

    private void placeItem(int item) {
        grid.SetCellItem(handTarget.getGridLoc(), item);
    }

    public override void _UnhandledInput(InputEvent @event) {
        if (Input.IsActionJustPressed("place")) {
            captureMouse();
            placeItem(0);
        }
        if (Input.IsKeyPressed(Key.Escape)) {
            releaseMouse();
        }

        if (@event is InputEventMouseMotion env) {
            rotateLook(env.Relative);
        }
    }

    public override void _PhysicsProcess(double delta) {
        Vector3 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor()) {
            velocity += GetGravity() * (float)delta;
        }

        // Handle Jump.
        if (Input.IsActionJustPressed("jump") && IsOnFloor()) {
            velocity.Y = JumpVelocity;
        }

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay
        // actions.
        Vector2 inputDir =
            Input.GetVector("left", "right", "forward", "backward");
        Vector3 direction =
            (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y))
                .Normalized();
        if (direction != Vector3.Zero) {
            velocity.X = direction.X * Speed;
            velocity.Z = direction.Z * Speed;
        } else {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }

    private void rotateLook(Vector2 rotation) {
        lookRotation.X -= rotation.Y * lookSpeed;
        lookRotation.X =
            Math.Clamp(lookRotation.X, Mathf.DegToRad(-85), Mathf.DegToRad(85));
        lookRotation.Y -= rotation.X * lookSpeed;

        Transform3D trans = Transform;
        trans.Basis = Basis.Identity;
        Transform = trans;

        trans = head.Transform;
        trans.Basis = Basis.Identity;
        head.Transform = trans;

        RotateY(lookRotation.Y);
        head.RotateX(lookRotation.X);
    }

    private void captureMouse() {
        Input.MouseMode = Input.MouseModeEnum.Captured;
        mouseCaptured = true;
    }

    private void releaseMouse() {
        Input.MouseMode = Input.MouseModeEnum.Visible;
        mouseCaptured = false;
    }
}
