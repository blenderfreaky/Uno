namespace Uno
{
    using System;

    public class UnoGame
    {
        public UnoPlayer[] Players { get; }
    }

    public class UnoPlayer
    {
        public string Name { get; }
        public int CardCount { get; set; }

        public UnoPlayer(string name, int cardCount)
        {
            Name = name;
            CardCount = cardCount;
        }
    }

    public class OpenUnoPlayer : UnoPlayer
    {
        public ICard[] Cards { get; }

        public OpenUnoPlayer(string name, int cardCount, ICard[] cards) : base(name, cardCount)
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
        void ActOn(UnoGame game);
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

    public class BlackCard : ICard
    {
        public BlackCardType CardType { get; }

        public string DisplayName => $"{CardType}";

        public bool Matches(ICard card) => true;
    }

    public enum BlackCardType
    {
        ChooseColor,
        ChooseColorDrawFour,
    }

    public enum CardColor
    {
        Red,
        Green,
        Blue,
        Yellow
    }
}
