﻿
/*
 * Notes
 */

using System;
using System.Linq;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;

using SpatialSlur;
using SpatialSlur.Collections;

using Vec3d = Rhino.Geometry.Vector3d;

namespace SpatialSlur.Grasshopper.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class AlignVertices : GH_Component
    {
        /// <summary>
        /// 
        /// </summary>
        public AlignVertices()
          : base("Align Vertices", "AlignVerts",
              "Aligns positions of near-coincident vertices in a mesh.",
              "Mesh", "Util")
        {
        }


        /// <inheritdoc />
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddMeshParameter("mesh", "mesh", "Mesh with vertices to align", GH_ParamAccess.item);
            pManager.AddNumberParameter("radius", "radius", "Vertex search radius", GH_ParamAccess.item, 1.0e-4);
        }


        /// <inheritdoc />
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddMeshParameter("result", "result", "Mesh with aligned vertices", GH_ParamAccess.item);
        }


        /// <inheritdoc />
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Mesh mesh = null;
            double tol = 0.0;

            if (!DA.GetData(0, ref mesh)) return;
            if (!DA.GetData(1, ref tol)) return;

            var verts = mesh.Vertices;
            var points = verts.Select(p => (Vector3d)p).ToArray();

            Proximity.Consolidate(points, tol, 3);
            //Message = (points.Consolidate(tol)) ? "Converged" : "Not converged";
   
            for (int i = 0; i < verts.Count; i++)
                verts[i] = (Point3f)points[i];

            DA.SetData(0, new GH_Mesh(mesh));
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
            get { return new Guid("{FC8A6CF2-C16A-4A23-8012-28B9F36B1844}"); }
        }
    }
}
