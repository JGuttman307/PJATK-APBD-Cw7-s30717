namespace APBD_Cw7.Models;

public class ComponentTypes
{
    
    public int Id { get; set; }
    public string Abbreviation { get; set; }
    public string Name { get; set; }

    public ICollection<Components> Components { get; set; }
}