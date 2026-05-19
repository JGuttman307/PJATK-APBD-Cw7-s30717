using APBD_Cw7.DTOs.Requests;
using APBD_Cw7.DTOs.Responses;

namespace APBD_Cw7.Services;

public interface IPCsService
{
    Task<IEnumerable<PCResponse>> GetAllAsync();

    Task<IEnumerable<PCComponentsResponse>> GetComponentsAsync(int id);

    Task<PCResponse> CreateAsync(PCCreateRequest dto);

    Task<bool> UpdateAsync(int id, PCUpdateRequest dto);

    Task<bool> DeleteAsync(int id);
}