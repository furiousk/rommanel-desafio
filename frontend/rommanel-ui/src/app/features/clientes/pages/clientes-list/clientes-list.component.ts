import { Component, inject, OnInit, DestroyRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';

import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';

import { ClienteService } from '../../services/cliente.service';
import { Cliente } from '../../models/cliente.model';
import { Router } from '@angular/router';
import {
  debounceTime,
  distinctUntilChanged,
  tap
} from 'rxjs/operators';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-clientes-list',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatTableModule,
    MatIconModule,
    MatButtonModule,
    MatTooltipModule,
    MatInputModule,
    MatFormFieldModule
  ],
  templateUrl: './clientes-list.component.html',
  styleUrl: './clientes-list.component.scss',
})

export class ClientesListComponent implements OnInit {

  private clienteService = inject(ClienteService);
  private router = inject(Router);
  private fb = inject(FormBuilder);
  private destroyRef = inject(DestroyRef);

  clientes: Cliente[] = [];
  colunas: string[] = ['nome', 'documento', 'email', 'cidade', 'acoes'];
  filtroForm: FormGroup = this.fb.group({
    nome: [''],
    cidade: ['']
  });

  ngOnInit(): void {
    this.configureReactiveSearch();
    this.find();
  }

  find() {
    const { nome="", cidade="" } = this.filtroForm.value;
    this.clienteService.search(nome, cidade).subscribe({
      next: (res) => (this.clientes = res)
    });
  }

  configureReactiveSearch() {
    this.filtroForm.valueChanges
      .pipe(
        debounceTime(400),
        distinctUntilChanged(),
        takeUntilDestroyed(this.destroyRef),
        tap(() => {
          const { nome, cidade } = this.filtroForm.value;
          if ((nome?.length ?? 0) >= 3 || (cidade?.length ?? 0) >= 3) { this.find() }
          if ((nome?.length ?? 0) === 0 && (cidade?.length ?? 0) === 0) { this.find() }
        })
      )
      .subscribe();
  }

  newClient() {
    this.router.navigate(['/clientes/novo']);
  }

  editClient(id: string) {
    this.router.navigate([`/clientes/${id}/editar`]);
  }

  removeClient(id: string) {
    if (confirm('Deseja realmente excluir este cliente?')) {
      this.clienteService.delete(id).subscribe(() => {
        this.clientes = this.clientes.filter(c => c.id !== id);
      });
    }
  }
}
