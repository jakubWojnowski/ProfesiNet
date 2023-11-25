namespace ProfesiNet.Users.Application.Certificates.Dtos;

public class GetCertificateDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
}