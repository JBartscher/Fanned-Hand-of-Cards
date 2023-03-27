using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public partial class Hand : Node2D
{
    [ExportCategory("Card Scene")] [Export]
    private PackedScene _cardScene;

    [ExportCategory("X Position")] [Export]
    private Curve XPositionCurve;

    [Export(PropertyHint.Range, "0,50.0,0.1")]
    private float HandWidth = 20.0f;

    public readonly List<Card> CardsInHand = new List<Card>();


    public override void _Ready()
    {
        base._Ready();
        for (int i = 0; i < 5; i++)
        {
            AddCard();
            CalculateHandRatio();
        }
    }

    public void AddCard()
    {
        var newCard = _cardScene.Instantiate() as Card;
        AddChild(newCard);
        CardsInHand.Add(newCard);
    }

    private void CalculateHandRatio()
    {
        GD.Print("CalculateHandRatio");
        foreach (var it in CardsInHand.Select((c, i) => new { Card = c, Index = i }))
        {
            var ratioInHand = ((float)it.Index / (float)CardsInHand.Count);
            GD.Print(ratioInHand);
            it.Card.Ratio = ratioInHand;
            it.Card.GlobalPosition += Vector2.Right * (XPositionCurve.Sample(ratioInHand) * HandWidth);
        }
    }

    private void Foo()
    {
    }
}