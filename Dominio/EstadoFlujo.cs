namespace Dominio
{
    public class EstadoFlujo
    {
        public int Id { get; set; }
        public Estado Origen { get; set; }
        public Estado Destino { get; set; }
    }
}