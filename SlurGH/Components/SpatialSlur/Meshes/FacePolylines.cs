﻿
/*
 * Notes
 */

using System;
using System.Linq;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;

using SpatialSlur.Grasshopper.Types;
using SpatialSlur.Grasshopper.Params;

namespace SlurGH.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class FacePolylines : GH_Component
    {
        /// <summary>
        /// 
        /// </summary>
        public FacePolylines()
          : base("Face Polylines", "FacePolys",
              "Returns the boundary of each face in a given halfedge mesh as a closed polyline",
              "SpatialSlur", "Mesh")
        {
        }


        /// <inheritdoc />
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddParameter(new HeMesh3dParam(), "heMesh", "heMesh", "", GH_ParamAccess.item);
        }


        /// <inheritdoc />
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("result", "result", "", GH_ParamAccess.list);
        }


        /// <inheritdoc />
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            GH_HeMesh3d mesh = null;
            if (!DA.GetData(0, ref mesh)) return;

            var result = mesh.Value.Faces.Select(f =>
            {
                if (f.IsUnused) return new GH_Curve();

                var poly = new Polyline(f.Halfedges.Select(he => (Point3d)he.Start.Position));
                poly.Add(poly[0]);

                return new GH_Curve(poly.ToNurbsCurve());
            });
        
            DA.SetDataList(0, result);
        }


        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get { return null; }
        }


        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{BD140500-B9EA-43EE-94B9-358BEACF803C}"); }
        }
    }
}

