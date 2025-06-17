import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { ClienteService } from '../../services/cliente.service';
import { Cliente } from '../../models/cliente.model';

@Component({
  selector: 'app-clientes-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule
  ],
  templateUrl: './clientes-list.component.html',
  styleUrls: ['./clientes-list.component.scss']
})
export class ClientesListComponent implements OnInit {
  clientes: Cliente[] = [];
  loading = false;
  erro = '';

  constructor(private clienteService: ClienteService) {}

  ngOnInit(): void {
    this.buscarClientes();
  }

  buscarClientes(): void {
    this.loading = true;
    this.clienteService.getAll().subscribe({
      next: (res) => {
        this.clientes = res;
        this.loading = false;
      },
      error: () => {
        this.erro = 'Erro ao buscar clientes';
        this.loading = false;
      }
    });
  }

  deletar(id: string): void {
    if (!confirm('Deseja realmente excluir este cliente?')) return;

    this.clienteService.delete(id).subscribe({
      next: () => this.buscarClientes(),
      error: () => alert('Erro ao excluir cliente')
    });
  }
}
