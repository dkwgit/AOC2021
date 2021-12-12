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
        internal GraphNode(GraphNode<T>? parent, T value, bool singleVisitNode)
        {
            this.Parent = parent;
            this.SingleVisitNode = singleVisitNode;
            this.Value = value;
        }

        internal List<GraphNode<T>> Children { get; } = new();

        internal GraphNode<T>? Parent { get; }

        internal bool SingleVisitNode { get; }

        internal T Value { get; }

        internal bool IsStart
        {
            get
            {
                if (this.Parent == null)
                {
                    return true;
                }

                return false;
            }
        }

        internal bool IsEnd
        {
            get
            {
                if (this.Children.Count > 0)
                {
                    return true;
                }

                return false;
            }
        }

        // determine, based on visit history in path, whether a single visit value has already been visited
        internal static bool CanVisitSmallCave(List<T> visitPath, T value, T smallCaveWithTwoVisits)
        {
            if (!value.Equals(smallCaveWithTwoVisits))
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
                if (visitPath.Where(x => x.Equals(smallCaveWithTwoVisits)).Count() < 2)
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

        internal void BuildGraph(List<T> currentPath, Dictionary<T, List<T>> productionRules, List<List<T>> pathList, T leafValue, Func<T, bool> isSingleVisitValue, T smallCaveWithTwoVisits)
        {
            currentPath.Add(this.Value);
            if (this.Value.Equals(leafValue))
            {
                pathList.Add(new List<T>(currentPath));
                currentPath.RemoveAt(currentPath.Count - 1);
                return;
            }

            List<T> childValues = productionRules[this.Value];
            foreach (var childValue in childValues)
            {
                if (isSingleVisitValue(childValue) && !CanVisitSmallCave(currentPath, childValue, smallCaveWithTwoVisits))
                {
                    continue;
                }

                GraphNode<T> newChild = new(this, childValue, isSingleVisitValue(childValue));
                this.AddChild(newChild);
                newChild.BuildGraph(currentPath, productionRules, pathList, leafValue, isSingleVisitValue, smallCaveWithTwoVisits);
            }

            currentPath.RemoveAt(currentPath.Count - 1);
            return;
        }
    }
}
