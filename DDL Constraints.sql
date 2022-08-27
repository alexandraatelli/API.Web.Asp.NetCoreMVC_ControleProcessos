--Consistencias Adicionais camada SGBD - SQL Server
--Bloqueia cadastro de pessoas com mesmo numero de documento
ALTER TABLE clientes ADD CONSTRAINT documento_duplicado_clientes UNIQUE (documento);

--Bloqueia tipo de documento errado (comprimento) de acor com o tipo fisica/juridica 
ALTER TABLE clientes ADD CONSTRAINT Doc_TipoPessoa_Incorreto 
CHECK ((tipopessoa=1 and len(documento)=11) or (tipopessoa=2 and len(documento)=14));

--Bloqueia cadastro de valores negativos para o processo
alter table processos ADD CONSTRAINT valor_causa_negativo CHECK (valorcausa >=0);

--Bloqueia formato de processo fora do padrao (somento numeros, comprimento 20)
alter table processos ADD CONSTRAINT formato_nr_processo_errado 
CHECK ((numeroprocesso not like '%[^0-9]%') and (len(numeroprocesso)=20));

--Bloqueia cadastro de processo duplicado
ALTER TABLE processos ADD CONSTRAINT numero_duplicado_processo UNIQUE (numeroprocesso);