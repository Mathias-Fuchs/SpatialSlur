﻿using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;

using SpatialSlur.SlurField;

/*
 * Notes
 */

namespace SlurSketchGH.Grasshopper.Components
{
    public class GeodesicDistanceL2 : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the FastMarching2dTest class.
        /// </summary>
        public GeodesicDistanceL2()
          : base("Geodesic Distance L2", "GeoDistL2",
                "Returns the Euclidean geodesic distance field for a set of sources",
                "SpatialSlur", "Field")
        {
        }


        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("travelCost", "travelCost", "", GH_ParamAccess.item);
            pManager.AddGenericParameter("sources", "sources", "", GH_ParamAccess.item);
        }


        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("result", "result", "", GH_ParamAccess.item);
        }


        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            GH_ObjectWrapper costIn = null;
            GH_ObjectWrapper srcIn = null;    

            if (!DA.GetData(0, ref costIn)) return;
            if (!DA.GetData(1, ref srcIn)) return;

            var cost = (GridScalarField2d)costIn.Value;
            var src = (IEnumerable<int>)srcIn.Value;

            var dist = SolveInstanceImpl(cost, src);

            DA.SetData(0, new GH_ObjectWrapper(dist));
        }


        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }


        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{057278e1-ad59-424b-abf1-58508a7d9f6c}"); }
        }


        /// <summary>
        /// 
        /// </summary>
        private GridScalarField2d SolveInstanceImpl(GridScalarField2d cost, IEnumerable<int> sources)
        {
            var dist = new GridScalarField2d(cost);
            FieldSim.GeodesicDistanceL2(cost, sources, dist.Values);
            return dist;
        }
    }
}