using System.ComponentModel;

namespace WeVsVirus.Models.Enums
{
    public enum SwabJobState : byte
    {
        [Description("Offen")]
        Open,
        [Description("Zugeteilt")]
        Assigned,
        [Description("Angenommen")]
        Accepted,
        [Description("Zugestellt")]
        Complete,
        [Description("Abgebrochen")]
        Canceled
    }
}
