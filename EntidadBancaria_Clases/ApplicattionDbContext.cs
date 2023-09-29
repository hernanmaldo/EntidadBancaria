using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadBancaria_Clases
{
    public class ApplicattionDbContext :DbContext
    {
       public DbSet<Cliente> clientes { get; set;}
       public DbSet<CuentaBancaria> cuentasBancarias { get; set;}
       public DbSet<TarjetaCredito> tarjetasCredito { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-4U7JAH5\SQLEXPRESS;Database=EntidadBancaria");
        }
    }


}

