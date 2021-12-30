// -----------------------------------------------------------------------
// <copyright file="Day17.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using System.Text.RegularExpressions;
    using AOC2021.Data;

    public class Day17 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        private int minX;

        private int maxX;

        private int minY;

        private int maxY;

        private (int YVelocity, int MaxY)[]? yVelocitiesAndMaxYPositions;

        public Day17(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public int MinX => this.minX;

        public int MaxX => this.maxX;

        public int MinY => this.minY;

        public int MaxY => this.maxY;

        public (int YVelocity, int MaxY)[] YVelocitiesAndMaxYPositions
        {
            get
            {
                if (this.yVelocitiesAndMaxYPositions == null)
                {
                    throw new InvalidOperationException("Accessing YVelocitiesAndMaxYPositions before initialized.");
                }

                return this.yVelocitiesAndMaxYPositions;
            }
        }

        public override string GetDescription()
        {
            return "Trajectories";
        }

        public override string Result1()
        {
            this.PrepData();

            int bestXVelocity = this.GetXVelocityThatMaximizesTurns();
            this.yVelocitiesAndMaxYPositions = this.GetYVelocitiesAndMaxYPositions(bestXVelocity);

            int maxY = this.YVelocitiesAndMaxYPositions.OrderByDescending(y => y.MaxY).First().MaxY;

            long result = maxY;
            return result.ToString();
        }

        public override string Result2()
        {
            int maximumYVelocity = this.YVelocitiesAndMaxYPositions.OrderByDescending(y => y.YVelocity).First().YVelocity;

            int goodCombos = 0;

            List<(int X, int Y)> velocityPairs = new();
            foreach (int x in Enumerable.Range(0, this.MaxX + 1))
            {
                foreach (int y in Enumerable.Range(this.MinY, Math.Abs(this.MinY) + maximumYVelocity + 1))
                {
                    velocityPairs.Add((x, y));
                }
            }

            var discard = velocityPairs.Select(pair =>
            {
                int currentXVelocity = pair.X;
                int currentYVelocity = pair.Y;
                int xPosition = 0;
                int yPosition = 0;
                int turn = 0;
                bool doCalculation = true;
                bool result = false;

                while (doCalculation)
                {
                    ((currentXVelocity, currentYVelocity), (xPosition, yPosition), turn) = DoTurn(currentXVelocity, currentYVelocity, xPosition, yPosition, turn);
                    if (xPosition > this.MaxX || yPosition < this.MinY)
                    {
                        doCalculation = false;
                    }

                    if (xPosition >= this.MinX && xPosition <= this.MaxX && yPosition <= this.MaxY && yPosition >= this.MinY)
                    {
                        doCalculation = false;
                        result = true;
                        goodCombos++;
                    }
                }

                return ((pair.X, pair.Y), result);
            }).ToArray().Where(x => x.result).ToArray(); // ToArray simply ensures we push through the lazy evaluation of IEnumerable

            long result = goodCombos;
            return result.ToString();
        }

        internal static ((int XVelocity, int YVelocity) Velocity, (int XPosition, int YPosition) Position, int Turn) DoTurn(int xVelocity, int yVelocity, int xPosition, int yPosition, int turn)
        {
            turn++;
            xPosition += xVelocity;
            yPosition += yVelocity;
            if (xVelocity > 0)
            {
                xVelocity--;
            }

            yVelocity--;
            return ((xVelocity, yVelocity), (xPosition, yPosition), turn);
        }

        internal int GetXVelocityThatMaximizesTurns()
        {
            int bestXVelocity = Enumerable.Range(0, this.MaxX + 1).Select(xVelocity =>
            {
                int currentXVelocity = xVelocity;
                int currentYVelocity = 0;
                int xPosition = 0;
                int yPosition = 0;
                int turn = 0;
                bool doCalculation = true;
                int maxGoodTurn = -1;

                while (doCalculation)
                {
                    ((currentXVelocity, currentYVelocity), (xPosition, yPosition), turn) = DoTurn(currentXVelocity, currentYVelocity, xPosition, yPosition, turn);
                    if (xPosition >= this.MinX && xPosition <= this.MaxX)
                    {
                        if (turn > maxGoodTurn)
                        {
                            maxGoodTurn = turn;
                        }
                    }

                    if (xPosition > this.MaxX || currentXVelocity <= 0)
                    {
                        doCalculation = false;
                    }
                }

                return (xVelocity, maxGoodTurn);
            }).OrderByDescending(result => result.maxGoodTurn).Where(pair => pair.maxGoodTurn > 0).First().xVelocity;

            return bestXVelocity;
        }

        internal (int YVelocity, int MaxY)[] GetYVelocitiesAndMaxYPositions(int bestXVelocity)
        {
            var results = Enumerable.Range(0, 1000).Select(yVelocity =>
            {
                int currentXVelocity = bestXVelocity;
                int currentYVelocity = yVelocity;
                int xPosition = 0;
                int yPosition = 0;
                int turn = 0;
                bool doCalculation = true;
                bool result = false;
                int maxY = -1;

                while (doCalculation)
                {
                    ((currentXVelocity, currentYVelocity), (xPosition, yPosition), turn) = DoTurn(currentXVelocity, currentYVelocity, xPosition, yPosition, turn);
                    if (yPosition > maxY)
                    {
                        maxY = yPosition;
                    }

                    if (this.MaxY >= yPosition && yPosition >= this.MinY && this.MinX <= xPosition && xPosition <= this.MaxX)
                    {
                        result = true;
                        doCalculation = false;
                    }

                    if (yPosition < this.MinY || xPosition > this.MaxX)
                    {
                        doCalculation = false;
                    }
                }

                return (YVelocity: yVelocity, MaxY: result ? maxY : -1);
            }).OrderByDescending(pair => pair.MaxY).Where(pair => pair.MaxY != -1).ToArray();

            return results;
        }

        internal void PrepData()
        {
            string[] lines = this.datastore.GetRawData(this.GetName());

            string line = lines[0];

            string pattern = @"(-?\d+)";
            var matches = Regex.Matches(line, pattern);
            this.minX = int.Parse(matches[0].Value);
            this.maxX = int.Parse(matches[1].Value);
            this.minY = int.Parse(matches[2].Value);
            this.maxY = int.Parse(matches[3].Value);
        }
    }
}
