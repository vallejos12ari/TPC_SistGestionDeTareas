namespace Dominio
{
    public class ReporteTareasPorEstado
    {
        public string Estado { get; set; }
        public int Cantidad { get; set; }
    }

    public class ReporteTareasVencidas
    {
        public int Vencidas { get; set; }
        public int EnFecha { get; set; }
    }

    public class ReporteHorasPorUsuario
    {
        public string Usuario { get; set; }
        public decimal TotalHoras { get; set; }
    }
}