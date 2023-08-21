// -----------------------------------------------------------------------
// <copyright file="Cuboid.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models
{
    using FluentAssertions;

    internal record class Cuboid(int Id, bool On, int MinX, int MaxX, int MinY, int MaxY, int MinZ, int MaxZ, bool InInit, string Type)
    {
        public static Action<Cuboid>? PublishCreate = null;

        private static int NextId = 1;

        private long? count = null;

        internal long Count
        {
            get
            {
                if (!this.count.HasValue)
                {
                    this.count = ((long)(Math.Abs(this.MaxX - this.MinX) + 1)) * ((long)(Math.Abs(this.MaxY - this.MinY) + 1)) * ((long)(Math.Abs(this.MaxZ - this.MinZ) + 1));
                }

                return this.count.Value;
            }

            set
            {
                this.count = value;
            }
        }

        internal Cuboid? Parent1 { get; set; } = null;

        internal Cuboid? Parent2 { get; set; } = null;

        internal bool Deleted { get; set; } = false;

        internal List<Cuboid> Children { get; } = new();

        internal static Cuboid GetCuboid(bool on, int minX, int maxX, int minY, int maxY, int minZ, int maxZ, bool inInit, string type = "primary")
        {
            int id = Cuboid.NextId++;
            Cuboid newCuboid = new(id, on, minX, maxX, minY, maxY, minZ, maxZ, inInit, type);

            if (PublishCreate is not null)
            {
                PublishCreate(newCuboid);
            }

            return newCuboid;
        }

        internal void Intersect(Cuboid other)
        {
            int? minX = null;
            int? maxX = null;
            int? minY = null;
            int? maxY = null;
            int? minZ = null;
            int? maxZ = null;

            if ((other.MinX >= this.MinX && other.MinX <= this.MaxX) || (other.MaxX >= this.MinX && other.MaxX <= this.MaxX))
            {
                minX = (other.MinX >= this.MinX && other.MinX <= this.MaxX) ? other.MinX : this.MinX;
                maxX = (other.MaxX >= this.MinX && other.MaxX <= this.MaxX) ? other.MaxX : this.MaxX;
            }

            if ((other.MinY >= this.MinY && other.MinY <= this.MaxY) || (other.MaxY >= this.MinY && other.MaxY <= this.MaxY))
            {
                minY = (other.MinY >= this.MinY && other.MinY <= this.MaxY) ? other.MinY : this.MinY;
                maxY = (other.MaxY >= this.MinY && other.MaxY <= this.MaxY) ? other.MaxY : this.MaxY;
            }

            if ((other.MinZ >= this.MinZ && other.MinZ <= this.MaxZ) || (other.MaxZ >= this.MinZ && other.MaxZ <= this.MaxZ))
            {
                minZ = (other.MinZ >= this.MinZ && other.MinZ <= this.MaxZ) ? other.MinZ : this.MinZ;
                maxZ = (other.MaxZ >= this.MinZ && other.MaxZ <= this.MaxZ) ? other.MaxZ : this.MaxZ;
            }

            List<int?> allCoords = new() { minX, maxX, minY, maxY, minZ, maxZ };
            bool allCoordsSet = allCoords.All(c => c.HasValue);

            if (allCoordsSet)
            {
                bool inInit = allCoords.All(x => Math.Abs(x!.Value) <= 50);
                Cuboid intersection = Cuboid.GetCuboid(other.On, minX!.Value, maxX!.Value, minY!.Value, maxY!.Value, minZ!.Value, maxZ!.Value, inInit, "intersection");

                intersection.Parent1 = this;
                intersection.Parent2 = other;

                this.Children.Add(intersection);
                other.Children.Add(intersection);

                this.Count -= intersection.Count;
                other.Count -= intersection.Count;
            }
        }

        internal bool Contains(Cuboid other)
        {
            if (this.MinX <= other.MinX && this.MaxX >= other.MaxX && this.MinY <= other.MinY && this.MaxY >= other.MaxY && this.MinZ <= other.MinZ && this.MaxZ >= other.MaxZ)
            {
                return true;
            }

            return false;
        }

        internal void Cull(Cuboid other)
        {
            foreach (Cuboid child in this.Children)
            {
                child.Cull(other);
            }

            if (other.Contains(this))
            {
                this.Delete();
            }

            if (this.Count <= 0)
            {
                this.Delete();
            }
        }

        internal void Delete()
        {
            foreach (Cuboid child in this.Children)
            {
                child.Delete();
            }

            this.Children.Clear();

            this.Deleted = true;
            if (this.Parent1 != null)
            {
                this.Parent1 = null;
            }

            if (this.Parent2 != null)
            {
                this.Parent1 = null;
            }
        }
    }
}
