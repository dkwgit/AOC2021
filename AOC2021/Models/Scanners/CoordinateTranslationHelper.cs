// -----------------------------------------------------------------------
// <copyright file="CoordinateTranslationHelper.cs" company="David Wright">
// Copyright (c) David Wright. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AOC2021.Models.Scanners
{
    using MathNet.Spatial.Euclidean;

    internal class CoordinateTranslationHelper
    {
        private readonly Func<Point3D, double> xAccessor;

        private readonly Func<Point3D, double> yAccessor;

        private readonly Func<Point3D, double> zAccessor;

        private Point3D? currentPoint;

        private Vector3D translationVector = new(0, 0, 0);

        internal CoordinateTranslationHelper(Func<Point3D, double> xAccessor, Func<Point3D, double> yAccessor, Func<Point3D, double> zAccessor)
        {
            this.xAccessor = xAccessor;
            this.yAccessor = yAccessor;
            this.zAccessor = zAccessor;
        }

        internal double X
        {
            get
            {
                double xValue = this.xAccessor(this.CurrentPoint);
                return xValue + this.TranslationVector.X;
            }
        }

        internal double Y
        {
            get
            {
                double yValue = this.yAccessor(this.CurrentPoint);
                return yValue + this.TranslationVector.Y;
            }
        }

        internal double Z
        {
            get
            {
                double zValue = this.zAccessor(this.CurrentPoint);
                return zValue + this.TranslationVector.Z;
            }
        }

        internal Vector3D TranslationVector
        {
            get
            {
                return this.translationVector;
            }

            set
            {
                this.translationVector = value;
            }
        }

        private Point3D CurrentPoint
        {
            get
            {
                if (!this.currentPoint.HasValue)
                {
                    throw new InvalidOperationException("CurrentPoint accessed before initialized");
                }

                return this.currentPoint.Value;
            }
        }

        internal CoordinateTranslationHelper SetPoint(Point3D point)
        {
            this.currentPoint = point;
            return this;
        }
    }
}
