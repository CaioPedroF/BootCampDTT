mermaid
erDiagram
 MINA ||--o{ PRODUCAO : "produz"
 PRODUCAO ||--o{ ESTOQUE : "gera"
 MINA {
 string Codigo
 string Nome
 decimal Capacidade
 }
 PRODUCAO {
 int Id
 string MinaCodigo
 DateTime Data
 decimal Volume
 }
 ESTOQUE {
 int Id
 int ProducaoId
 decimal Quantidade
 string Local
 }