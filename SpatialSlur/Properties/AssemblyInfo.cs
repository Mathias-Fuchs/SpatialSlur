﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("SpatialSlur")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("SpatialSlur")]
[assembly: AssemblyCopyright("Copyright © David Reeves 2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("11db2625-f1c7-4fca-a7d3-f8ac93f99c8a")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
// [assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyVersion(SpatialSlur.AssemblyInfo.VersionNumber)]

// Expose internals
//[assembly: InternalsVisibleTo("SlurGH")]
//[assembly: InternalsVisibleTo("Examples")]

namespace SpatialSlur
{
    /// <summary>
    /// 
    /// </summary>
    public static class AssemblyInfo
    {
        /// <summary></summary>
        public const string VersionNumber = "0.3.1.*";
    }
}