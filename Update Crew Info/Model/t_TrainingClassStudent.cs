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
    
    public partial class t_TrainingClassStudent
    {
        public int c_PID { get; set; }
        public int c_ClassID { get; set; }
        public System.DateTime c_ClassDate { get; set; }
        public short c_TrainingTypeID { get; set; }
        public short c_TrainingTypeLinkID { get; set; }
        public string c_CrewID { get; set; }
        public byte c_FinalResult { get; set; }
        public int c_SchedulingID { get; set; }
        public string c_DocQualified { get; set; }
        public Nullable<System.DateTime> c_ExpiredDate { get; set; }
        public Nullable<System.DateTime> c_ExpiredBreak { get; set; }
        public string c_Note { get; set; }
        public string c_WarningMessage { get; set; }
        public bool c_IsDeleted { get; set; }
        public byte c_ClassNature { get; set; }
        public string c_CreatedBy { get; set; }
        public Nullable<System.DateTime> c_CreatedAt { get; set; }
        public string c_WarningID { get; set; }
        public string c_WarningDisabledIDs { get; set; }
    }
}