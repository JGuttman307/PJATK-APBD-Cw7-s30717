using APBD_Cw7.DTOs.Requests;
using APBD_Cw7.DTOs.Responses;
using APBD_Cw7.Infrastructure;
using APBD_Cw7.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace APBD_Cw7.Services;

public class PCsService : IPCsService
{
     private readonly DatabaseContext _context;

    public PCsService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PCResponse>> GetAllAsync()
    {
        return await _context.PCs
            .Select(x => new PCResponse(
            
                x.Id,
                x.Name,
                x.Weight,
                x.Warranty,
                x.CreatedAt,
                x.Stock
            ))
            .ToListAsync();
    }

    public async Task<IEnumerable<PCComponentsResponse>> GetComponentsAsync(int id)
    {
        var pc = await _context.PCs
            .Include(x => x.PCComponents)
            .ThenInclude(x => x.Component)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (pc == null)
            return null;

        return pc.PCComponents.Select(x => new PCComponentsResponse( 
        
            x.Component.Code,
            x.Component.Name,
            x.Amount
        ));
    }

    public async Task<PCResponse> CreateAsync(PCCreateRequest dto)
    {
        var pc = new PCs
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt,
            Stock = dto.Stock
        };

        _context.PCs.Add(pc);
        await _context.SaveChangesAsync();

        return new PCResponse
        (
            pc.Id,
            pc.Name,
            pc.Weight,
            pc.Warranty,
            pc.CreatedAt,
            pc.Stock
        );
    }

    public async Task<bool> UpdateAsync(int id, PCUpdateRequest dto)
    {
        var pc = await _context.PCs.FindAsync(id);

        if (pc == null)
            return false;

        pc.Name = dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pc = await _context.PCs.FindAsync(id);

        if (pc == null)
            return false;

        _context.PCs.Remove(pc);

        await _context.SaveChangesAsync();

        return true;
    }
}