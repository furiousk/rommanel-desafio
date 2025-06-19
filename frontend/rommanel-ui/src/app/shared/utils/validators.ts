import { AbstractControl, ValidationErrors, ValidatorFn, FormGroup } from '@angular/forms';

// ===== CPF =====
export function isValidCpf(cpf: string): boolean {
  cpf = cpf.replace(/\D/g, '');
  if (cpf.length !== 11 || /^(\d)\1+$/.test(cpf)) return false;

  let sum = 0;
  for (let i = 0; i < 9; i++) sum += +cpf[i] * (10 - i);
  let check1 = (sum * 10) % 11;
  if (check1 === 10 || check1 === 11) check1 = 0;
  if (check1 !== +cpf[9]) return false;

  sum = 0;
  for (let i = 0; i < 10; i++) sum += +cpf[i] * (11 - i);
  let check2 = (sum * 10) % 11;
  if (check2 === 10 || check2 === 11) check2 = 0;

  return check2 === +cpf[10];
}

// ===== CNPJ =====
export function isValidCnpj(cnpj: string): boolean {
  cnpj = cnpj.replace(/\D/g, '');
  if (cnpj.length !== 14 || /^(\d)\1+$/.test(cnpj)) return false;

  let size = cnpj.length - 2;
  let numbers = cnpj.substring(0, size);
  let digits = cnpj.substring(size);
  let sum = 0;
  let pos = size - 7;

  for (let i = size; i >= 1; i--) {
    sum += +numbers[size - i] * pos--;
    if (pos < 2) pos = 9;
  }

  let result = sum % 11 < 2 ? 0 : 11 - (sum % 11);
  if (result !== +digits[0]) return false;

  size++;
  numbers = cnpj.substring(0, size);
  sum = 0;
  pos = size - 7;

  for (let i = size; i >= 1; i--) {
    sum += +numbers[size - i] * pos--;
    if (pos < 2) pos = 9;
  }

  result = sum % 11 < 2 ? 0 : 11 - (sum % 11);
  return result === +digits[1];
}

// ===== CPF/CNPJ Validator =====
export function cpfOuCnpjValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value?.replace(/\D/g, '');
    if (!value) return null;

    if (value.length === 11 && !isValidCpf(value)) {
      return { cpfInvalido: true };
    }

    if (value.length === 14 && !isValidCnpj(value)) {
      return { cnpjInvalido: true };
    }

    if (value.length !== 11 && value.length !== 14) {
      return { documentoInvalido: true };
    }

    return null;
  };
}

// ===== Idade mínima (18 anos) =====
export function idadeMinimaValidator(minIdade = 18): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const nascimento = new Date(control.value);
    const hoje = new Date();

    const idade = hoje.getFullYear() - nascimento.getFullYear();
    const mes = hoje.getMonth() - nascimento.getMonth();
    const dia = hoje.getDate() - nascimento.getDate();

    const idadeFinal = mes < 0 || (mes === 0 && dia < 0) ? idade - 1 : idade;

    return idadeFinal >= minIdade ? null : { menorDeIdade: true };
  };
}

// ===== Regras de inscrição estadual =====
export function inscricaoEstadualValidator(): ValidatorFn {
  return (form: AbstractControl): ValidationErrors | null => {
    if (!(form instanceof FormGroup)) return null;

    const documento = form.get('documento')?.value?.replace(/\D/g, '');
    const isento = form.get('isentoIE')?.value;
    const ie = form.get('inscricaoEstadual')?.value?.trim();

    const isJuridica = documento?.length === 14;

    if (isJuridica && !isento && !ie) {
      return { inscricaoEstadualObrigatoria: true };
    }

    return null;
  };
}
