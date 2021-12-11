// -----------------------------------------------------------------------
// <copyright file="FlashCounter.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models
{
    internal class FlashCounter : IObserver<UpdateType>
    {
        public int Flashes { get; set; } = 0;

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(UpdateType update)
        {
            if (update == UpdateType.FlashFromNeighbor)
            {
                this.Flashes++;
            }
        }
    }
}
