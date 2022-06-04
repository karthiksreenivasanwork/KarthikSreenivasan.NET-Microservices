using System.ComponentModel.DataAnnotations;

namespace PlatformService.Dtos
{
    /// <summary>
    /// This is a Data Transfer Object (DTO) class that exposes members 
    /// of PlatformService.Models.Platform which are required for CREATING
    /// data.
    /// 
    /// Note: Since this is a create DTO, if the caller does not provide
    /// the required properties, the controller will return a pre-formatted data 
    /// for the property values that are missing.
    /// </summary>
    public class PlatformCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public string Cost { get; set; }
    }
}