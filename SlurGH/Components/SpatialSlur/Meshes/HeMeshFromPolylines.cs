﻿
/*
 * Notes
 */

using System;
using System.Collections.Generic;
using System.Linq;

using Rhino.Geometry;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;

using SpatialSlur.Meshes;
using SpatialSlur.Rhino;
using SpatialSlur.Grasshopper.Types;
using SpatialSlur.Grasshopper.Params;

namespace SpatialSlur.Grasshopper.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class HeMeshFromPolylines : GH_Component
    {
        /// <summary>
        /// 
        /// </summary>
        public HeMeshFromPolylines()
          : base("HeMesh From Polylines", "FromPolys",
              "Creates a halfedge mesh from a list of closed polylines",
              "SpatialSlur", "Mesh")
        {
        }


        /// <inheritdoc />
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("polylines", "polys", "", GH_ParamAccess.list);
            pManager.AddNumberParameter("tolerance", "tol", "", GH_ParamAccess.item, 1.0e-4);
        }


        /// <inheritdoc />
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddParameter(new HeMesh3dParam(), "result", "result", "", GH_ParamAccess.item);
        }


        /// <inheritdoc />
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            var curves = new List<GH_Curve>();
            if (!DA.GetDataList(0, curves)) return;

            var tol = 0.0;
            if (!DA.GetData(1, ref tol)) return;

            var mesh = HeMesh3d.Factory.CreateFromPolylines(curves.Select(crv =>
            {
                crv.Value.TryGetPolyline(out Polyline poly);
                return poly;
            }), tol);

            DA.SetData(0, new GH_HeMesh3d(mesh));
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
            get { return new Guid("{2A36DB93-46BB-419C-810B-98B67D88BEDB}"); }
        }
    }
}
