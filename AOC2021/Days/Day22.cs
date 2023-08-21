// -----------------------------------------------------------------------
// <copyright file="Day22.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using AOC2021.Data;
    using AOC2021.Models;

    internal class Day22 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        private readonly List<Cuboid> cuboids = new();

        public Day22(DataStore datastore)
        {
            this.datastore = datastore;
        }

        public override string GetDescription()
        {
            return "Reactor Reboot";
        }

        public override string Result1()
        {
            this.PrepData();

            HashSet<(int X, int Y, int Z)> onLocations = new();
            ProcessCuboids(this.cuboids.Where(c => c.InInit), onLocations);

            long result = onLocations.Count;
            return result.ToString();
        }

        public override string Result2()
        {
            List<Cuboid> masterList = new();
            Cuboid.PublishCreate = (Cuboid c) => { masterList.Add(c); };
            this.cuboids.Clear();
            this.PrepData();
            masterList = masterList.Where(c => c.InInit).ToList();

            Cuboid startCuboid = this.cuboids.First();

            List<Cuboid> processedCuboids = new() { startCuboid };
            List<Cuboid> unprocessedCuboids = this.cuboids.Skip(1).Take(19).ToList();

            while (unprocessedCuboids.Count > 0)
            {
                Cuboid unprocessed = unprocessedCuboids.First();

                var discard = processedCuboids.Select(c =>
                {
                    c.Cull(unprocessed);

                    return c;
                }).Count();

                processedCuboids = processedCuboids.Where(p => !p.Deleted).ToList();

                foreach (Cuboid p in processedCuboids)
                {
                    p.Intersect(unprocessed);
                }

                processedCuboids.Add(unprocessed);
                unprocessedCuboids = unprocessedCuboids.Skip(1).ToList();
            }

            long result = processedCuboids.Where(p => p.On && !p.Deleted).Sum(p => p.Count);
            return result.ToString();
        }

        internal static void ProcessCuboids(IEnumerable<Cuboid> cuboids, HashSet<(int X, int Y, int Z)> onLocations)
        {
            int index = 0;
            foreach (Cuboid cuboid in cuboids)
            {
                var process = (
                    from x in Enumerable.Range(cuboid.MinX, cuboid.MaxX - cuboid.MinX + 1)
                    from y in Enumerable.Range(cuboid.MinY, cuboid.MaxY - cuboid.MinY + 1)
                    from z in Enumerable.Range(cuboid.MinZ, cuboid.MaxZ - cuboid.MinZ + 1)
                    select (x, y, z)).Select(triplet =>
                    {
                        if (cuboid.On)
                        {
                            if (!onLocations.Contains(triplet))
                            {
                                onLocations.Add(triplet);
                            }
                        }
                        else
                        {
                            if (onLocations.Contains(triplet))
                            {
                                onLocations.Remove(triplet);
                            }
                        }

                        return 1;
                    });

                long discard = process.Count();
                index++;
            }
        }

        internal void PrepData()
        {
            string[] lines = this.datastore.GetRawData(this.GetName());
            foreach (string s in lines)
            {
                string line = s;

                bool on = false;
                int minX = -1;
                int maxX = -1;
                int minY = -1;
                int maxY = -1;
                int minZ = -1;
                int maxZ = -1;

                if (line.Contains("on "))
                {
                    on = true;
                    line = line.Replace("on ", string.Empty);
                }
                else if (line.Contains("off "))
                {
                    on = false;
                    line = line.Replace("off ", string.Empty);
                }

                line = line.Replace("x=", string.Empty);
                line = line.Replace("y=", string.Empty);
                line = line.Replace("z=", string.Empty);

                string[] parts = line.Split(',');

                for (int i = 0; i < parts.Length; i++)
                {
                    string[] minMax = parts[i].Split("..");
                    if (i == 0)
                    {
                        (minX, maxX) = (int.Parse(minMax[0]), int.Parse(minMax[1]));
                    }
                    else if (i == 1)
                    {
                        (minY, maxY) = (int.Parse(minMax[0]), int.Parse(minMax[1]));
                    }
                    else
                    {
                        (minZ, maxZ) = (int.Parse(minMax[0]), int.Parse(minMax[1]));
                    }
                }

                List<int> allCoords = new() { minX, maxX, minY, maxY, minZ, maxZ };
                bool inInit = allCoords.All(x => Math.Abs(x) <= 50);
                this.cuboids.Add(Cuboid.GetCuboid(on, minX, maxX, minY, maxY, minZ, maxZ, inInit, "primary"));
            }
        }
    }
}
