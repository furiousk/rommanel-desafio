import { Routes } from '@angular/router';
import { ClientesListComponent } from './features/clientes/pages/clientes-list/clientes-list.component';

export const routes: Routes = [
  { path: '', redirectTo: 'clientes', pathMatch: 'full' },
  { path: 'clientes', component: ClientesListComponent }
];
