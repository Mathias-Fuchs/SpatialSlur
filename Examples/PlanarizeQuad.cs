﻿
/*
 * Notes
 */

using System;
using SpatialSlur;
using SpatialSlur.Dynamics;

namespace SpatialSlur.Examples
{
    /// <summary>
    /// Planarizes a randomly generated quadrilateral.
    /// </summary>
    static class PlanarizeQuad
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Start()
        {
            var random = new Random(0);
            var box = new Interval3d(new Vector3d(0.0), new Vector3d(10.0)); // create a interval between the (0,0,0) and (10,10,10)

            // create bodies
            var bodies = new Body[] {
                new Body(random.NextVector3d(box)),
                new Body(random.NextVector3d(box)),
                new Body(random.NextVector3d(box)),
                new Body(random.NextVector3d(box))
            };

            // create constraints
            var constraints = new IConstraint[] {
                new PlanarQuad(0, 1, 2, 3)
            };

            // create solver
            var solver = new ConstraintSolver();
     
            // wait for keypress to start the solver
            Console.WriteLine("Press return to start the solver.");
            Console.ReadLine();

            // step the solver until converged
            while (!solver.IsConverged)
            {
                solver.Step(bodies, constraints);
                Console.WriteLine($"    step {solver.StepCount}");
            }

            Console.WriteLine("\nSolver converged! Press return to exit.");
            Console.ReadLine();
        }
    }
}
