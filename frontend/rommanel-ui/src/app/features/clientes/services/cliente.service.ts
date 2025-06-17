import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Cliente } from '../models/cliente.model';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ClienteService {
  private readonly api = 'http://localhost:5000/api/clientes';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Cliente[]> {
    return this.http.get<Cliente[]>(`${this.api}/search`);
  }

  getById(id: string): Observable<Cliente> {
    return this.http.get<Cliente>(`${this.api}/${id}`);
  }

  create(cliente: Cliente): Observable<any> {
    return this.http.post(this.api, cliente);
  }

  update(id: string, patchData: Partial<Cliente>): Observable<any> {
    return this.http.patch(`${this.api}/${id}`, patchData);
  }

  delete(id: string): Observable<any> {
    return this.http.delete(`${this.api}/${id}`);
  }
}
