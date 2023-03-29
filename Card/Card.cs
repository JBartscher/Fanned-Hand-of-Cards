using Godot;
using System;

public partial class Card : Node2D
{

    public Label Title = null;
    public Sprite2D BackgroundSprite = null;
    public float Ratio = 0.0f;

    public override void _Ready()
    {
        Title = GetNode<Label>("Label");
        BackgroundSprite = GetNode<Sprite2D>("background");
    }
}
