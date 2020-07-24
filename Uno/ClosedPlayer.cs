namespace Uno
{
    using System;
    using System.Collections.Generic;

    public class UnoClientGame
    {
        public ClosedPlayer[] Opponents { get; }
        public OpenPlayer Me { get; set; }
    }

    public class UnoServerGame
    {
        public OpenPlayer[] Players { get; }

        public List<ICard> Cards { get; }

    }

    public class Player
    {
        public Guid Guid { get; }
        public string Name { get; }

        public Player(Guid guid, string name)
        {
            Guid = guid;
            Name = name;
        }
    }

    public class ClosedPlayer : Player
    {
        public int CardCount { get; set; }

        public ClosedPlayer(Guid guid, string name, int cardCount) : base(guid, name)
        {
            CardCount = cardCount;
        }
    }

    public class OpenPlayer : Player
    {
        public List<ICard> Cards { get; }

        public OpenPlayer(Guid guid, string name, List<ICard> cards) : base(guid, name)
        {
            Cards = cards;
        }
    }

    public interface ICard
    {
        string DisplayName { get; }

        bool Matches(ICard card);
    }

    public interface IColoredCard : ICard
    {
        CardColor Color { get; }
    }

    public interface IActionCard : ICard
    {
        ICardAction GetAction(ICard card, ICardAction? previousAction);
    }

    public interface ICardAction
    {
        Dictionary<ICard, Player> CardMovement { get; }
        bool ActImmediately { get; }
    }

    public class NumberCard : IColoredCard
    {
        public int Number { get; }
        public CardColor Color { get; }

        public string DisplayName => $"{Color} {Number}";

        public bool Matches(ICard card) => card is NumberCard numberCard
            ? Number == numberCard.Number || Color == numberCard.Color
            : card.Matches(this);
    }

    public class BlackCard : IActionCard
    {
        public BlackCardType CardType { get; }

        public string DisplayName => $"{CardType}";

        public bool Matches(ICard card) => true;
    }

    public readonly struct DrawAction : ICardAction
    {
        public readonly int Amount;

        public DrawAction(int amount) => Amount = amount;

        public bool ActImmediately => false;
    }

    public enum BlackCardType
    {
        None,
        DrawFour,
    }

    public enum CardColor
    {
        Red,
        Green,
        Blue,
        Yellow
    }
}
