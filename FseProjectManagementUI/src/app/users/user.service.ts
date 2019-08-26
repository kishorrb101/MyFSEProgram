import { Injectable } from '@angular/core';
import { AppHttpService } from '../core/api/app-http.service';
import { appSettings } from '../app.settings';
import { Observable } from 'rxjs';
import { User } from './user';
import { FilterState } from '../shared/filter/models/filter-state';
import { DataResult } from '../shared/filter/models/data-result';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpService: AppHttpService) { }
  private readonly url: string = appSettings.api.user.path;

  query(state: FilterState): Observable<DataResult<User>> {
    return this.httpService.post<DataResult<User>>({ url: this.url + '/query' }, state);
  }

  getAll(): Observable<User[]> {
    return this.httpService.get<User[]>({ url: this.url + '/getUsers' });
  }

  create(user: User): Observable<User> {
    return this.httpService.post<User>({ url: `${this.url}/update` }, user);
  }

  update(user: User): Observable<User> {
    return this.httpService.post<User>({ url: `${this.url}/update` }, user);
  }

  get(id: string): Observable<User> {
    return this.httpService.get<User>({ url: `${this.url}/${id}` });
  }

  delete(id: number): Observable<User> {
    return this.httpService.delete<User>({ url: `${this.url}/delete/${id}` });
  }
}
