// -----------------------------------------------------------------------
// <copyright file="GraphNode.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Graph
{
    internal class GraphNode
    {
        internal GraphNode(GraphNode? parent, string value)
        {
            this.Parent = parent;
            this.Value = value;
        }

        internal List<GraphNode> Children { get; } = new();

        internal GraphNode? Parent { get; }

        internal string Value { get; }

        internal static bool IsSmallCave(string cave)
        {
            return char.IsLower(cave[0]);
        }

        // determine, based on visit history in path, whether a single visit value has already been visited
        internal static bool CanVisitSmallCave(List<string> currentPath, string value, string smallCaveAllowingTwoVisits)
        {
            if (value != smallCaveAllowingTwoVisits)
            {
                if (currentPath.Contains(value))
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
                if (currentPath.Where(x => x == smallCaveAllowingTwoVisits).Count() < 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        internal void AddChild(GraphNode child)
        {
            this.Children.Add(child);
        }

        internal void BuildGraph(List<string> currentPath, Dictionary<string, List<string>> connectedCaves, PathCounter counter, string endCave, string smallCaveAllowingTwoVisits)
        {
            if (this.Value == endCave)
            {
                // We got to end cave, add this to the list of paths
                if (smallCaveAllowingTwoVisits == string.Empty || currentPath.Count(x => x == smallCaveAllowingTwoVisits) == 2)
                {
                    counter.Increment();
                }

                return;
            }

            if (GraphNode.IsSmallCave(this.Value))
            {
                currentPath.Add(this.Value);
            }

            List<string> childValues = connectedCaves[this.Value];
            foreach (var childValue in childValues)
            {
                if (GraphNode.IsSmallCave(childValue) && !CanVisitSmallCave(currentPath, childValue, smallCaveAllowingTwoVisits))
                {
                    continue;
                }

                GraphNode newChild = new(this, childValue);
                this.AddChild(newChild);
                newChild.BuildGraph(currentPath, connectedCaves, counter, endCave, smallCaveAllowingTwoVisits);
            }

            if (GraphNode.IsSmallCave(this.Value))
            {
                currentPath.RemoveAt(currentPath.Count - 1);
            }

            return;
        }

        internal void CountNodes(string endCave, ref int visitCount)
        {
            visitCount++;

            if (this.Value == endCave)
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
