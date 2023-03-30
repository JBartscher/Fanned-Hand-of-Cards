using Godot;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public partial class Hand : Node2D
{
    [ExportCategory("Card Scene")] [Export]
    private PackedScene _cardScene;

    [ExportCategory("X Position")] [Export]
    private Curve _XPositionCurve;

    [Export(PropertyHint.Range, "0,500.0,0.1")]
    private float _handWidth = 250.0f;

    [ExportCategory("Y Position")] [Export]
    private Curve _YPositionCurve;

    [Export(PropertyHint.Range, "-50,50.0,0.1")]
    private float _handHeight = -50.0f;

    [ExportCategory("Card Rotation")] [Export]
    private Curve _rotationPositionCurve;
    
    [Export(PropertyHint.Range, "-50,50.0,0.1")]
    private float _rotationDegree = -15.0f;

    public readonly List<Card> CardsInHand = new List<Card>();

    
    public override void _Ready()
    {
        base._Ready();
        for (int i = 0; i < 5; i++)
        {
            AddCard();
        }
    }
    
    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_accept"))
        {
            GD.Print("ui_accept occurred!");
            AddCard();
        }
        if (@event.IsActionPressed("ui_cancel"))
        {
            GD.Print("ui_cancel occurred!");
            RemoveCard();
        }
    }

    public void AddCard()
    {
        var newCard = _cardScene.Instantiate() as Card;
        this.AddChild(newCard);
        newCard.GlobalPosition = GlobalPosition;
        CardsInHand.Add(newCard);
        CalculateHandRatio();
    }

    public void RemoveCard()
    {
        var cardToRemove = CardsInHand.Last();
        CardsInHand.Remove(cardToRemove);
        cardToRemove.QueueFree();
        CalculateHandRatio();
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
        GD.Print("------------------");
        
        foreach (var it in CardsInHand.Select((c, i) => new { Card = c, Index = i }))
        {
            var ratioInHand = (it.Index / (float)(CardsInHand.Count - 1));
            it.Card.Ratio = ratioInHand;
            it.Card.Title.Text = ratioInHand.ToString(CultureInfo.InvariantCulture);

            var txP = _XPositionCurve.Sample(ratioInHand) * _handWidth;
            var tyP = _YPositionCurve.Sample(ratioInHand) * _handHeight;
            var tr = _rotationPositionCurve.Sample(ratioInHand) * _rotationDegree;

            var cardPosition = GlobalPosition;
            // cardPosition.X += txP;
            // cardPosition.Y += tyP;
            // it.Card.GlobalPosition = cardPosition;
            Transform2D rotationTransform = new Transform2D(Mathf.DegToRad(tr), new Vector2( cardPosition.X + txP,  cardPosition.Y + tyP));
            it.Card.Transform = rotationTransform; //.InterpolateWith(rotationTransform, 15.0f);
            
           
            GD.Print($"x:{txP} y:{tyP} r:{tr}");
        }
    }
}