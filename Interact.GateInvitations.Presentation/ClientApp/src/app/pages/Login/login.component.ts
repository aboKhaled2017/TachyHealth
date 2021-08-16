import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { IErrorModel } from '../../models/Error.model';
import { AuthService } from '../../shared/Services/auth.service';
import { LoaderService } from '../../shared/Services/loader-service.service';
import { CommonFormUtility } from '../../Utilities/form.utility';

@Component({
  selector: 'app-dashboard',
  templateUrl: 'login.component.html'
})
export class LoginComponent {
handleProcess:false;
form: FormGroup;
    loading = false;
    submitted = false;
    returnUrl: string;
    errors = {
      username:[],
      password:[],
      g:[]
    };
  constructor(private authService: AuthService,
              private router:Router,
              private route:ActivatedRoute,
              private loaderService:LoaderService,
              private formBuilder:FormBuilder) { 
    loaderService.isLoading.subscribe(val=>this.loading=val);
  }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      username:this.formBuilder.control('', [Validators.required]),
      password: this.formBuilder.control('', Validators.required),
      userType:this.formBuilder.control(2)
  });
  
   this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }
  setErrors(error){
    CommonFormUtility.setErrors(error,this.errors,this.form);
  }
  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) return;
    this.authService.login(this.form.value)
        .pipe(first())
        .subscribe(
            data => {
                this.router.navigate([this.returnUrl]);
            },
            (err:IErrorModel) => {
          
               if(err.hasValidationError) this.setErrors(err.error);
            });
}
 }
