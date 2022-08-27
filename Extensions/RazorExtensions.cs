using Microsoft.AspNetCore.Mvc.Razor;

namespace AppControleJuridico.Extensions
{
    /// <summary>
    /// Método que estede a RazorView, passando Int que é o Tipo de Pessoa física ou jurídica e o documento. Se o tipo da pessoa for 1, converte o documento para Int64 e formatar o item para string que é uma forma de trabalhar a formatação de valores. Exitem documentação da Microsoft sobre isso.
    /// </summary>
    public static class RazorExtensions
    {
        public static string FormataDocumento(this RazorPage page, int TipoPessoa, string Documento)
        {
            return TipoPessoa == 1 ? Convert.ToUInt64(Documento).ToString(format: @"000\.000\.000\-00") :
                Convert.ToUInt64(Documento).ToString(format: @"00\.000\.000\/0000\-00");
        }
    }
}
