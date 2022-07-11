import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GetDoctorPerCategoryComponent } from './get-doctor-per-category/get-doctor-per-category.component';
//<<<<<<< HEAD
import { PatientRoutingModule } from './patient.routing';
//=======
import { PatientRecordComponent } from './patient-record/patient-record.component';
import {FormsModule} from '@angular/forms';
import { FilterPipe } from './Pipes/filter.pipe';
import { SearchForDoctorComponent } from './search-for-doctor/search-for-doctor.component';
import {DropdownModule} from 'primeng/dropdown';
import { PatientHomeComponent } from './patient-home/patient-home.component'
import {RouterModule} from '@angular/router';
//import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
//import { BrowserModule } from '@angular/platform-browser';
import {InputTextModule} from 'primeng/inputtext';
import {ButtonModule} from 'primeng/button';
import {DialogModule} from 'primeng/dialog';
import { SafePipe } from './Pipes/safe.pipe';
import {RatingModule} from 'primeng/rating';
import { NumberOfRecordsInSameDatePipe } from './Pipes/number-of-records-in-same-date.pipe';
import { NgbRatingModule  } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    GetDoctorPerCategoryComponent,
    PatientRecordComponent,
    FilterPipe,
    SearchForDoctorComponent,
    PatientHomeComponent,
    SafePipe,
    NumberOfRecordsInSameDatePipe
  ],
  imports: [
    CommonModule,
    PatientRoutingModule,
    FormsModule,
    DropdownModule,
    RouterModule,
    InputTextModule,
    ButtonModule,
    //BrowserAnimationsModule,
    //BrowserModule,
    DialogModule,
    RatingModule,
    NgbRatingModule
  ]
})
export class PatientModule { }
