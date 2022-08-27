function BuscaCep() {
    try {
        document.getElementById('Cep').addEventListener('focusout', async function (e) {
            let dados = await fetch('https://viacep.com.br/ws/' + Cep.value + '/json/');
            let vcep = await dados.json();
            
            if (vcep.erro == 'true') {
                alert("Cep inválido!");
            }
            else {

                document.getElementById('Logradouro').value = (typeof vcep.logradouro === "undefined" ? "" : vcep.logradouro);
                document.getElementById('Complemento').value = (typeof vcep.complemento === "undefined" ? "" : vcep.complemento);
                document.getElementById('Bairro').value = (typeof vcep.bairro === "undefined" ? "" : vcep.bairro);
                document.getElementById('Cidade').value = (typeof vcep.localidade === "undefined" ? "" : vcep.localidade);
                document.getElementById('Estado').value = (typeof vcep.uf === "undefined" ? "" : vcep.uf);
            }
        });
    }
    catch (err) {
        window.alert("Site fora do ar!");
    }
}

// Função para processar os dados incluídos, bloqueando o usuário de acioná-lo demasiada vezes.
function ControlaClickBotao(idbt,idform)
{  
    document.getElementById(idbt).disabled = true;
    document.getElementById(idbt).value = "Processando";
    document.getElementById(idform).submit();
}

//Plugin DataTable
$(document).ready(function () {
    $('#tabela').DataTable( {
        "language": {
            "url" : "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese-Brasil.json"
        }
    } );
} );

