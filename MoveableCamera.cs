using Godot;
using System;

public partial class MoveableCamera : Camera2D
{
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        // Translate(GetMovementVector());
        Vector2 direction = GetMovementVector();
        var pos = GlobalPosition;
        pos.X += direction.X * 10;
        pos.Y += direction.Y* 10;


        GlobalPosition = pos;
    }

    private Vector2 GetMovementVector()
    {
        Vector2 direction = Vector2.Zero;

        // As good practice, you should replace UI actions with custom gameplay actions.
        direction.X = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        direction.Y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");

        return direction.Normalized(); // normalize?
    }
}