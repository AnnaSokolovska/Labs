//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Notary2015
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class order
    {
        public int idOrder { get; set; }
        public string Type { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int Notary_idNotary { get; set; }

        [JsonIgnore]
        public virtual notary notary { get; set; }
    }
}
