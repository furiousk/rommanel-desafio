export interface Cliente {
  id: string;
  nome: string;
  documento: string;
  dataNascimento: string;
  telefone: string;
  email: string;
  inscricaoEstadual?: string;
  isentoIE: boolean;
  tipoPessoa: string;
  cep: string;
  logradouro: string;
  numero: string;
  bairro: string;
  cidade: string;
  estado: string;
}