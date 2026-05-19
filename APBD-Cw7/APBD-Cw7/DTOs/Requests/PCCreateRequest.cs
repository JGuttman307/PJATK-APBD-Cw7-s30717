namespace APBD_Cw7.DTOs.Requests;

public record PCCreateRequest(
    string Name,
    float Weight,
    int Warranty,
    DateTime CreatedAt,
    int Stock
    );