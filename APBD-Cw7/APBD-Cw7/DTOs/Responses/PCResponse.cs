using Microsoft.AspNetCore.Mvc;

namespace APBD_Cw7.DTOs.Responses;

public record PCResponse(
    int Id,
    string Name,
    float Weight,
    int Warranty,
    DateTime CreatedAt,
    int Stock
);
    
