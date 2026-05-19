namespace APBD_Cw7.Models;

public class ComponentTypes
{
    
    public int Id { get; set; }
    public string Abbreviation { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public ICollection<Components> Components { get; set; }
}