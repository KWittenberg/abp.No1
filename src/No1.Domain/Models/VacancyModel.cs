using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace No1.Models;

[NotMapped]
public class VacancyModel : AuditedEntity<string>
{
    public override string Id => VacancyId;
    public string dataAreaId { get; set; }
    public string VacancyId { get; set; }
        
        
    public DateTime EndDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public DateTime RAVJobConfirmed { get; set; }
    public DateTime RAVJobReported { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime WebEndDate { get; set; }
    public DateTime WebStartDate { get; set; }
    public double SstWorkingHoursPerWeek { get; set; }
    public long AgeFrom { get; set; }
    public long AgeTo { get; set; }
    public long Commission { get; set; }
    public long MaxSalary { get; set; }
    public long ProposalSalary { get; set; }
    public long StartTimeDay1 { get; set; }
    public long VacancyNum { get; set; }
    public long VacancyNumOpen { get; set; }
    public long WorkFactorFrom { get; set; }
    public long WorkFactorTo { get; set; }
    public string BusRelAccount { get; set; }
    public string CandidatePayrollType { get; set; }
    public string CommissionFrequency { get; set; }
    public string ContactName { get; set; }
    public string CountryId { get; set; }
    public string CurrencyCode { get; set; }
    public string CustContact { get; set; }
    public string CustInvContact { get; set; }
    public string Description { get; set; }
    public string DriverLicenseID { get; set; }
    public string Expensens { get; set; }
    public string JobId { get; set; }
    public string KeyWordsText { get; set; }
    public string MandateOrder { get; set; }
    public string MeetingPlace { get; set; }
    public string NotRealisedCode { get; set; }
    public string OccupationGroup { get; set; }
    public string OperationAddress { get; set; }
    public string OperationCity { get; set; }
    public string OperationCountryRegion { get; set; }
    public string OperationCounty { get; set; }
    public string OperationState { get; set; }
    public string OperationStreet { get; set; }
    public string OperationZipCode { get; set; }
    public string OrderReasonID { get; set; }
    public string PersonnelProjectAccounting { get; set; }
    public string Place { get; set; }
    public string ProjId { get; set; }
    public string ProjInvoiceProjId { get; set; }
    public string RAVReportingObligationExists { get; set; }
    public string RecruitType { get; set; }
    public string RegionId { get; set; }
    public string Remarks { get; set; }
    public string Requirements { get; set; }
    public string ResponsibleEmpl { get; set; }
    public string Route { get; set; }
    public string SexRequested { get; set; }
    public string SstEducation { get; set; }
    public string SstExperience { get; set; }
    public string SstKnowLedge { get; set; }
    public string SstWebTxt { get; set; }
    public string Status { get; set; }
    public string StfTexMatchID { get; set; }
    public string StfTexSourceUrl { get; set; }
    public string Tasks { get; set; }
    public string Type { get; set; }
    public string VehicleID { get; set; }
    public string WageType { get; set; }
    public string WorkingHoursDiscription { get; set; }
}