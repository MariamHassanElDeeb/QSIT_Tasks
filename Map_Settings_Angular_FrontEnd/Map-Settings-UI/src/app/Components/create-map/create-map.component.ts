import { environment } from 'src/enviroments/enviroments';
import { Router } from '@angular/router';
import { ChangeDetectorRef, Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators, AbstractControl, ValidationErrors, ValidatorFn, AsyncValidatorFn } from '@angular/forms';
import { CreatemapService } from 'src/app/Services/createmap.service';
import { AuthService } from 'src/app/Services/auth.service';
import { HttpClient } from '@angular/common/http';
import { ApiPaths } from 'src/app/Enums/api-paths';

@Component({
  selector: 'app-create-map',
  templateUrl: './create-map.component.html',
  styleUrls: ['./create-map.component.css']
})
export class CreateMapComponent implements OnInit {
  createmapform: FormGroup;
  TourCreated: string = "Tour Created";
  Types: any ;
  subtypes: any;
  selectedType: any=null;
  //selectedOption: string = 'Select Type ..';
  selectedSubtype:string='';


 constructor(private fb: FormBuilder, 
  private changeDetectorRef: ChangeDetectorRef, 
  private ClientService: CreatemapService,
   private router: Router,
   private authService: AuthService,
   private http: HttpClient) {

  this.createmapform = this.fb.group({
    clusterRedius: ['', [Validators.required, Validators.min(0.01), Validators.max(99), Validators.pattern(/^\d+(\.\d{1,3})?$/)]],
    isGeofenced: [''],
    timeBuffer: ['', [Validators.required, Validators.min(0),Validators.pattern(/^\d+(\.\d{1,3})?$/)]],
    locationBuffer: ['', [Validators.required, Validators.min(0.01), Validators.max(99),Validators.pattern(/^\d+(\.\d{1,3})?$/)]],
    duration: ['', [Validators.required, Validators.min(0),Validators.pattern(/^\d+(\.\d{1,3})?$/)]],
    mapSubtypeID:['',Validators.required]
  });
  
  // this.createtourform.get('postedAt')?.setValue(this.formattedDate);
}
  ngOnInit() {

    this.http.get(environment.baseUrl+ApiPaths.getmaptypes).subscribe(
      (response) => {
        this.Types=response;
       // console.log(this.selectedType);
    //console.log(this.selectedType);

      },
      (error) => {
        console.error(error);
      }
    );
    
  }

  onTypeChange(event: any) {
    const selectedValue = event.target.value;
    this.http.get(environment.baseUrl + ApiPaths.getmaptypes+ApiPaths.getsuptypes+selectedValue).subscribe(
      (response) => {
        this.subtypes=response;
      },
      (error) => {
        console.error(error);
      }
    );

    console.log(selectedValue); 
  }

  // onSubTypeChange(event: any) {
  //   const selectedValue = Number(event.target.value);
  //   console.log(selectedValue); 
  // }




  submitForm() {
    // Access the values of the URLs from the parent component and submit them along with the other form data
    const formValue = {...this.createmapform.value};
formValue.mapSubtypeID=Number(formValue.mapSubtypeID);
    ////console.log('form:', formValue);
    if (formValue) {

      this.ClientService.CreateMap(formValue).subscribe(
        {
          next: (data:any) => {
            console.log(data);
            setTimeout(() => {
              // Router navigation code
              this.router.navigateByUrl("/created");
            }, 3000);
          },
          error: () => {
            this.router.navigateByUrl('Error');
          }

        });

    }


  }

  public get clusterRediusValid(): boolean {
    return this.createmapform.controls["clusterRedius"].valid;
  }
  public get timeBufferValid(): boolean {
    return this.createmapform.controls["timeBuffer"].valid;
  }
  public get locationBufferValid(): boolean {
    return this.createmapform.controls["locationBuffer"].valid;
  }
  public get durationValid(): boolean {
    return this.createmapform.controls["duration"].valid;
  }
}
