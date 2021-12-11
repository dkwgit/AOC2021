// -----------------------------------------------------------------------
// <copyright file="Cell.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models
{
    using System.Reactive.Subjects;

    internal enum UpdateType
    {
        Turn,
        FlashProcessing,
        FlashFromNeighbor,
    }

    internal class Cell : ISubject<UpdateType, UpdateType>
    {
        public Cell(int x, int y, int value) => (this.X, this.Y, this.Value) = (x, y, value);

        public int X { get; init; }

        public int Y { get; init; }

        public int Value { get; set; }

        public bool FlashedThisTurn { get; set; } = false;

        public List<IObserver<UpdateType>> Observers { get; } = new List<IObserver<UpdateType>>();

        public void ProcessTurn()
        {
            this.OnNext(UpdateType.Turn);
        }

        public void ProcessForFlashes()
        {
            this.OnNext(UpdateType.FlashProcessing);
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public IDisposable Subscribe(IObserver<UpdateType> observer)
        {
            this.Observers.Add(observer);
            return null!;
        }

        public void OnNext(UpdateType update)
        {
            if (update == UpdateType.Turn)
            {
                this.FlashedThisTurn = false;
                if (this.Value >= 10)
                {
                    this.Value = 0;
                }

                this.Value++;
            }

            if (update == UpdateType.FlashFromNeighbor)
            {
                this.Value++;
            }

            if ((update == UpdateType.FlashProcessing || update == UpdateType.FlashFromNeighbor) && !this.FlashedThisTurn)
            {
                if (this.Value >= 10)
                {
                    this.FlashedThisTurn = true;
                    foreach (var observer in this.Observers)
                    {
                        observer.OnNext(UpdateType.FlashFromNeighbor);
                    }
                }
            }
        }
    }
}
