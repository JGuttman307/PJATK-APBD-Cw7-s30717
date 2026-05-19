using Microsoft.EntityFrameworkCore;

namespace APBD_Cw7.Infrastructure;

public class DatabaseContext(DbContextOptions opt) : DbContext(opt)
{
    
}