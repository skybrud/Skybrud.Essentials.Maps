namespace Skybrud.Essentials.Maps.Geometry.Shapes {

    public interface IShape : IGeometry {

        bool Contains(IPoint point);

        IPoint GetCenter();

        double GetArea();

        double GetCircumference();

        IRectangle GetBoundingBox();

    }

}