//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Update_Crew_Info.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CR_Flight_CoviComRoute
    {
        public int ID { get; set; }
        public Nullable<int> FlightId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Note { get; set; }
        public Nullable<bool> Isdeleted { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string Creatorid { get; set; }
        public string Modifierid { get; set; }
        public string Creator { get; set; }
        public string Modifier { get; set; }
    }
}
