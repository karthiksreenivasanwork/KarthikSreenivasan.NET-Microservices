namespace PlatformService.Dtos
{
    /// <summary>
    /// This is a Data Transfer Object (DTO) class that exposes members 
    /// of PlatformService.Models.Platform which are required for READING
    /// data from this service.
    /// 
    /// For now, we are exposing all the properties.
    /// Note: Since this is a Read DTO, we do not need data annotations.
    /// </summary>
    public class PlatformReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Publisher { get; set; }

        public string Cost { get; set; }
    }
}