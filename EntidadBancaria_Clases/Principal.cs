using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadBancaria_Clases
{
    public class Principal
    {
        ApplicattionDbContext dbContext = new ApplicattionDbContext();
        public void AgregarCliente(Cliente cliente)
        {
            if (cliente != null) {

                dbContext!.clientes.Add(cliente);
                dbContext.SaveChanges();

            }


        }

        public void CrearCuentaBancaria(CuentaBancaria cuentaBancaria)
        {
            if (cuentaBancaria != null)
            {
                dbContext!.cuentasBancarias.Add(cuentaBancaria);
                dbContext.SaveChanges(); }
        }

        public void EmitirTarjetaCredito(TarjetaCredito tarjetaCredito)
        {
            if (tarjetaCredito != null)
            {
                dbContext!.tarjetasCredito.Add(tarjetaCredito);
                dbContext.SaveChanges();
            }
        }

        public void PausarTarjetaCredito(int id, string estado)
        {
            
            TarjetaCredito? tarjetaCreditoSelect = dbContext!.tarjetasCredito.Find(id);

            if (tarjetaCreditoSelect != null)
            {

                tarjetaCreditoSelect.estado = estado;
            }

        }

        public void RealizarDeposito(string numeroCuenta, double monto)
        {
            CuentaBancaria? cuentaBancariaSelect = dbContext!.cuentasBancarias.Where(c => c.numeroCuenta == numeroCuenta) as CuentaBancaria;

            if (cuentaBancariaSelect != null)
            {
                cuentaBancariaSelect.saldo += monto;
            }

        }

        public void RealizarExtraccion(string numeroCuenta, double monto)
        {
            CuentaBancaria? cuentaBancariaSelect = dbContext!.cuentasBancarias.Where(c => c.numeroCuenta == numeroCuenta) as CuentaBancaria;

            if (cuentaBancariaSelect != null)
            {
                cuentaBancariaSelect.saldo -= monto;
            }

        }

        public void RealizarTRansferencia(string numeroCuentaOrigen, string numeroCuentaDestino, double monto)
        {
            CuentaBancaria? cuentaBancariaOrigenSelect = dbContext!.cuentasBancarias.Where(c => c.numeroCuenta == numeroCuentaOrigen) as CuentaBancaria;

            CuentaBancaria? cuentaBancariaDestinoSelect = dbContext!.cuentasBancarias.Where(c => c.numeroCuenta == numeroCuentaDestino) as CuentaBancaria;

            if (cuentaBancariaOrigenSelect != null && cuentaBancariaDestinoSelect != null)
            {
                cuentaBancariaOrigenSelect.saldo -= monto;
                cuentaBancariaDestinoSelect.saldo += monto;
            }

        }

        public void PagarTarjetaCredito(string numeroTarjeta, string numeroCuenta, string monto = "total")
        {

            CuentaBancaria? cuentaBancariaSelect = dbContext!.cuentasBancarias.Where(c => c.numeroCuenta == numeroCuenta) as CuentaBancaria;

            TarjetaCredito? tarjetaCreditoSelect = dbContext!.tarjetasCredito.Where(t => t.numeroTarjeta == numeroTarjeta) as TarjetaCredito;
            if (cuentaBancariaSelect != null && tarjetaCreditoSelect != null)
            {
                if (monto != "total")
                {
                    cuentaBancariaSelect.saldo -= Convert.ToDouble(monto);
                    tarjetaCreditoSelect.saldoDisponible += Convert.ToDouble(monto);

                }
                if (monto == "total")
                {
                    cuentaBancariaSelect.saldo -= tarjetaCreditoSelect.limiteCredito - tarjetaCreditoSelect.saldoDisponible;

                    tarjetaCreditoSelect.saldoDisponible = tarjetaCreditoSelect.limiteCredito;
                }

            }
        }

        public double GenerarResumenTarjeta(string numeroTarjeta)
        {
            
            TarjetaCredito? tarjetaCreditoSelect = dbContext!.tarjetasCredito.Where(t => t.numeroTarjeta == numeroTarjeta) as TarjetaCredito;
            if (tarjetaCreditoSelect != null)
            {
                double proximopago = tarjetaCreditoSelect.limiteCredito - tarjetaCreditoSelect.saldoDisponible;

                return proximopago;
            }else
            {
                return -1;

            }
        }





    }
}
