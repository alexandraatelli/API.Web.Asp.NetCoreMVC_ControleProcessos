using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppControleJuridico.Models
{
    public class Processo : Entity
    {
        [DisplayName("Nome Cliente:")]
        public Guid ClienteId { get; set; }


        [DisplayName("Tipo Justiça:")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string TipoJustiça { get; set; }


        [DisplayName("Tipo Ação:")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(150, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string TipoAcao { get; set; }


        [DisplayName("Num. Processo:")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(20, ErrorMessage = "O campo {0} precisa ter {1} caracteres", MinimumLength = 20)]
        public string NumeroProcesso { get; set; }


        [DisplayName("Parte Contrária:")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string ParteContraria { get; set; }


        [DisplayName("Autuação:")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime Autuacao { get; set; }


        [DisplayName("Trâmite:")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Tramites { get; set; }


        [DisplayName("Vr. Causa:")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal ValorCausa { get; set; }


        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }

        /// <summary>
        /// EF Relacionamento: Temos para esse processo um cliente: Cliente tem muitos processos, enquanto
        /// um processo tem apenas um cliente - 1 para n.
        /// </summary>
        public Cliente Cliente { get; set; } // propriedades de navegação para o EF
    }
}
