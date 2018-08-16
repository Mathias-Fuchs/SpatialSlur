﻿
/*
 * Notes
 */

using System;
using System.Linq;

using Grasshopper.Kernel;

using SpatialSlur.Meshes;
using SpatialSlur.Grasshopper.Types;
using SpatialSlur.Grasshopper.Params;

namespace SlurGH.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class FacePlanarity : GH_Component
    {
        /// <summary>
        /// 
        /// </summary>
        public FacePlanarity()
          : base("Face Planarity", "FacePln",
              "Returns the planar deviation of each face in a halfedge mesh.",
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
            pManager.AddNumberParameter("result", "result", "", GH_ParamAccess.list);
        }


        /// <inheritdoc />
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            GH_HeMesh3d mesh = null;
            if (!DA.GetData(0, ref mesh)) return;
            
            var result = mesh.Value.Faces.Select(f => f.IsUnused ? 0.0 : f.GetPlanarity(v => v.Position));
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
            get { return new Guid("{85A0F167-8DCD-4397-848F-93482E6708D7}"); }
        }
    }
}

