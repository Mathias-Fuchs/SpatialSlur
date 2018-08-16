﻿
/*
 * Notes
 */

using System;
using Rhino.Geometry;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using SpatialSlur.Rhino;

namespace SpatialSlur.Grasshopper.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class MeshSeparate : GH_Component
    {
        /// <summary>
        /// 
        /// </summary>
        public MeshSeparate()
          : base("Mesh Separate", "Separate",
              "Separates all faces in a given mesh",
              "Mesh", "Util")
        {
        }


        /// <inheritdoc />
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddMeshParameter("mesh", "mesh", "Mesh to separate", GH_ParamAccess.item);
        }


        /// <inheritdoc />
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddMeshParameter("result", "result", "Separated mesh", GH_ParamAccess.item);
        }


        /// <inheritdoc />
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Mesh mesh = null;
            if (!DA.GetData(0, ref mesh)) return;

            mesh = mesh.ToPolySoup();

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
            get { return new Guid("{29FE6C46-B4B0-4BEE-95D6-549001490DE5}"); }
        }
    }
}

