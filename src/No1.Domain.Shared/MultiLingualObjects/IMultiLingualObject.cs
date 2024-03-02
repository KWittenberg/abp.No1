using System.Collections.Generic;

namespace No1.MultiLingualObjects;

public interface IMultiLingualObject<TTranslation> where TTranslation : class, IObjectTranslation
{
    IList<TTranslation> Translations { get; set; }
}