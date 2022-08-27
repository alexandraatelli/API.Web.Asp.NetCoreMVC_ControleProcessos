using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppControleJuridico.Models
{
    public class Cliente : Entity
    {

        //[DisplayName("Nome Cliente:")]
        [Display(Name = "Nome Cliente:",Order = -9)]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string NomeCliente { get; set; }


        [DisplayName("Documento:")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
        public string Documento { get; set; }


        [DisplayName("Tipo Pessoa:")]
        public TipoPessoa TipoPessoa { get; set; }


        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }


        [DisplayName("CEP:")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(8, ErrorMessage = "O campo {0} precisa ter {1} caracteres", MinimumLength = 8)]
        public string Cep { get; set; }


        [DisplayName("Logradouro:")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Logradouro {get; set; }


        [DisplayName("Nr:")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Numero { get; set; }


        [DisplayName("Complemento:")]
        public string Complemento { get; set; }


        [DisplayName("Bairro:")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Bairro { get; set; }


        [DisplayName("Cidade:")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Cidade { get; set; }


        [DisplayName("Estado:")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Estado { get; set; }


        /// <summary>
        /// Para fins de relacionamento, o Entity Framework precisa entender que via declaração de propriedade,
        /// precisa-se delaclar: Relação de 1 para "muitos" de Processo.
        /// </summary>
        public IEnumerable<Processo> Processos { get; set; }
    }
}
