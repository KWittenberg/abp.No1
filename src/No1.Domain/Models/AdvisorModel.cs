using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace No1.Models;

[NotMapped]
public class AdvisorModel : AuditedEntity<string>
{
    public override string Id => EmplId;
    public string dataAreaId { get; set; }
    public string EmplId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
        
    public DateTime AbsentFrom { get; set; }
    public DateTime AbsentTo { get; set; }
    public DateTime AvailableFrom { get; set; }
    public DateTime AvailableTo { get; set; }
    public DateTime EmploymentDate { get; set; }
    public DateTime NextContactDate { get; set; }
    public DateTime RecommendationDate { get; set; }
    public DateTime TerminatedDate { get; set; }
    public DateTime TerminationDatePlanned { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
        
    public long Person { get; set; }
    public string AbsentReason { get; set; }
    public string Ansessungsbescheinigung { get; set; }
    public string AnsessungsbescheinigungInfo { get; set; }
    public string BusRelSubsegment2Id { get; set; }
    public string Candidate { get; set; }
    public string CandidateStatusId { get; set; }
    public string CheckedBy { get; set; }
    public string DataArea { get; set; }
    public string DefaultDimensionDisplayValue { get; set; }
    public string EMail { get; set; }
    public string EmploymentType { get; set; }
    public string EmplPayrollStatus { get; set; }
    public string Fax { get; set; }
    public string FunctionId { get; set; }
    public string Hobbies { get; set; }
    public string Impression { get; set; }
    public string ImpressionPreviousWork { get; set; }
    public string ImpressionRecommendation { get; set; }
    public string ImpressionShort { get; set; }
    public string Informal { get; set; }
    public string IntermediateEarnings { get; set; }
    public string LeavingReasonId { get; set; }
    public string MaritalStatus { get; set; }
    public string Mobile { get; set; }
    public string MobileCh { get; set; }
    public string MobilityId { get; set; }
    public string PeriodOfNotice { get; set; }
    public string PersonActive { get; set; }
    public string Phone { get; set; }
    public string PhoneEmergency { get; set; }
    public string PrimaryCity { get; set; }
    public string PrimaryCountryISOCode { get; set; }
    public string PrimaryStreet { get; set; }
    public string PrimaryZipCode { get; set; }
    public string QualityCode { get; set; }
    public string Recommendation { get; set; }
    public string RecommendationType { get; set; }
    public string RecruitType { get; set; }
    public string ReportInstructionsFreeText { get; set; }
    public string ShiftWillingnessEarly { get; set; }
    public string ShiftWillingnessGeneral { get; set; }
    public string ShiftWillingnessHoliday { get; set; }
    public string ShiftWillingnessLate { get; set; }
    public string ShiftWillingnessNight { get; set; }
    public string ShiftWillingnessTxt { get; set; }
    public string ShiftWillingnessWeekend { get; set; }
    public string Url { get; set; }
    public string WageTypeCurrent { get; set; }
    public string WageTypeWished { get; set; }
}