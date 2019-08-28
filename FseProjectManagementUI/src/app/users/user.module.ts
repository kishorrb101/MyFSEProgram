import { UserDetailsComponent } from './user-details/user-details.component';
import { UserListComponent } from './user-list/user-list.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { NameValidatorDirective } from './user-details/name-validator.directive';

const routes: Routes = [
  { path: 'list', component: UserListComponent },
  { path: 'new', component: UserDetailsComponent },
  { path: ':id', component: UserDetailsComponent },
];

@NgModule({
  declarations: [UserListComponent, UserDetailsComponent, NameValidatorDirective],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes)
  ]
})
export class UserModule { }
