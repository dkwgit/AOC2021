// -----------------------------------------------------------------------
// <copyright file="Day19.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Days
{
    using System.Text.RegularExpressions;
    using AOC2021.Data;
    using AOC2021.Models.Scanners;
    using FluentAssertions;
    using MathNet.Spatial.Euclidean;

    public class Day19 : BaseDay, IDay
    {
        private readonly DataStore datastore;

        private List<Scanner>? scanners;

        public Day19(DataStore datastore)
        {
            this.datastore = datastore;
        }

        internal List<Scanner> Scanners
        {
            get
            {
                if (this.scanners == null)
                {
                    throw new InvalidOperationException("Access to Scanners property before initialized.");
                }

                return this.scanners;
            }
        }

        public override string GetDescription()
        {
            return "Scanner/beacon mapping";
        }

        public override string Result1()
        {
            /*
             * Basic approach is to use the distances between beacons as a way of detected overlapped sensors.
             * It wasn't clear this would work? What is beacons have a lot of identical distances between themselves.
             * Fortunately, they almost never do.
             *
             * I translate the requirement of amount of shared beacons to find relate sensors to an amount of shared distances between beacons
             * to find related sensors.
             *
             * I handle translation of differing coordinate systems by matching two point pairs between the two coordinate systems,
             * which supplies enough to adjust the x/y/z orientation and then calculation a translation vector.
             */
            this.PrepData();

            Scanner referenceScanner = this.Scanners[0];

            // Every scanner that makes it into here has had its coordinate system re-expressed in terms of scanner 0
            List<Scanner> relatedScanners = new() { referenceScanner };
            List<Scanner> scannersNeedingToBeRelated = new(this.Scanners.Skip(1));

            int sourceScannerIndex = 0;
            while (relatedScanners.Count < this.Scanners.Count)
            {
                Scanner sourceScanner = relatedScanners[sourceScannerIndex++];
                var intersections = GetScannersWithOverlappedPointDistances(sourceScanner, scannersNeedingToBeRelated);
                foreach (var item in intersections)
                {
                    Scanner relatedScanner = TranslateCoordinatesOfOverlappedScannerToSourceScannerSystem(sourceScanner, item);
                    relatedScanners.Add(relatedScanner);
                    scannersNeedingToBeRelated = scannersNeedingToBeRelated.Where(s => s.Id != relatedScanner.Id).ToList();
                }
            }

            // For part 2
            this.scanners = relatedScanners;

            HashSet<Point3D> beacons = new();
            foreach (Scanner s in relatedScanners)
            {
                foreach (var point in s.Points)
                {
                    beacons.Add(point);
                }
            }

            long result = beacons.Count;
            return result.ToString();
        }

        public override string Result2()
        {
            Scanner current = this.Scanners[0];
            List<Scanner> scannersToCompareForManhattanDistance = new(this.Scanners.Skip(1));
            int largestManhattanDistance = int.MinValue;

            while (scannersToCompareForManhattanDistance.Count > 0)
            {
                foreach (Scanner s in scannersToCompareForManhattanDistance)
                {
                    double distance = Math.Abs(current.Origin.X - s.Origin.X) + Math.Abs(current.Origin.Y - s.Origin.Y) + Math.Abs(current.Origin.Z - s.Origin.Z);
                    if ((int)distance > largestManhattanDistance)
                    {
                        largestManhattanDistance = (int)distance;
                    }
                }

                current = scannersToCompareForManhattanDistance[0];
                scannersToCompareForManhattanDistance.RemoveAt(0);
            }

            long result = largestManhattanDistance;
            return result.ToString();
        }

        internal void PrepData()
        {
            string[] lines = this.datastore.GetRawData(this.GetName());

            string state = "initial";
            int scannerId = -1;
            List<Point3D> points = new();
            List<Scanner> scanners = new();
            foreach (string line in lines)
            {
                if (line == string.Empty)
                {
                    state = "initial";
                    scanners.Add(new Scanner(scannerId, points.ToArray()));
                    scannerId = -1;
                    points.Clear();
                }

                if (state == "points")
                {
                    string[] strings = line.Split(",");
                    double[] coordinates = strings.Select(s => double.Parse(s)).ToArray();
                    Point3D point = new(coordinates[0], coordinates[1], coordinates[2]);
                    points.Add(point);
                }

                if (state == "initial" && line.Contains("scanner"))
                {
                    string pattern = @"(\d+)";
                    Match m = Regex.Match(line, pattern);
                    if (m.Success)
                    {
                        scannerId = int.Parse(m.Groups[1].Value);
                    }

                    state = "points";
                }
            }

            scanners.Add(new Scanner(scannerId, points.ToArray()));

            this.scanners = scanners.Select(s =>
            {
                s.ProcessDistances();
                return s;
            }).ToList();
        }

        private static Scanner TranslateCoordinatesOfOverlappedScannerToSourceScannerSystem(Scanner source, (Scanner S, List<double> OverlappedDistances) scannerToTranslate)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            CoordinateTranslationHelper helper = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            (Scanner target, List<double> distances) = scannerToTranslate;
            Scanner relatedScanner = new(target.Id);

            Dictionary<Point3D, SortedSet<double>> sourcepointsToDistances = new();
            Dictionary<Point3D, SortedSet<double>> targetpointsToDistances = new();

            var dictionaryInsertionHandler = (Point3D[] pointArray, double distance, Dictionary<Point3D, SortedSet<double>> dictionary) =>
            {
                foreach (var point in pointArray)
                {
                    if (!dictionary.ContainsKey(point))
                    {
                        dictionary[point] = new SortedSet<double>();
                    }

                    dictionary[point].Add(distance);
                }
            };

            // For each participating source and target point (participating in the overlapped distance), get all the distances it shares with other points, sorted.
            foreach (var distance in distances)
            {
                (Point3D sourcePoint1, Point3D sourcePoint2, Line3D _) = source.DistancesToPoints[distance][0];
                Point3D[] sourcePoints = new Point3D[2] { sourcePoint1, sourcePoint2 };
                dictionaryInsertionHandler(sourcePoints, distance, sourcepointsToDistances);
                (Point3D targetPoint1, Point3D targetPoint2, Line3D _) = target.DistancesToPoints[distance][0];
                Point3D[] targetPoints = new Point3D[2] { targetPoint1, targetPoint2 };
                dictionaryInsertionHandler(targetPoints, distance, targetpointsToDistances);
            }

            List<(Point3D SourcePoint, Point3D TargetPoint)> sourcePointsToTargetPoints = new();
            foreach ((Point3D sourcePoint, SortedSet<double> sortedDistances) in sourcepointsToDistances)
            {
                // Each point is uniquely identified by the Max and min distances in which it participates
                double max = sortedDistances.Max();
                double min = sortedDistances.Min();

                Point3D targetPoint = targetpointsToDistances.Where(kvPair => kvPair.Value.Max() == max && kvPair.Value.Min() == min).Select(kvPair => kvPair.Key).First();
                sourcePointsToTargetPoints.Add((sourcePoint, targetPoint));
                if (sourcePointsToTargetPoints.Count >= 2)
                {
                    // Only need two point pairs for the translation/adjustment counts
                    break;
                }
            }

            // Now we have a list of source point to target point combos, so can go about translating the coordinate system of the target to the source system
            foreach (List<(Point3D SourcePoint, Point3D TargetPoint)> list in sourcePointsToTargetPoints.ProduceWindows(2))
            {
                double sourceXComponent = list[1].SourcePoint.X - list[0].SourcePoint.X;
                double sourceYComponent = list[1].SourcePoint.Y - list[0].SourcePoint.Y;
                double sourceZComponent = list[1].SourcePoint.Z - list[0].SourcePoint.Z;

                // Assumption: the puzzle does not have situation X diff or Y diff  or Z diff between points is the same.
                // Glad it held. It was so much easier do the adjustment logic as a result.
                sourceXComponent.Should().NotBe(sourceYComponent);
                sourceXComponent.Should().NotBe(sourceZComponent);
                sourceYComponent.Should().NotBe(sourceZComponent);

                double targetXComponent = list[1].TargetPoint.X - list[0].TargetPoint.X;
                double targetYComponent = list[1].TargetPoint.Y - list[0].TargetPoint.Y;
                double targetZComponent = list[1].TargetPoint.Z - list[0].TargetPoint.Z;

                // The helper will be able to express things in terms of source X or Y or Z, regardless of how X or Y or Z are assigned in the orientation of the target.
                helper = GetTranslationHelper(sourceXComponent, sourceYComponent, sourceZComponent, targetXComponent, targetYComponent, targetZComponent);

                double targetXComponentAdjusted = helper.SetPoint(list[1].TargetPoint).X - helper.SetPoint(list[0].TargetPoint).X;
                double targetYComponentAdjusted = helper.SetPoint(list[1].TargetPoint).Y - helper.SetPoint(list[0].TargetPoint).Y;
                double targetZComponentAdjusted = helper.SetPoint(list[1].TargetPoint).Z - helper.SetPoint(list[0].TargetPoint).Z;

                targetXComponentAdjusted.Should().Be(sourceXComponent);
                targetYComponentAdjusted.Should().Be(sourceYComponent);
                targetZComponentAdjusted.Should().Be(sourceZComponent);

                helper.SetPoint(list[0].TargetPoint);
                Vector3D translationVector = new(
                    list[0].SourcePoint.X - helper.X,
                    list[0].SourcePoint.Y - helper.Y,
                    list[0].SourcePoint.Z - helper.Z
                );

                // One we set the vector, retrieving X or Y or Z means that not only are we adjusting selection of X, Y, and Z in terms of the source,
                // but we are also expressing the values of points in terms of the source.
                helper.TranslationVector = translationVector;

                double targetXWithTranslation = helper.X;
                double targetYWithTranslation = helper.Y;
                double targetZWithTranslation = helper.Z;

                targetXWithTranslation.Should().Be(list[0].SourcePoint.X);
                targetYWithTranslation.Should().Be(list[0].SourcePoint.Y);
                targetZWithTranslation.Should().Be(list[0].SourcePoint.Z);

                // I ran this across the full collection a few times to checke that all was well.
                // Yet only one pair of shared points is needed (the first window) to get the translation/adjustment calculation.
                // Hence the break;
                break;
            }

            // Get a source corrected origin of the newly related scanner.
            helper!.SetPoint(target.Origin);
            relatedScanner.Origin = new Point3D(helper.X, helper.Y, helper.Z);

            // Translate all the points to the new coordinate system
            foreach (var point in target.Points)
            {
                helper.SetPoint(point);
                Point3D translatedPoint = new(helper.X, helper.Y, helper.Z);
                relatedScanner.Points.Add(translatedPoint);
            }

            relatedScanner.ProcessDistances();
            return relatedScanner;
        }

        private static List<(Scanner S, List<double> OverlappedDistances)> GetScannersWithOverlappedPointDistances(Scanner referenceScanner, List<Scanner> scannersToRelateToReference)
        {
            /*
             * The puzzle asks for at least shared 12 beacons (points) between sensors that can be related to each other.
             * 12 points would have a combined possible distance combination of 66 as follows:
             * For the first point, there are 11 distances.
             * For the second point, there are 10 distances, since distance between first and second is already calculated.
             * third/9
             * ...
             * Total combinations = 11 + 10 + 9 + 8 + 7 + 6 + 5 + 4 + 3 + 2 + 1 = 66
             *
             * However, I found that a few of the scanners had a situation where two point pairs generated the same distance.
             * When that happens, we don't know which point pair with that distance is really the shared point pair between sensors,
             * if there is a shared pair.
             *
             * Initially I thought this would cause major problems for my approach. However, I realized that I don't need all 12 points,
             * just the knowledge that their are 12 shared points.  So, I could leave off the distance that had two possible point pairs.
             * I dropped the ambiguous distance via an expression like: DistancesToPoints.Keys.Where(k => s.DistancesToPoints[k].Count == 1).
             *
             * Dropping the ambiguous distance meant I had to adjust my notion of enough overalapped distance down by 1. 66 became 65.
             */
            int enoughOverlappedDistances = 65; // 66 - 1 see comment above.

            var result = scannersToRelateToReference.
                Select(s =>
                    ( // Tuple Ctor
                        Scanner: s,
                        OverlappedDistances: s.DistancesToPoints.Keys.Where(k => s.DistancesToPoints[k].Count == 1). // Count == 1 looks for unambiguous distances. Some scanners have some point combinations that share the same distance between different points
                            Intersect(referenceScanner.DistancesToPoints.Keys.Where(k => referenceScanner.DistancesToPoints[k].Count == 1)).
                            ToList()
                    )
                ).
                Where(c => c.OverlappedDistances.Count >= enoughOverlappedDistances).
                ToList();

            return result;
        }

        private static CoordinateTranslationHelper GetTranslationHelper(double sourceXComponent, double sourceYComponent, double sourceZComponent, double targetXComponent, double targetYComponent, double targetZComponent)
        {
            Func<Point3D, double> xAccessor;
            if (Math.Abs(sourceXComponent) == Math.Abs(targetXComponent))
            {
                double multiplier = Math.Sign(sourceXComponent) == Math.Sign(targetXComponent) ? 1D : -1D;
                xAccessor = (Point3D p) => p.X * multiplier;
            }
            else if (Math.Abs(sourceXComponent) == Math.Abs(targetYComponent))
            {
                double multiplier = Math.Sign(sourceXComponent) == Math.Sign(targetYComponent) ? 1D : -1D;
                xAccessor = (Point3D p) => p.Y * multiplier;
            }
            else
            {
                double multiplier = Math.Sign(sourceXComponent) == Math.Sign(targetZComponent) ? 1D : -1D;
                xAccessor = (Point3D p) => p.Z * multiplier;
            }

            Func<Point3D, double> yAccessor;
            if (Math.Abs(sourceYComponent) == Math.Abs(targetXComponent))
            {
                double multiplier = Math.Sign(sourceYComponent) == Math.Sign(targetXComponent) ? 1D : -1D;
                yAccessor = (Point3D p) => p.X * multiplier;
            }
            else if (Math.Abs(sourceYComponent) == Math.Abs(targetYComponent))
            {
                double multiplier = Math.Sign(sourceYComponent) == Math.Sign(targetYComponent) ? 1D : -1D;
                yAccessor = (Point3D p) => p.Y * multiplier;
            }
            else
            {
                double multiplier = Math.Sign(sourceYComponent) == Math.Sign(targetZComponent) ? 1D : -1D;
                yAccessor = (Point3D p) => p.Z * multiplier;
            }

            Func<Point3D, double> zAccessor;
            if (Math.Abs(sourceZComponent) == Math.Abs(targetXComponent))
            {
                double multiplier = Math.Sign(sourceZComponent) == Math.Sign(targetXComponent) ? 1D : -1D;
                zAccessor = (Point3D p) => p.X * multiplier;
            }
            else if (Math.Abs(sourceZComponent) == Math.Abs(targetYComponent))
            {
                double multiplier = Math.Sign(sourceZComponent) == Math.Sign(targetYComponent) ? 1D : -1D;
                zAccessor = (Point3D p) => p.Y * multiplier;
            }
            else
            {
                double multiplier = Math.Sign(sourceZComponent) == Math.Sign(targetZComponent) ? 1D : -1D;
                zAccessor = (Point3D p) => p.Z * multiplier;
            }

            CoordinateTranslationHelper helper = new(xAccessor, yAccessor, zAccessor);
            return helper;
        }
    }
}
