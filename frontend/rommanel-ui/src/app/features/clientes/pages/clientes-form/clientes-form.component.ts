import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { NgxMaskDirective, provideNgxMask } from 'ngx-mask';

import { ClienteService } from '../../services/cliente.service';
import { Cliente } from '../../models/cliente.model';

import { mapApiErrorsToForm } from '../../../../shared/utils/api-error-handler';

import {
  idadeMinimaValidator,
  cpfOuCnpjValidator,
  inscricaoEstadualValidator
} from '../../../../shared/utils/validators';

@Component({
  selector: 'app-clientes-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    RouterModule,
    NgxMaskDirective
  ],
  templateUrl: './clientes-form.component.html',
  styleUrl: './clientes-form.component.scss',
  providers: [provideNgxMask()]
})
export class ClientesFormComponent {

  private fb = inject(FormBuilder);
  private clienteService = inject(ClienteService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  form!: FormGroup;
  isEditMode = false;
  clienteId!: string;
  dataNascimentoLabel: string = 'Data de Nascimento';
  estados: { value: string; label: string }[] = [
    { value: 'AC', label: 'Acre' },
    { value: 'AL', label: 'Alagoas' },
    { value: 'AP', label: 'Amapá' },
    { value: 'AM', label: 'Amazonas' },
    { value: 'BA', label: 'Bahia' },
    { value: 'CE', label: 'Ceará' },
    { value: 'DF', label: 'Distrito Federal' },
    { value: 'ES', label: 'Espírito Santo' },
    { value: 'GO', label: 'Goiás' },
    { value: 'MA', label: 'Maranhão' },
    { value: 'MT', label: 'Mato Grosso' },
    { value: 'MS', label: 'Mato Grosso do Sul' },
    { value: 'MG', label: 'Minas Gerais' },
    { value: 'PA', label: 'Pará' },
    { value: 'PB', label: 'Paraíba' },
    { value: 'PR', label: 'Paraná' },
    { value: 'PE', label: 'Pernambuco' },
    { value: 'PI', label: 'Piauí' },
    { value: 'RJ', label: 'Rio de Janeiro' },
    { value: 'RN', label: 'Rio Grande do Norte' },
    { value: 'RS', label: 'Rio Grande do Sul' },
    { value: 'RO', label: 'Rondônia' },
    { value: 'RR', label: 'Roraima' },
    { value: 'SC', label: 'Santa Catarina' },
    { value: 'SP', label: 'São Paulo' },
    { value: 'SE', label: 'Sergipe' },
    { value: 'TO', label: 'Tocantins' }
  ];

  ngOnInit(): void {
    this.form = this.fb.group({
      nome: ['', Validators.required],
      documento: ['', [Validators.required, cpfOuCnpjValidator()]],
      dataNascimento: ['', [Validators.required, idadeMinimaValidator(18)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      inscricaoEstadual: [''],
      isentoIE: [true],
      cep: ['', Validators.required],
      logradouro: ['', Validators.required],
      numero: ['', Validators.required],
      bairro: ['', Validators.required],
      cidade: ['', Validators.required],
      estado: ['', Validators.required],
    }, {
      validators: [inscricaoEstadualValidator()]
    });

    this.form.get('documento')?.valueChanges.subscribe(value => {
      const cleaned = (value || '').replace(/\D/g, '');

      if (cleaned.length === 14) {
        // Pessoa jurídica
        this.dataNascimentoLabel = 'Data de Abertura';
        this.form.get('dataNascimento')?.clearValidators();
      } else {
        // Pessoa física
        this.dataNascimentoLabel = 'Data de Nascimento';
        this.form.get('dataNascimento')?.setValidators([
          Validators.required,
          idadeMinimaValidator(18)
        ]);
      }

      this.form.get('dataNascimento')?.updateValueAndValidity();
    });
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.clienteId = id;
        this.loadCliente(id);
      }
    });
  }
  private formatDateToInput(dateStr: string): string {
    const [day, month, year] = dateStr.split('/');
    return `${year}-${month}-${day}`;
  }
  loadCliente(id: string): void {
    console.log(id);
    this.clienteService.getById(id).subscribe(cliente => {
      this.form.patchValue({
        ...cliente,
        estado: cliente.estado,
        isentoIE: cliente.isentoIE,
        dataNascimento: this.formatDateToInput(cliente.dataNascimento)
      });
    });
  }

  save(): void {

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const cliente: Cliente = this.form.value;
    const operacao = this.clienteId
    ? this.clienteService.update(this.clienteId, cliente)
    : this.clienteService.create(cliente);

    operacao.subscribe({
        next: () => {
          this.router.navigate(['/clientes']);
        },
        error: (errorResponse) => {
          const errorBody = errorResponse.error;
          mapApiErrorsToForm(errorBody, this.form);
        }
      });
  }
}
