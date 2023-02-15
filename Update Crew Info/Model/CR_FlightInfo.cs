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
    
    public partial class CR_FlightInfo
    {
        public int FlightID { get; set; }
        public System.DateTime Date { get; set; }
        public string FlightNo { get; set; }
        public string Routing { get; set; }
        public string Aircraft { get; set; }
        public string RegisterNo { get; set; }
        public Nullable<System.DateTime> UTC { get; set; }
        public Nullable<System.DateTime> UTCstd { get; set; }
        public Nullable<System.DateTime> UTCDeparts { get; set; }
        public Nullable<System.DateTime> UTCArrives { get; set; }
        public Nullable<System.DateTime> LTstd { get; set; }
        public Nullable<System.DateTime> Departed { get; set; }
        public Nullable<System.DateTime> Arrived { get; set; }
        public string Parking { get; set; }
        public string Gate { get; set; }
        public string TypeApl { get; set; }
        public string Acf { get; set; }
        public string AcfNo { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public string VipRemark { get; set; }
        public string SpecialInfo { get; set; }
        public string Carry { get; set; }
        public Nullable<int> Capacity { get; set; }
        public string Classify { get; set; }
        public string PaxRemark { get; set; }
        public Nullable<int> TotalPax { get; set; }
        public Nullable<int> TotalPaxC { get; set; }
        public Nullable<int> TotalPaxI { get; set; }
        public Nullable<int> TotalPaxY { get; set; }
        public Nullable<int> CkinC { get; set; }
        public Nullable<int> CkinI { get; set; }
        public Nullable<int> CkinY { get; set; }
        public Nullable<int> TotalPaxCKI { get; set; }
        public Nullable<int> TotalVIP { get; set; }
        public Nullable<int> TotalCIP { get; set; }
        public Nullable<int> TotalWchr { get; set; }
        public Nullable<int> TotalSM { get; set; }
        public Nullable<int> TotalBSCT { get; set; }
        public Nullable<int> TotalINF { get; set; }
        public Nullable<int> TotalUM { get; set; }
        public Nullable<int> TotalBLND { get; set; }
        public Nullable<int> TotalDEAF { get; set; }
        public Nullable<int> TotalSTCR { get; set; }
        public Nullable<int> TotalEXST { get; set; }
        public Nullable<int> TotalDEPU { get; set; }
        public Nullable<int> Version { get; set; }
        public Nullable<bool> isLocked { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string Creator { get; set; }
        public string Modifier { get; set; }
        public string Creatorid { get; set; }
        public string Modifierid { get; set; }
        public Nullable<int> CrewTaskStatus { get; set; }
        public Nullable<int> FlightReportStatus { get; set; }
        public Nullable<int> AssessmentStatus { get; set; }
        public Nullable<int> OJTStatus { get; set; }
        public string Purserid { get; set; }
        public string PurserName { get; set; }
        public Nullable<System.DateTime> PurserDate { get; set; }
        public Nullable<bool> FORequest { get; set; }
        public Nullable<System.DateTime> FORequested { get; set; }
        public Nullable<System.DateTime> FOApplied { get; set; }
        public Nullable<int> Avesid { get; set; }
        public Nullable<System.DateTime> UTCsta { get; set; }
        public Nullable<System.DateTime> UTCetd { get; set; }
    }
}
