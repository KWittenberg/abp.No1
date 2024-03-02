using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace No1.Models;

public class DossierSubmittalModelJson
{
    public string odatacontext { get; set; }
    public DossierSubmittalModel[] value { get; set; }
}

public class DossierSubmittalModel : AuditedEntity<string>
{
    public override string Id => DossierSubmittalId;
    public string dataAreaId { get; set; }
    public string DossierSubmittalId { get; set; }
    public string StatusId { get; set; }
    public DateTime StatusIdChanged { get; set; }

    public DateTime SstMonth { get; set; }
    public DateTime SstStartDate { get; set; }

    public long SstAmount { get; set; }
    public long SstFixedFeePercent { get; set; }
    public long SstYearlyGrossSalary { get; set; }
    public string BusinessLineId { get; set; }
    public string BusRelAccount { get; set; }
    public string BusRelContact { get; set; }
    public string Closed { get; set; }
    public string ConsultantCandidate_PersonnelNumber { get; set; }
    public string ConsultantSubsidiary { get; set; }
    public string ConsultantVacancy_PersonnelNumber { get; set; }
    public string Description { get; set; }
    public string DirPerson_FK_PartyNumber { get; set; }
    public string DirPerson_FK1_PartyNumber { get; set; }
    public string DirPerson_FK2_PartyNumber { get; set; }
    public string DirPerson_FK3_PartyNumber { get; set; }
    public string EmplId { get; set; }
    public string EmplIdWorker_PersonnelNumber { get; set; }
    public string ImpressionEmpl { get; set; }
    public string ImpressionPreviousWork { get; set; }
    public string ImpressionRecommendation { get; set; }
    public string Notes { get; set; }
    public string ResponsiblePerson { get; set; }
    public string ResponsibleWorker_PersonnelNumber { get; set; }
    public string ServiceAreaId { get; set; }
    public string SstContracted { get; set; }
    public string SstReason { get; set; }
    public string TypeId { get; set; }
    public string UrlJobRequest { get; set; }
    public string UrlOffer { get; set; }
    public string Vacancy_VacancyId { get; set; }
    public string VacancyAssignmentID { get; set; }
    
    public string Interview_01Txt { get; set; }
    public DateTime Interview_01 { get; set; }
    public string YshSchedulingSite_01 { get; set; }
    public string YshSchedulingParticipant_01 { get; set; }
    public string YshSchedulingCustomerComments_01 { get; set; }

    public string Interview_02Txt { get; set; }
    public DateTime Interview_02 { get; set; }
    public string YshSchedulingSite_02 { get; set; }
    public string YshSchedulingParticipant_02 { get; set; }
    public string YshSchedulingCustomerComments_02 { get; set; }

    public string Interview_03Txt { get; set; }
    public DateTime Interview_03 { get; set; }
    public string YshSchedulingSite_03 { get; set; }
    public string YshSchedulingParticipant_03 { get; set; }
    public string YshSchedulingCustomerComments_03 { get; set; }
}