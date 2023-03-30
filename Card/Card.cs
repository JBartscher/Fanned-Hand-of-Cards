using Godot;
using System;

public partial class Card : Node2D
{
    public Label Title = null;
    public Sprite2D FrontSprite = null;
    public Sprite2D BackSprite = null;
    public float Ratio = 0.0f;

    public override void _Ready()
    {
        Title = GetNode<Label>("Label");
        FrontSprite = GetNode<Sprite2D>("Front");
        BackSprite = GetNode<Sprite2D>("Back");
    }
}