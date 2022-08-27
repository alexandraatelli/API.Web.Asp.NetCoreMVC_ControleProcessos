namespace AppControleJuridico.Models
{
    /// <summary>
    /// Ela só pode ser herdada.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Gera valor no formato Guid usado como Id para qualquer objeto a ser persistido na tabela
        /// </summary>
        protected Entity()
        {
            Id = Guid.NewGuid(); 
                                 
        }

        public Guid Id { get; set; }
    }
}
