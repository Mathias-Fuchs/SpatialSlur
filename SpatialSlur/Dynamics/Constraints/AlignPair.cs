﻿
/*
 * Notes
 */

using System;
using System.Collections.Generic;
using SpatialSlur.Collections;

namespace SpatialSlur.Dynamics.Constraints
{
    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public class AlignPair : Constraint, IConstraint
    {
        private Vector3d _delta;
        private int _i0, _i1;

        private Vector3d _target;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="index0"></param>
        /// <param name="index1"></param>
        /// <param name="target"></param>
        /// <param name="weight"></param>
        public AlignPair(int index0, int index1, Vector3d target, double weight = 1.0)
        {
            _i0 = index0;
            _i1 = index1;
            _target = target;
            Weight = weight;
        }


        /// <summary>
        /// 
        /// </summary>
        public int Index0
        {
            get { return _i0; }
            set { _i0 = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public int Index1
        {
            get { return _i1; }
            set { _i1 = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public Vector3d Target
        {
            get { return _target; }
            set { _target = value; }
        }


        /// <inheritdoc />
        public void Calculate(ReadOnlyArrayView<Body> bodies)
        {
            _delta = Vector3d.Reject(bodies[_i1].Position.Current - bodies[_i0].Position.Current, _target) * 0.5;
        }


        /// <inheritdoc />
        public void Apply(ReadOnlyArrayView<Body> bodies)
        {
            bodies[_i0].Position.AddDelta(_delta, Weight);
            bodies[_i1].Position.AddDelta(-_delta, Weight);
        }


        /// <inheritdoc />
        public void GetEnergy(out double linear, out double angular)
        {
            linear = _delta.Length * 2.0;
            angular = 0.0;
        }


        #region Explicit Interface Implementations

        bool IConstraint.AffectsPosition
        {
            get { return true; }
        }


        bool IConstraint.AffectsRotation
        {
            get { return false; }
        }


        IEnumerable<int> IConstraint.Indices
        {
            get
            {
                yield return _i0;
                yield return _i1;
            }
            set
            {
                var itr = value.GetEnumerator();

                itr.MoveNext();
                _i0 = itr.Current;

                itr.MoveNext();
                _i1 = itr.Current;
            }
        }

        #endregion
    }
}
