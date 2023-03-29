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
    private Curve _XPositionCurve;

    [Export(PropertyHint.Range, "0,50.0,0.1")]
    private float _handWidth = 250.0f;

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
        this.AddChild(newCard);
        newCard.GlobalPosition = GlobalPosition;
        GD.Print(newCard.GlobalPosition);
        CardsInHand.Add(newCard);
    }

    private void CalculateHandRatio()
    {
        if (!CardsInHand.Any())
        {
            return;
        }
        if (CardsInHand.Count == 1)
        {
            var ratioInHand = 0.5f;
            CardsInHand.First().Ratio = ratioInHand;
            CardsInHand.First().Title.Text = ratioInHand.ToString();
            return;
        }
        GD.Print("---------------------");
        
        foreach (var it in CardsInHand.Select((c, i) => new { Card = c, Index = i }))
        {
            var ratioInHand = (it.Index / (float)(CardsInHand.Count -1));
            it.Card.Ratio = ratioInHand;
            it.Card.Title.Text = ratioInHand.ToString();

            // Vector2 cardPosition = it.Card.GlobalPosition;
            // GD.Print(cardPosition);
            // cardPosition.X = cardPosition.X + (_XPositionCurve.Sample(ratioInHand) * _handWidth);
           

            var t = _XPositionCurve.Sample(ratioInHand);
            var tP = _XPositionCurve.Sample(ratioInHand) * _handWidth;
           
            var cardPosition = GlobalPosition;
            cardPosition.X += tP;
            it.Card.GlobalPosition = cardPosition;
            GD.Print(t + " " + tP+ " " + cardPosition);
        }
    }

    private void Foo()
    {
    }
}