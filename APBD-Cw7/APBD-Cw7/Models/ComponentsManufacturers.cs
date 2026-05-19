namespace APBD_Cw7.Models;

public class ComponentsManufacturers
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public DateTime FoundationDate { get; set; }

    public ICollection<Components> Components { get; set; }
}