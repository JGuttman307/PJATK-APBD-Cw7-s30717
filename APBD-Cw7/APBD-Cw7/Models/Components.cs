namespace APBD_Cw7.Models;

public class Components
{
    public string Code { get; set; } = string.Empty; //nvarchar(max)
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int ComponentManufacturerId { get; set; }
    public ComponentsManufacturers ComponentManufacturer { get; set; }

    public int ComponentTypeId { get; set; }
    public ComponentTypes ComponentType { get; set; }

    public ICollection<PCComponents> PCComponents { get; set; }
}