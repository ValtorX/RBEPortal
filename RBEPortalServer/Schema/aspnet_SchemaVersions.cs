//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace RBEPortalServer.Schema
{
    [DataContract(IsReference = true)]
    public partial class aspnet_SchemaVersions
    {
        [DataMember]
        public string Feature { get; set; }
        [DataMember]
        public string CompatibleSchemaVersion { get; set; }
        [DataMember]
        public bool IsCurrentVersion { get; set; }
    }
    
}
