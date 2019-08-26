import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectDetailsComponent } from './project-details/project-details.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';

const routes: Routes = [
  { path: 'list', component: ProjectListComponent },
  { path: 'new', component: ProjectDetailsComponent },
  { path: ':id', component: ProjectDetailsComponent },
];


@NgModule({
  declarations: [ProjectListComponent, ProjectDetailsComponent],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes)
  ]
})
export class ProjectModule { }
