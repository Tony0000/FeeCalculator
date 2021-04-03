using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public record FeeQuery
    {
        [Required]
        [DefaultValue(100)]
        public double? InitialValue { get; init; }
        [Required]
        [DefaultValue(5)]
        public uint? Months { get; init; }
    }
}