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
    
    public partial class t_TrainingTypeCategory
    {
        public short c_PID { get; set; }
        public string c_Name { get; set; }
        public string c_Code { get; set; }
        public int c_GroupID { get; set; }
        public System.DateTime c_ValidFr { get; set; }
        public Nullable<System.DateTime> c_ValidTo { get; set; }
        public short c_BreakCodeID { get; set; }
        public byte c_ExpireMonths { get; set; }
        public short c_EstimateStudents { get; set; }
        public string c_Location { get; set; }
        public decimal c_WarningMonths { get; set; }
        public bool c_StatisticEnable { get; set; }
        public short c_SortOrder { get; set; }
        public string c_ExternalCode { get; set; }
        public bool c_IsDeleted { get; set; }
        public string c_TimesDefault { get; set; }
        public bool c_WarningDisabled { get; set; }
    }
}
