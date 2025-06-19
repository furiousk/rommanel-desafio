import { Routes } from '@angular/router';
import { ClientesListComponent } from './features/clientes/pages/clientes-list/clientes-list.component';
import { ClientesFormComponent } from './features/clientes/pages/clientes-form/clientes-form.component';

export const routes: Routes = [
  { path: '', redirectTo: 'clientes', pathMatch: 'full' },
  { path: 'clientes', component: ClientesListComponent },
  { path: 'clientes/novo', component: ClientesFormComponent },
  { path: 'clientes/:id/editar', component: ClientesFormComponent },
  { path: '**', redirectTo: 'clientes' }
];
