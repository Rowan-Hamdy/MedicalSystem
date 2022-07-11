import { Component, OnInit } from '@angular/core';
import { Patient } from 'src/app/_Models/patient';
import { Subscription } from 'rxjs';
import { PatientService } from '../Patient.service';
import { Router } from '@angular/router';
import { PatientHomeComponent } from '../patient-home/patient-home.component';

@Component({
  selector: 'app-patient-info',
  templateUrl: './patient-info.component.html',
  styleUrls: ['./patient-info.component.css']
})
export class PatientInfoComponent implements OnInit {

  patient:Patient=new Patient();
  sub:Subscription|null=null;

  constructor(public patientSer:PatientService,public router:Router) {}
  

  ngOnInit(): void {
    this.sub=this.patientSer.getPatient().subscribe(
      data=>{
        this.patient=data;
        console.log(this.patient.id);

        console.log(this.patient.profilePic);
      }
    );
   
  }

  ngOnDestroy(): void {
    this.sub?.unsubscribe();
  }

}