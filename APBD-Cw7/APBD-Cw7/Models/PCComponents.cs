namespace APBD_Cw7.Models;

public class PCComponents //ważne
{
    public int PCId { get; set; }
    public PCs PC { get; set; }

    public string ComponentCode { get; set; }
    public Components Component { get; set; }

    public int Amount { get; set; }
}