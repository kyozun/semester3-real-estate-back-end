using System.ComponentModel;

namespace semester3_real_estate_back_end.Helpers.Query;

public class JuridicalQuery
{
    public Guid? JuridicalId { get; set; }

    public string? Name { get; set; }

    [DefaultValue("10")] public int Limit { get; set; }
    public int Offset { get; set; }
}