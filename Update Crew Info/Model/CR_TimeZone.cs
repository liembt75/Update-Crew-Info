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
    
    public partial class CR_TimeZone
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public Nullable<short> Summer_Begin { get; set; }
        public Nullable<short> Winter_Begin { get; set; }
        public Nullable<decimal> Summer_diff { get; set; }
        public Nullable<decimal> Winter_diff { get; set; }
        public string Note { get; set; }
    }
}
