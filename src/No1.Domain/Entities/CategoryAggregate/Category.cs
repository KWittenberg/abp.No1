using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace No1.Entities.CategoryAggregate;

public class Category : AuditedAggregateRoot<Guid>//, IMultiLingualObject<CategoryTranslation>
{
    protected Category()
    {
    }

    public Category(Guid id, string name, string? description) : base(id)
    {
        SetName(name);

        SetDescription(description);

        // Translations = new List<CategoryTranslation>();
    }

    public string Name { get; private set; }

    public string? Description { get; private set; }

    // public IList<CategoryTranslation> Translations { get; set; }

    public void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), CategoryConsts.NameLength);
    }

    public void SetDescription(string? description)
    {
        Description = Check.NotNullOrWhiteSpace(description, nameof(description), CategoryConsts.DescriptionLength);
    }
}