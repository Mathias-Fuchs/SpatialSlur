﻿
/*
 * Notes
 */

using System;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;

using Vec3d = Rhino.Geometry.Vector3d;

namespace SpatialSlur.Grasshopper.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class MeshClosedPolyline : GH_Component
    {
        /// <summary>
        /// 
        /// </summary>
        public MeshClosedPolyline()
          : base("Mesh Closed Polyline", "MeshPoly",
              "Creates a mesh from a closed polyline.",
              "Mesh", "Util")
        {
        }


        /// <inheritdoc />
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("polyline", "poly", "Closed polyline to mesh", GH_ParamAccess.item);
        }


        /// <inheritdoc />
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddMeshParameter("mesh", "mesh", "Resulting mesh", GH_ParamAccess.item);
        }


        /// <inheritdoc />
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            GH_Curve curve = null;
            if (!DA.GetData(0, ref curve)) return;

            if (!curve.Value.TryGetPolyline(out var poly))
                throw new ArgumentException("The given curve is not a polyline.");
            
            DA.SetData(0, new GH_Mesh(Mesh.CreateFromClosedPolyline(poly)));
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
            get { return new Guid("{8D35230C-B9CD-479D-AAD6-F579E4BEB5F1}"); }
        }
    }
}
