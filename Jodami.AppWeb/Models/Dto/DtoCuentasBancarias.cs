using Jodami.Entity;

namespace Jodami.AppWeb.Models.Dto
{
    public class DtoCuentasBancarias
    {
        public int IdSocio { get; set; }
        public string TipoNroDcmto { get; set; }
        public string Nombres { get; set; }
        public string Situacion { get; set; }

        public string TipoSocio { get; set; }
        public string ControladorOrigen { get; set; }
        public string AccionOrigen { get; set; }

        public SocioCuentaBanco NewSocioCuentaBanco { get; set; }

        public List<SocioCuentaBanco> LstSocioCuentaBanco { get; set; }

        public List<Moneda> LstMoneda { get; set; }
        public List<EntidadFinanciera> LstEntidadFinanciera { get; set; }
        public List<TipoCuentaBancaria> LstTipoCuentaBancaria { get; set; }
         

    }
}