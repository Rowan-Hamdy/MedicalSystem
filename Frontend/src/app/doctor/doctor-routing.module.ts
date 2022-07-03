import {NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { DoctorLoginGuard } from "../_Guards/doctorLogin.guard";
import { DoctorEditComponent } from "./doctor-edit/doctor-edit.component";
import { DoctorHomeComponent } from "./doctor-home/doctor-home.component";
import { DoctorInfoComponent } from "./doctor-info/doctor-info.component";
import { DoctorPatientInfoComponent } from "./doctor-patient-info/doctor-patient-info.component";
import { DoctorPatientSearchComponent } from "./doctor-patient-search/doctor-patient-search.component";
import { DoctorPatientComponent } from "./doctor-patient/doctor-patient.component";
import { EditPrescriptionComponent } from "./edit-prescription/edit-prescription.component";
import { PatientHistoryComponent } from "./patient-history/patient-history.component";
import { RecordPrescriptionComponent } from "./record-prescription/record-prescription.component";


const routes:Routes =[
    {path:"",component:DoctorHomeComponent,canActivate:[DoctorLoginGuard],children:[
        {path:"info",component:DoctorInfoComponent},
        {path:"edit",component:DoctorEditComponent},
        {path:"patientsearch",component:DoctorPatientSearchComponent},
        {path:"patient/:id",component:DoctorPatientComponent,children:[
            {path:"info",component:DoctorPatientInfoComponent},
            {path:"history",component:PatientHistoryComponent},
        ]},
        {path:"recordpre/:id",component:RecordPrescriptionComponent},
        {path:"editpre/:id",component:EditPrescriptionComponent}
    ]},
    {path:"home",component:DoctorHomeComponent,canActivate:[DoctorLoginGuard]},
];

@NgModule({
    imports:[
        RouterModule.forChild(routes)
    ],
    exports:[
        RouterModule
    ]
})

export class doctorRoutingModule{

}