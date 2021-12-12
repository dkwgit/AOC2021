// -----------------------------------------------------------------------
// <copyright file="GraphNode.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Graph
{
    internal class GraphNode<T>
        where T : notnull
    {
        internal GraphNode(GraphNode<T>? parent, T value)
        {
            this.Parent = parent;
            this.Value = value;
        }

        internal List<GraphNode<T>> Children { get; } = new();

        internal GraphNode<T>? Parent { get; }

        internal T Value { get; }

        // determine, based on visit history in path, whether a single visit value has already been visited
        internal static bool CanVisitSmallCave(List<T> visitPath, T value, T smallCaveAllowingTwoVisits)
        {
            if (!value.Equals(smallCaveAllowingTwoVisits))
            {
                if (visitPath.Contains(value))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (visitPath.Where(x => x.Equals(smallCaveAllowingTwoVisits)).Count() < 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        internal void AddChild(GraphNode<T> child)
        {
            this.Children.Add(child);
        }

        internal void BuildGraph(List<T> currentPath, Dictionary<T, List<T>> connectedCaves, List<List<T>> completePathList, T endCave, Func<T, bool> isSmallCave, T smallCaveAllowingTwoVisits)
        {
            currentPath.Add(this.Value);
            if (this.Value.Equals(endCave))
            {
                // We got to end cave, add this to the list of paths
                completePathList.Add(new List<T>(currentPath));
                currentPath.RemoveAt(currentPath.Count - 1);
                return;
            }

            List<T> childValues = connectedCaves[this.Value];
            foreach (var childValue in childValues)
            {
                if (isSmallCave(childValue) && !CanVisitSmallCave(currentPath, childValue, smallCaveAllowingTwoVisits))
                {
                    continue;
                }

                GraphNode<T> newChild = new(this, childValue);
                this.AddChild(newChild);
                newChild.BuildGraph(currentPath, connectedCaves, completePathList, endCave, isSmallCave, smallCaveAllowingTwoVisits);
            }

            currentPath.RemoveAt(currentPath.Count - 1);
            return;
        }

        internal void CountNodes(T endCave, ref int visitCount)
        {
            visitCount++;

            if (this.Value.Equals(endCave))
            {
                return;
            }

            foreach (var child in this.Children)
            {
                child.CountNodes(endCave, ref visitCount);
            }

            return;
        }
    }
}
